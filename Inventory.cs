using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Collections;

namespace RFID_Inventory
{

    public partial class Inventory : Form
    {
        private const string _DateFormat = "MMddyyyyHHmmss";

        //Device
        internal RFIDReader _ReaderAPI;
        private bool _IsConnected;
        private DeviceSettings _Device;

        //Tag data -Hashtable is for session -List is for entire census
        internal Dictionary<string, Location> _RoomTable;
        private Hashtable _TagTable;
        internal List<ListViewItem> _TagList;
        private int _MaxRead;
        private bool _InRoom;

        //User Interface
        //private delegate void UpdateStatusLabel(RFIDResults text);  TODO need?
        private delegate void UpdateUI(string stringData);
        private UpdateUI _UpdateUIHandler;
        internal User _User;

        //Queue TimeStamps for starting program and entering rooms
        internal Queue _TimeStamps;
        private TagRecords _TagRec;

        public Inventory()
        {
            InitializeComponent();

            _Device = new DeviceSettings(this);
            _UpdateUIHandler = new UpdateUI(myUpdateUI);

            defaultType();
            defaultServer();
            disableSetButton();


            RoomParser rooms = new RoomParser();
            _RoomTable = rooms.Rooms;
            _TagTable = new Hashtable(1023);
            _TagList = new List<ListViewItem>(5000);
            _MaxRead = -100;
            _InRoom = false;

            _User = new User();
            if (_User.ShowDialog() == DialogResult.OK)
                userLabel.Text = _User.UserName;
            else
                Application.Exit();

            Connect();

            _TimeStamps = new Queue();
            _TimeStamps.Enqueue(DateTime.Now.ToString(_DateFormat));
            _TagRec = new TagRecords();

            if (_IsConnected)
                StartSequence();
        }

        private void Connect()
        {
            uint port = 5084;
            _ReaderAPI = new RFIDReader("127.0.0.1", port, 0);

            try
            {
                _ReaderAPI.Connect();
                _Device.configureAntenna();
                /*
                 * Setup Events
                 */
                _ReaderAPI.Events.AttachTagDataWithReadEvent = false;
                _ReaderAPI.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);
                _ReaderAPI.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);
                _ReaderAPI.Events.NotifyBufferFullWarningEvent = true;
                _ReaderAPI.Events.NotifyReaderDisconnectEvent = true;

