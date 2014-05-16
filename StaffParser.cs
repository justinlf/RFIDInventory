using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RFID_Inventory
{
    class StaffParser
    {
        public const string file = @"Program Files\RFID_Inventory\staff.xml";
        private XmlDocument staff;
        private Dictionary<string, string> staffdict;

        public StaffParser()
        {
            staff = new XmlDocument();
            staff.Load(file);
            staffdict = new Dictionary<string, string>();
            buildDict();
        }

        public Dictionary<string, string> Staff { get { return staffdict; } }

        private void buildDict()
        {
            XmlNodeList members = staff.SelectNodes("authorized_staff/staff");
            foreach (XmlNode node in members)
            {
                string id = node.Attributes["id"].Value;
                string name = String.Format("{0}, {1}", node.SelectSingleNode("last").InnerText,
                    node.SelectSingleNode("first").InnerText);
                staffdict.Add(id, name);
            }
        }

        private bool addtoDict(string id, string first, string last)
        {
            string name = String.Format("{0}, {1}", last, first);
            if (!staffdict.ContainsKey(id))
            {
                staffdict.Add(id, name);
                return true;
            }
            return false;
        }

        public void addStaff(string id, string first, string last)
        {
            if (addtoDict(id, first, last))
            {
                XmlElement newStaff = staff.CreateElement("staff");
                XmlAttribute newID = staff.CreateAttribute("id");
                newID.Value = id;
                newStaff.SetAttributeNode(newID);
                XmlElement newFirst = staff.CreateElement("first");
                newFirst.InnerText = first;
                XmlElement newLast = staff.CreateElement("last");
                newLast.InnerText = last;
                newStaff.AppendChild(newFirst);
                newStaff.AppendChild(newLast);
                staff.DocumentElement.AppendChild(newStaff);
            }
        }

        public bool contains(string id)
        {
            return staffdict.ContainsKey(id);
        }

        public void saveStaff()
        {
            XmlTextWriter writer = new XmlTextWriter(file, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            staff.Save(writer);
            writer.Flush();
        }
    }
}