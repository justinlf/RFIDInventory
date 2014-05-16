using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace RFID_Inventory
{
    class PDTFile
    {
        private const string _FileFormat = "yyyyMMddHHmmss";

        private Inventory _Inventory = null;

        public PDTFile(Inventory inv)
        {
            _Inventory = inv;
        }

        public string write()
        {
            string room = null;
            string appdatpath = @"\Application Data\Inventory\";
            string created = DateTime.Now.ToString(_FileFormat);
            string filename =  created + ".pdt";
            StreamWriter outfile = new StreamWriter(appdatpath + filename);
            //staff
            string staffid = _Inventory._User.UserID;
            outfile.WriteLine(_Inventory._TimeStamps.Dequeue() + " ST" + staffid.PadLeft(8,'0'));
            foreach (ListViewItem censusitem in _Inventory._TagList)
            {
                /*
                 * Once tags start showing new room, write new location
                 */
                if (censusitem.SubItems[1].Text != room)
                {
                    room = censusitem.SubItems[1].Text;
                    string roomid = _Inventory._RoomTable.FirstOrDefault(x => x.Value.Name == room).Value.ID;
                    outfile.WriteLine(_Inventory._TimeStamps.Dequeue() + " LO" + roomid.PadLeft(8,'0'));
                }

                /*
                 * If the census item is checked, it is duplicate, and only the correct duplicate tag shall be written
                 */
                if (censusitem.Checked)
                {
                    ListViewItem correcttag = null;
                    foreach (ListViewItem duplicate in _Inventory.duplicate_LV.Items)
                    {
                        //tag selected by user, write this tag
                        if (censusitem.Text == duplicate.Text && duplicate.Checked)
                        {
                            if (duplicate.SubItems[1].Text == room)
                            {
                                correcttag = duplicate;
                                break;
                            }
                            //user selected a tag, do not use autoselected tag
                            else
                                correcttag = null;
                        }
                        //auto selected tag, write this tag if user did not select the duplicate tag to be written
                        else if (censusitem.Text == duplicate.Text && duplicate.SubItems[1].Text == room && duplicate.ForeColor == Color.Green)
                            correcttag = duplicate;
                    }
                    //only write tag from correct location
                    if(correcttag != null)
                        outfile.WriteLine(correcttag.SubItems[4].Text + " CC" + censusitem.Text);
                }
                //not duplicate, write
                else
                    outfile.WriteLine(censusitem.SubItems[4].Text + " CC" + censusitem.Text);
            }
            outfile.Flush();
            outfile.Close();

            return filename;
        }
    }
}