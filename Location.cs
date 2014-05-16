using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RFID_Inventory
{
    class Location
    {
        private string _ID;
        private string _Name;

        public Location(string id, string name)
        {
            _ID = id;
            _Name = name;
        }

        public string ID { get { return _ID; } }
        public string Name { get { return _Name; } }
    }
}