using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RFID_Inventory
{
    public partial class User : Form
    {
        private StaffParser _staff;
        private string _username;

        public User()
        {
            InitializeComponent();
            _staff = new StaffParser();
            foreach (string user in _staff.Staff.Values)
            {
                user_LB.Items.Add(user);
            }
        }

        public string UserName
        {
            get { return _username; }
        }

        public string UserID
        {
            get
            {
                return _staff.Staff.FirstOrDefault(x => x.Value == _username).Key;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (user_LB.SelectedIndex > -1)
            {
                _username = user_LB.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}