                _IsConnected = _ReaderAPI.IsConnected;

            }
            catch (OperationFailureException ofe)
            {
                userNotification.Text = ofe.Result.ToString();
                userNotification.Visible = true;
            }
        }

        private void Events_ReadNotify(object sender, Events.ReadEventArgs args)
        {
            this.Invoke(_UpdateUIHandler, new object[] { args.ReadEventData.TagData.TagID });
        }

        public void Events_StatusNotify(object sender, Events.StatusEventArgs args)
        {
            this.Invoke(_UpdateUIHandler, new object[] { args.StatusEventData.StatusEventType.ToString() });
        }

        private void myUpdateUI(string stringData)
        {
            if (stringData == "BUFFER_FULL_WARNING_EVENT")
            {
                userNotification.Text = "Buffer Full Warning";
                userNotification.Visible = true;
            }
            else if (stringData == "DISCONNECTION_EVENT")
            {
                userNotification.Text = "Radio Disconnected";
                userNotification.Visible = true;
                //TODO this.Connect("Reconnect");
            }
            else
            {
                TagData[] tagData = _ReaderAPI.Actions.GetReadTags(1000);

                if (tagData != null)
                {
                    for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
                    {
                        /*
                         * Read room tags and store in persistant table
                         */
                        if (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                        tagData[nIndex].OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
                        {
                            TagData tag = tagData[nIndex];
                            processRoomTag(tag);
                        }
                        /* 
                         * Display all inventoried tags or tags on which 
                         * Read access operation was successful
                        */
                        if (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE)
                        {
                            TagData tag = tagData[nIndex];
                            processCageTag(tag);
                        }//tag inventory
                    }//for
                }
            }
        }

        private void processRoomTag(TagData tag)
        {
            //bool isFound = false;
            string tagID = tag.TagID;
            string membank = tag.MemoryBankData;
            int rssi = tag.PeakRSSI;
            string tagIDshort = tag.TagID.Substring(tag.TagID.Length - 8);            

            if (tagID != _Device.DeviceID && _RoomTable.ContainsKey(tagIDshort))  //membank != "00000000"
            {
                //lock (_RoomTable.SyncRoot)
                //{
                //    isFound = _RoomTable.ContainsKey(tagID);
                //}
                //if (!isFound)
                //{
                //    _RoomTable.Add(tagID, membank);
                //}
                string name = _RoomTable[tagIDshort].Name;
                if (!room_CB.Items.Contains(name))
                {
                    room_CB.Items.Add(name);
                }
                int cbindex = room_CB.Items.IndexOf(name);
                if (rssi > _MaxRead)
                {
                    _MaxRead = rssi;
                    room_CB.SelectedIndex = cbindex;
                    enableSessionButton();
                    System.Media.SystemSounds.Exclamation.Play();
                }
            }
        }

        private void processCageTag(TagData tag)
        {
            bool isFound = false;
            string tagID = tag.TagID;
            string tagIDshort = tag.TagID.Substring(tag.TagID.Length - 8); 

            if (tagID != _Device.DeviceID && !_RoomTable.ContainsKey(tagIDshort))
            {
                lock (_TagTable.SyncRoot)
                {
                    isFound = _TagTable.ContainsKey(tagID);
                }

                if (isFound)
                {
                    ListViewItem item = (ListViewItem)_TagTable[tagID];

                    updateListViewItem(item, tag);
                }
                else
                {
                    ListViewItem item = createListViewItem(tag);

                    session_LV.BeginUpdate();
                    session_LV.Items.Add(item);
                    session_LV.EndUpdate();

                    lock (_TagTable.SyncRoot)
                    {
                        _TagTable.Add(tagID, item);
                    }
                    totalTagValueLabel.Text = _TagTable.Count.ToString();
                }
            }
        }

        private ListViewItem createListViewItem(TagData tag)
        {
            System.Media.SystemSounds.Beep.Play();
            string TagIDshort = tag.TagID.Substring(tag.TagID.Length - 8);
            ListViewItem item = new ListViewItem(TagIDshort);

            ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
            subItem.Text = room_CB.SelectedItem.ToString();
            item.SubItems.Add(subItem);

            subItem = new ListViewItem.ListViewSubItem();
            subItem.Text = tag.PeakRSSI.ToString();
            item.SubItems.Add(subItem);

            subItem = new ListViewItem.ListViewSubItem();
            subItem.Text = tag.TagSeenCount.ToString();
            item.SubItems.Add(subItem);

            subItem = new ListViewItem.ListViewSubItem();
            subItem.Text = DateTime.Now.ToString(_DateFormat);
            item.SubItems.Add(subItem);
            //TODO add full tagID at end if want to use tag locate feature //see TagLocationing and TagData.LocationInfo
            return item;
        }

        private void updateListViewItem(ListViewItem item, TagData tag)
        {
            int peak = tag.PeakRSSI;
            int count = tag.TagSeenCount;
            int total = 0;
            int rssi;
            try
            {
                rssi = int.Parse(item.SubItems[2].Text);
                total = int.Parse(item.SubItems[3].Text) + count;
                if (peak > rssi)
                    item.SubItems[2].Text = peak.ToString();
                item.SubItems[3].Text = total.ToString();
            }
            catch (FormatException fe)
            {
                userNotification.Text = fe.Message;
                userNotification.Visible = true;
            }
        }

        private void StartSequence()
        {
            _Device.readMemBank(true);
            if (_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                _ReaderAPI.Actions.TagAccess.OperationSequence.PerformSequence(_Device.AccessFilter, _Device.TriggerInfo, null);
        }

        private void StopSequence()
        {
            if (_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
            {
                _ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence();
                _Device.readMemBank(false);
            }
        }

        private void StartReading()
        {
            _ReaderAPI.Actions.Inventory.Perform(_Device.PostFilter, _Device.TriggerInfo, null);
        }

        private void StopReading()
        {
            _ReaderAPI.Actions.Inventory.Stop();
            AddTags();
            Clear();
        }

        private void AddTags()
        {
            DuplicateHandler tagcomparator = new DuplicateHandler(this);
            tagcomparator.findDuplicates();
            foreach (ListViewItem item in session_LV.Items)
            {
                _TagList.Add((ListViewItem)item.Clone());
            }
        }

        private void Clear()
        {
            session_LV.Items.Clear();
            room_CB.SelectedIndex = -1;
            room_CB.Items.Clear();
            _TagTable.Clear();
            totalTagValueLabel.Text = _TagTable.Count.ToString();
            _MaxRead = -100;
        }

        private void Reset()
        {
            Clear();
            _TagList.Clear();
            duplicate_LV.Items.Clear();
            _TimeStamps.Clear();
            _TimeStamps.Enqueue(DateTime.Now.ToString(_DateFormat));
        }

        private void switchUser()
        {
            if (_User.ShowDialog() == DialogResult.OK)
                userLabel.Text = _User.UserName;
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            switchUser();
        }

        private void menuSwitch_Click(object sender, EventArgs e)
        {
            switchUser();
        }

        private void enableSessionButton()
        {
            sessionButton.Enabled = true;
            sessionButton.Text = "Enter Room";
            sessionButton.BackColor = Color.LimeGreen;
        }

        private void sessionButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsConnected)
                {
                    if (sessionButton.Text == "Enter Room")
                    {
                        EnterRoom();
                        System.Media.SystemSounds.Exclamation.Play();
                    }
                    else if (sessionButton.Text == "Exit Room")
                    {
                        ExitRoom();
                        System.Media.SystemSounds.Exclamation.Play();
                    }
                }
                else
                {
                    userNotification.Text = "Please check the RFID is running on the home screen " +
                    "and no other RFID application is running.";
                    userNotification.Visible = true;
                }
            }
            catch (OperationFailureException ex)
            {
                userNotification.Text = ex.Result.ToString();
                userNotification.Visible = true;
            }
        }

        private void EnterRoom()
        {
            _InRoom = true;
            _TimeStamps.Enqueue(DateTime.Now.ToString(_DateFormat));
            sessionButton.Text = "Exit Room";
            sessionButton.BackColor = Color.Tomato;
            room_CB.Enabled = false;
            StopSequence();
            StartReading();
        }

        private void ExitRoom()
        {
            _InRoom = false;
            sessionButton.Text = "Scan Room";
            sessionButton.BackColor = SystemColors.InactiveBorder;
            sessionButton.Enabled = false;
            room_CB.Enabled = true;
            StopReading();
            StartSequence();
        }

        private void menuRemove_Click(object sender, EventArgs e)
        {
            foreach (int index in session_LV.SelectedIndices)
            {
                session_LV.Items.RemoveAt(index);
            }
        }

        private void Save()
        {
            if (_InRoom)
            {
                ExitRoom();
            }
            PDTFile censusfile = new PDTFile(this);
            _TagRec.LastFile = censusfile.write();
            _TagRec.Uploaded = false;
            Reset();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if ((_InRoom && session_LV.Items.Count > 0) || _TagList.Count > 0)
            {
                Save();
                userNotification.Text = string.Format("File: {0} successfully written.", _TagRec.LastFile);
                userNotification.Visible = true;
            }
            else
            {
                userNotification.Text = "Nothing to save!";
                userNotification.Visible = true;
            }
        }

        private void Upload()
        {
            PDTClient client = new PDTClient(_Device.ServerIP, _Device.ServerPort);
            PDTOpenFile openFileDialog = new PDTOpenFile(@"\Application Data\Inventory\", "*.pdt");
            string filename = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                filename = openFileDialog.FileName;
            else
                return;

            statusLabel.Text = "uploading...";
            this.Refresh();

            if (client.upload(filename))
            {
                userNotification.Text = string.Format("File: {0} uploaded to server: {1}.", filename, _Device.ServerIP);
                userNotification.Visible = true;
            }
            else
            {
                userNotification.Text = string.Format("Could not connect to server: {0}!", _Device.ServerIP);
                userNotification.Visible = true;
            }
            statusLabel.Text = "";
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            Upload();
        }

        private void enableSetButton()
        {
            setButton.Enabled = true;
            setButton.BackColor = SystemColors.Control;
        }

        private void disableSetButton()
        {
            setButton.BackColor = SystemColors.InactiveBorder;
            setButton.Enabled = false;
        }

        private void cagecard_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableSetButton();
        }

        private void serverIP_TB_TextChanged(object sender, EventArgs e)
        {
            enableSetButton();
        }

        private void serverPort_TB_TextChanged(object sender, EventArgs e)
        {
            enableSetButton();
        }

        private void setSettings()
        {
            _Device.ServerIP = serverIP_TB.Text;
            _Device.ServerPort = int.Parse(serverPort_TB.Text);
            _Device.LastType = cagecard_CB.SelectedIndex;
            _Device.configureAntenna();
            _Device.saveSettings();
            disableSetButton();
        }

        private void defaultType()
        {
            if (_Device.DefaultType < cagecard_CB.Items.Count)
                cagecard_CB.SelectedIndex = _Device.DefaultType;
            else
                cagecard_CB.SelectedIndex = 0;
        }

        private void defaultServer()
        {
            serverIP_TB.Text = _Device.DefaultIP;
            serverPort_TB.Text = _Device.DefaultPort.ToString();
        }

        private void defaultButton_Click(object sender, EventArgs e)
        {
            defaultType();
            defaultServer();
            setSettings();
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            setSettings();
        }

        private void Exit()
        {
            YesNo exitDialog = new YesNo("Are you sure you want to exit?");
            if (exitDialog.ShowDialog() == DialogResult.Yes)
            {
                this.Refresh();
                exitDialog.Message = ("Would you like to save?");
                if (((_InRoom && session_LV.Items.Count > 0) || _TagList.Count > 0)
                    && exitDialog.ShowDialog() == DialogResult.Yes)
                {
                    this.Refresh();
                    Save();
                }
                this.Visible = false;
                if(_IsConnected)
                    _ReaderAPI.Disconnect();
                userNotification.Visible = false;
                userNotification.Dispose();
                Application.Exit();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}