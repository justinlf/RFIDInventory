using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID3;

namespace RFID_Inventory
{
    class DeviceSettings
    {
        private ConfigParser _Parser;
        private Inventory _Inventory;
        private RssiRangeFilter _Range;
        private AccessFilter _AccessFilter;
        private PostFilter _PostFilter;
        private TriggerInfo _TriggerInfo;
        private TagAccess.Sequence.Operation _Op;

        //Enumerate cage card type for power and filter switches
        public enum CAGE_CARD_TYPE
        {
            metal = 0,
            plastic = 1,
        }

        public DeviceSettings(Inventory inv)
        {
            _Parser = new ConfigParser();
            _Inventory = inv;
            setDefaults();

            //Construct access/post filter with no limit            
            _Range = new RssiRangeFilter();
            _AccessFilter = new AccessFilter();
            _AccessFilter.UseRSSIRangeFilter = true;
            _AccessFilter.RssiRangeFilter = _Range;
            _PostFilter = new PostFilter();
            _PostFilter.UseRSSIRangeFilter = true;
            _PostFilter.RssiRangeFilter = _Range;

            //Will trigger inventory with trigger button
            _TriggerInfo = new TriggerInfo();
            _TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD;
            _TriggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED;
            _TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT;
            _TriggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED;

            //Read user membank sequence
            _Op = new TagAccess.Sequence.Operation();
            _Op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
            _Op.ReadAccessParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
            _Op.ReadAccessParams.ByteCount = 4;
            _Op.ReadAccessParams.ByteOffset = 0;
            _Op.ReadAccessParams.AccessPassword = 0;
        }

        #region Getters
        public sbyte Filter { get; private set; }
        public int Power { get; private set; }
        public string DefaultIP { get; private set; }
        public int DefaultPort { get; private set; }
        public int DefaultType { get; private set; }
        public string ServerIP
        {
            get { return _Parser.ServerIP; }
            set { _Parser.ServerIP = value; }
        }
        public int ServerPort
        {
            get
            {
                int port = 0;
                try
                {
                    port = int.Parse(_Parser.ServerPort);
                }
                catch (FormatException fe)
                {
                    configError(fe);
                }
                return port;
            }
            set { _Parser.ServerPort = value.ToString(); }
        }
        public string DeviceID
        {
            get { return _Parser.DeviceID; }
            set { _Parser.DeviceID = value; }
        }
        public int LastType
        {
            get
            {
                int type = 0;
                try
                {
                    type = int.Parse(_Parser.LastType);
                }
                catch (FormatException fe)
                {
                    configError(fe);
                }
                return type;
            }
            set { _Parser.LastType = value.ToString(); }
        }
        public AccessFilter AccessFilter
        {
            get { return _AccessFilter; }
        }
        public PostFilter PostFilter
        {
            get { return _PostFilter; }
        }
        public TriggerInfo TriggerInfo
        {
            get { return _TriggerInfo; }
        }
        #endregion

        //Must be connected prior to setting transmit power
        public void configureAntenna()
        {
            readSettings();
            if (Power != 0 && Filter != 0)
            {
                //filter associated with power
                _Range.PeakRSSILowerLimit = Filter;
                setTxPower();
                updateLabels();
            }
        }        

        private void readSettings()
        {
            string power;
            string filter;

            CAGE_CARD_TYPE type = (CAGE_CARD_TYPE)Enum.Parse(typeof(CAGE_CARD_TYPE), _Inventory.cagecard_CB.SelectedItem.ToString(), true);
            switch (type)
            {
                case CAGE_CARD_TYPE.metal:
                    power = _Parser.MetalPower;
                    filter = _Parser.MetalFilter;
                    break;
                case CAGE_CARD_TYPE.plastic:
                    power = _Parser.PlasticPower;
                    filter = _Parser.PlasticFilter;
                    break;
                default:
                    power = "16";
                    filter = "-51";
                    break;
            }
            try
            {
                Filter = sbyte.Parse(filter);
                Power = int.Parse(power);
            }
            catch (FormatException fe)
            {
                configError(fe);
            }
        }

        private void setTxPower()
        {
            Antennas.Config antConfig;
            ushort[] antIDList = _Inventory._ReaderAPI.Config.Antennas.AvailableAntennas;
            int[] txValues = _Inventory._ReaderAPI.ReaderCapabilities.TransmitPowerLevelValues;

            //index = 10*(power-5) //power starts at 5dBm and increments in .1 intervals
            int txIndex = 10 * (Power - 5);
            for (int index = 0; index < antIDList.Length; index++)
            {
                int antID = (int)antIDList[index];
                try
                {
                    antConfig = _Inventory._ReaderAPI.Config.Antennas[antID].GetConfig();
                    antConfig.TransmitPowerIndex = (ushort)txIndex;
                    _Inventory._ReaderAPI.Config.Antennas[antID].SetConfig(antConfig);
                }
                catch (OperationFailureException exception)
                {
                    _Inventory.userNotification.Text = "Error setting power: " + exception;
                    _Inventory.userNotification.Visible = true;
                }
            }
        }

        private void setDefaults()
        {
            DefaultIP = _Parser.ServerIP;
            int port = 0;
            int type = 0;
            try
            {
                port = int.Parse(_Parser.ServerPort);
                type = int.Parse(_Parser.LastType);
            }
            catch (FormatException fe)
            {
                configError(fe);
            }
            DefaultPort = port;
            DefaultType = type;
        }

        private void updateLabels()
        {
            _Inventory.txpowerLabel.Text = "Tx Power:  " + Power + " dBm";
            _Inventory.filterLabel.Text = "Filter: " + Filter + " dBm";
        }

        private void configError(Exception e)
        {
            _Inventory.userNotification.Text = "Config file error: " + e;
            _Inventory.userNotification.Visible = true;
        }

        public void readMemBank(bool access)
        {
            _Inventory._ReaderAPI.Actions.TagAccess.OperationSequence.DeleteAll();
            if (access)
                _Inventory._ReaderAPI.Actions.TagAccess.OperationSequence.Add(_Op);
        }

        public void saveSettings()
        {
            _Parser.saveConfig();
        }
    }
}