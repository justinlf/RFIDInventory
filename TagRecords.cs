using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RFID_Inventory
{
    //TODO should remember all uploaded files in xml file.
    class TagRecords
    {

        private string lastfile;
        private bool uploaded;

        public TagRecords()
        {
            uploaded = true;
        }

        public TagRecords(string filename, bool sent)
        {
            lastfile = filename;
            uploaded = sent;
        }

        public string LastFile
        {
            get { return lastfile; }
            set { lastfile = value; }
        }

        public bool Uploaded
        {
            get { return uploaded; }
            set { uploaded = value; }
        }
    }
}