using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace RFID_Inventory
{
    class DuplicateHandler
    {
        private Inventory _Inventory = null;

        public DuplicateHandler(Inventory inv)
        {
            _Inventory = inv;
        }

        public void findDuplicates()
        {
            bool original_added;

            //Compare each tag from session to tags already added in census to find duplicates.
            foreach (ListViewItem sessionitem in _Inventory.session_LV.Items)
            {
                foreach (ListViewItem censusitem in _Inventory._TagList)
                {
                    //duplicate check
                    if (censusitem.Text == sessionitem.Text)
                    {
                        /*
                         * Need to determine if this is the first time this tag has been duplicated.
                         * If, so first instance will need to be added to list too.
                         */
                        original_added = false;
                        foreach (ListViewItem duplicate in _Inventory.duplicate_LV.Items)
                        {
                            if (censusitem.Text == duplicate.Text)
                            {
                                //not the first time this tag was duplicated
                                original_added = true;
                                break;
                            }
                        }
                        _Inventory.duplicate_LV.BeginUpdate();
                        //Make sure to add first instance of tag
                        if (!original_added)
                        {
                            insertDuplicate((ListViewItem)censusitem.Clone());
                            censusitem.Checked = true;
                        }
                        insertDuplicate((ListViewItem)sessionitem.Clone());
                        _Inventory.duplicate_LV.EndUpdate();
                        sessionitem.Checked = true;
                        break;
                    }
                }
            }
        }

        private void insertDuplicate(ListViewItem duplicate)
        {
            bool isInserted = false;
            bool firstEncounter = true;
            int lastCompare = -1;
            foreach (ListViewItem item in _Inventory.duplicate_LV.Items)
            {
                if (item.Text.CompareTo(duplicate.Text) == 0)
                {
                    lastCompare = item.Index;
                    int duplicateRSSI = int.Parse(duplicate.SubItems[2].Text);
                    int itemRSSI = int.Parse(item.SubItems[2].Text);
                    if (duplicateRSSI > itemRSSI)
                    {
                        _Inventory.duplicate_LV.Items.Insert(lastCompare, duplicate);
                        //If first encounter, then duplicate is max
                        if (firstEncounter)
                        {
                            duplicate.ForeColor = Color.Green;
                            item.ForeColor = Color.Red;
                        }
                        //Otherwise duplicate is in middle
                        else
                            duplicate.ForeColor = Color.Red;
                        isInserted = true;
                        break;
                    }
                    //Same tag found, no longer first encounter
                    firstEncounter = false;
                }
            }
            //Compared, but not selected => duplicate is min
            if (lastCompare >= 0 && !isInserted)
            {
                _Inventory.duplicate_LV.Items.Insert(lastCompare + 1, duplicate);
                duplicate.ForeColor = Color.Red;
            }
            //This item was the first instance of duplicated tag
            else if (!isInserted)
            {
                _Inventory.duplicate_LV.Items.Add(duplicate);
                //fist instance => current max
                duplicate.ForeColor = Color.Green;
            }
        }
    }
}