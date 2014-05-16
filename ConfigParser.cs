using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RFID_Inventory
{
    class ConfigParser
    {
        private const string file = @"Program Files\RFID_Inventory\config.xml";
        private XmlDocument config;

        private XmlNode lasttype;
        private XmlNode metalpower;
        private XmlNode metalfilter;
        private XmlNode plasticpower;
        private XmlNode plasticfilter;
        private XmlNode IP;
        private XmlNode port;
        private XmlNode deviceID;

        public ConfigParser()
        {
            config = new XmlDocument();
            config.Load(file);

            lasttype = config.SelectSingleNode("settings/type/last");
            metalpower = config.SelectSingleNode("settings/type/metal/power");
            metalfilter = config.SelectSingleNode("settings/type/metal/filter");
            plasticpower = config.SelectSingleNode("settings/type/plastic/power");
            plasticfilter = config.SelectSingleNode("settings/type/plastic/filter");
            IP = config.SelectSingleNode("settings/server/IP");
            port = config.SelectSingleNode("settings/server/port");
            deviceID = config.SelectSingleNode("settings/deviceID");
        }

        public string LastType
        {
            get { return lasttype.InnerText; }
            set { lasttype.InnerText = value; }
        }

        public string MetalPower
        {
            get { return metalpower.InnerText; }
            set { metalpower.InnerText = value; }
        }

        public string MetalFilter
        {
            get { return metalfilter.InnerText; }
            set { metalfilter.InnerText = value; }
        }

        public string PlasticPower
        {
            get { return plasticpower.InnerText; }
            set { plasticpower.InnerText = value; }
        }

        public string PlasticFilter
        {
            get { return plasticfilter.InnerText; }
            set { plasticfilter.InnerText = value; }
        }

        public string ServerIP
        {
            get { return IP.InnerText; }
            set { IP.InnerText = value; }
        }

        public string ServerPort
        {
            get { return port.InnerText; }
            set { port.InnerText = value; }
        }

        public string DeviceID
        {
            get { return deviceID.InnerText; }
            set { deviceID.InnerText = value; }
        }

        public void saveConfig()
        {
            XmlTextWriter writer = new XmlTextWriter(file, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            config.Save(writer);
            writer.Flush();
            writer.Close();
        }
    }
}