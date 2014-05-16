namespace RFID_Inventory
{
    partial class Inventory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inventoryTC = new System.Windows.Forms.TabControl();
            this.readTab = new System.Windows.Forms.TabPage();
            this.userLabel = new System.Windows.Forms.Label();
            this.userMenu = new System.Windows.Forms.ContextMenu();
            this.switchItem = new System.Windows.Forms.MenuItem();
            this.totalTagValueLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.session_LV = new System.Windows.Forms.ListView();
            this.columnHeader0 = new System.Windows.Forms.ColumnHeader();
            this.tagcontextMenu = new System.Windows.Forms.ContextMenu();
            this.removeItem = new System.Windows.Forms.MenuItem();
            this.room_CB = new System.Windows.Forms.ComboBox();
            this.sessionButton = new System.Windows.Forms.Button();
            this.manageTab = new System.Windows.Forms.TabPage();
            this.statusLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.duplicate_LV = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.serverPort_TB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.defaultButton = new System.Windows.Forms.Button();
            this.serverIP_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.filterLabel = new System.Windows.Forms.Label();
            this.txpowerLabel = new System.Windows.Forms.Label();
            this.setButton = new System.Windows.Forms.Button();
            this.cagecard_CB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userNotification = new Microsoft.WindowsCE.Forms.Notification();
            this.userButton = new System.Windows.Forms.MenuItem();
            this.exitButton = new System.Windows.Forms.MenuItem();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.inventoryTC.SuspendLayout();
            this.readTab.SuspendLayout();
            this.manageTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // inventoryTC
            // 
            this.inventoryTC.Controls.Add(this.readTab);
            this.inventoryTC.Controls.Add(this.manageTab);
            this.inventoryTC.Controls.Add(this.settingsTab);
            this.inventoryTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inventoryTC.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.inventoryTC.Location = new System.Drawing.Point(0, 0);
            this.inventoryTC.Name = "inventoryTC";
            this.inventoryTC.SelectedIndex = 0;
            this.inventoryTC.Size = new System.Drawing.Size(240, 188);
            this.inventoryTC.TabIndex = 0;
            // 
            // readTab
            // 
            this.readTab.Controls.Add(this.userLabel);
            this.readTab.Controls.Add(this.totalTagValueLabel);
            this.readTab.Controls.Add(this.label1);
            this.readTab.Controls.Add(this.session_LV);
            this.readTab.Controls.Add(this.room_CB);
            this.readTab.Controls.Add(this.sessionButton);
            this.readTab.Location = new System.Drawing.Point(0, 0);
            this.readTab.Name = "readTab";
            this.readTab.Size = new System.Drawing.Size(240, 161);
            this.readTab.Text = "Read";
            // 
            // userLabel
            // 
            this.userLabel.ContextMenu = this.userMenu;
            this.userLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.userLabel.Location = new System.Drawing.Point(7, 7);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(120, 20);
            this.userLabel.Text = "User";
            // 
            // userMenu
            // 
            this.userMenu.MenuItems.Add(this.switchItem);
            // 
            // switchItem
            // 
            this.switchItem.Text = "Switch";
            this.switchItem.Click += new System.EventHandler(this.menuSwitch_Click);
            // 
            // totalTagValueLabel
            // 
            this.totalTagValueLabel.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular);
            this.totalTagValueLabel.Location = new System.Drawing.Point(7, 80);
            this.totalTagValueLabel.Name = "totalTagValueLabel";
            this.totalTagValueLabel.Size = new System.Drawing.Size(100, 49);
            this.totalTagValueLabel.Text = "0";
            this.totalTagValueLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Tags Read:";
            // 
            // session_LV
            // 
            this.session_LV.Columns.Add(this.columnHeader0);
            this.session_LV.ContextMenu = this.tagcontextMenu;
            this.session_LV.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.session_LV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.session_LV.Location = new System.Drawing.Point(134, 4);
            this.session_LV.Name = "session_LV";
            this.session_LV.Size = new System.Drawing.Size(102, 155);
            this.session_LV.TabIndex = 3;
            this.session_LV.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Tag ID";
            this.columnHeader0.Width = 86;
            // 
            // tagcontextMenu
            // 
            this.tagcontextMenu.MenuItems.Add(this.removeItem);
            // 
            // removeItem
            // 
            this.removeItem.Text = "Remove";
            this.removeItem.Click += new System.EventHandler(this.menuRemove_Click);
            // 
            // room_CB
            // 
            this.room_CB.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.room_CB.Location = new System.Drawing.Point(7, 140);
            this.room_CB.Name = "room_CB";
            this.room_CB.Size = new System.Drawing.Size(120, 24);
            this.room_CB.TabIndex = 2;
            // 
            // sessionButton
            // 
            this.sessionButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.sessionButton.Enabled = false;
            this.sessionButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.sessionButton.Location = new System.Drawing.Point(7, 29);
            this.sessionButton.Name = "sessionButton";
            this.sessionButton.Size = new System.Drawing.Size(100, 27);
            this.sessionButton.TabIndex = 0;
            this.sessionButton.Text = "Scan Room";
            this.sessionButton.Click += new System.EventHandler(this.sessionButton_Click);
            // 
            // manageTab
            // 
            this.manageTab.Controls.Add(this.statusLabel);
            this.manageTab.Controls.Add(this.saveButton);
            this.manageTab.Controls.Add(this.uploadButton);
            this.manageTab.Controls.Add(this.duplicate_LV);
            this.manageTab.Location = new System.Drawing.Point(0, 0);
            this.manageTab.Name = "manageTab";
            this.manageTab.Size = new System.Drawing.Size(232, 159);
            this.manageTab.Text = "Manage";
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.Location = new System.Drawing.Point(82, 141);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(75, 18);
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.saveButton.Location = new System.Drawing.Point(164, 138);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(72, 20);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.uploadButton.Location = new System.Drawing.Point(4, 138);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(72, 20);
            this.uploadButton.TabIndex = 4;
            this.uploadButton.Text = "Upload";
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // duplicate_LV
            // 
            this.duplicate_LV.CheckBoxes = true;
            this.duplicate_LV.Columns.Add(this.columnHeader1);
            this.duplicate_LV.Columns.Add(this.columnHeader2);
            this.duplicate_LV.Columns.Add(this.columnHeader3);
            this.duplicate_LV.Columns.Add(this.columnHeader4);
            this.duplicate_LV.Columns.Add(this.columnHeader5);
            this.duplicate_LV.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.duplicate_LV.Location = new System.Drawing.Point(4, 4);
            this.duplicate_LV.Name = "duplicate_LV";
            this.duplicate_LV.Size = new System.Drawing.Size(232, 128);
            this.duplicate_LV.TabIndex = 3;
            this.duplicate_LV.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Duplicate Tags";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Room";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "RSSI";
            this.columnHeader3.Width = 48;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Count";
            this.columnHeader4.Width = 48;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date/Time";
            this.columnHeader5.Width = 120;
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.serverPort_TB);
            this.settingsTab.Controls.Add(this.label4);
            this.settingsTab.Controls.Add(this.defaultButton);
            this.settingsTab.Controls.Add(this.serverIP_TB);
            this.settingsTab.Controls.Add(this.label3);
            this.settingsTab.Controls.Add(this.filterLabel);
            this.settingsTab.Controls.Add(this.txpowerLabel);
            this.settingsTab.Controls.Add(this.setButton);
            this.settingsTab.Controls.Add(this.cagecard_CB);
            this.settingsTab.Controls.Add(this.label2);
            this.settingsTab.Location = new System.Drawing.Point(0, 0);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Size = new System.Drawing.Size(232, 159);
            this.settingsTab.Text = "Settings";
            // 
            // serverPort_TB
            // 
            this.serverPort_TB.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.serverPort_TB.Location = new System.Drawing.Point(133, 112);
            this.serverPort_TB.Name = "serverPort_TB";
            this.serverPort_TB.Size = new System.Drawing.Size(100, 23);
            this.serverPort_TB.TabIndex = 14;
            this.serverPort_TB.TextChanged += new System.EventHandler(this.serverPort_TB_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(51, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.Text = "Server Port:    ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // defaultButton
            // 
            this.defaultButton.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.defaultButton.Location = new System.Drawing.Point(7, 138);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(72, 20);
            this.defaultButton.TabIndex = 7;
            this.defaultButton.Text = "Default";
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // serverIP_TB
            // 
            this.serverIP_TB.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.serverIP_TB.Location = new System.Drawing.Point(133, 84);
            this.serverIP_TB.Name = "serverIP_TB";
            this.serverIP_TB.Size = new System.Drawing.Size(100, 23);
            this.serverIP_TB.TabIndex = 6;
            this.serverIP_TB.TextChanged += new System.EventHandler(this.serverIP_TB_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(60, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.Text = "Server IP:    ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // filterLabel
            // 
            this.filterLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.filterLabel.Location = new System.Drawing.Point(92, 54);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(100, 20);
            this.filterLabel.Text = "Filter:  0 RSSI";
            // 
            // txpowerLabel
            // 
            this.txpowerLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.txpowerLabel.Location = new System.Drawing.Point(65, 34);
            this.txpowerLabel.Name = "txpowerLabel";
            this.txpowerLabel.Size = new System.Drawing.Size(127, 20);
            this.txpowerLabel.Text = "Tx Power:  0 dBm";
            // 
            // setButton
            // 
            this.setButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.setButton.Enabled = false;
            this.setButton.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.setButton.Location = new System.Drawing.Point(161, 138);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(72, 20);
            this.setButton.TabIndex = 3;
            this.setButton.Text = "Set";
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // cagecard_CB
            // 
            this.cagecard_CB.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.cagecard_CB.Items.Add("metal");
            this.cagecard_CB.Items.Add("plastic");
            this.cagecard_CB.Location = new System.Drawing.Point(133, 7);
            this.cagecard_CB.Name = "cagecard_CB";
            this.cagecard_CB.Size = new System.Drawing.Size(100, 24);
            this.cagecard_CB.TabIndex = 1;
            this.cagecard_CB.SelectedIndexChanged += new System.EventHandler(this.cagecard_CB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(21, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.Text = "Cage Card Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // userNotification
            // 
            this.userNotification.InitialDuration = 5;
            this.userNotification.Text = "Welcome!";
            // 
            // userButton
            // 
            this.userButton.Text = "User";
            this.userButton.Click += new System.EventHandler(this.userButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Text = "Exit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.userButton);
            this.mainMenu.MenuItems.Add(this.exitButton);
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.ControlBox = false;
            this.Controls.Add(this.inventoryTC);
            this.Menu = this.mainMenu;
            this.Name = "Inventory";
            this.Text = "Inventory";
            this.inventoryTC.ResumeLayout(false);
            this.readTab.ResumeLayout(false);
            this.manageTab.ResumeLayout(false);
            this.settingsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl inventoryTC;
        private System.Windows.Forms.TabPage readTab;
        private System.Windows.Forms.TabPage manageTab;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Button sessionButton;
        private System.Windows.Forms.ContextMenu tagcontextMenu;
        private System.Windows.Forms.MenuItem removeItem;
        internal System.Windows.Forms.ListView session_LV;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        internal System.Windows.Forms.ListView duplicate_LV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label totalTagValueLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ComboBox cagecard_CB;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button setButton;
        internal System.Windows.Forms.Label txpowerLabel;
        internal System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        internal System.Windows.Forms.ComboBox room_CB;
        internal Microsoft.WindowsCE.Forms.Notification userNotification;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button defaultButton;
        private System.Windows.Forms.TextBox serverIP_TB;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TextBox serverPort_TB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.MenuItem userButton;
        private System.Windows.Forms.MenuItem exitButton;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.ContextMenu userMenu;
        private System.Windows.Forms.MenuItem switchItem;
    }
}