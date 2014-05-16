using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RFID_Inventory
{
    public partial class PDTOpenFile : Form
    {
        private String _filename;

        public PDTOpenFile(string path, string ext)
        {
            InitializeComponent();
            string[] directory = Directory.GetFileSystemEntries(path, ext);
            Array.Reverse(directory);
            string name;
            foreach (string file in directory)
            {
                name = file.Substring(path.Length);
                file_LB.Items.Add(name);
            }
        }

        public string FileName
        {
            get { return _filename; }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (file_LB.SelectedIndex > -1)
            {
                _filename = file_LB.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}