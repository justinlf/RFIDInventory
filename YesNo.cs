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
    public partial class YesNo : Form
    {
        public YesNo()
        {
            InitializeComponent();
        }

        public YesNo(String msg) : this()
        {
            Message = msg;
        }

        public string Message
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}