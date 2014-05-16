using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RFID_Inventory
{
    class RoomParser
    {
        public const string file = @"Program Files\RFID_Inventory\rooms.xml";
        private XmlDocument room;
        private Dictionary<string, Location> roomdict;

        public RoomParser()
        {
            room = new XmlDocument();
            room.Load(file);
            roomdict = new Dictionary<string, Location>();
            buildDict();
        }

        public Dictionary<string, Location> Rooms { get { return roomdict; } }

        private void buildDict()
        {
            XmlNodeList rooms = room.SelectNodes("rooms/room");
            foreach (XmlNode node in rooms)
            {
                string rfid = node.Attributes["rfid"].Value;
                string id = node.SelectSingleNode("id").InnerText;
                string name = node.SelectSingleNode("name").InnerText;
                Location loc = new Location(id, name);
                if(!roomdict.ContainsKey(rfid))
                    roomdict.Add(rfid, loc);
            }
        }
    }
}
