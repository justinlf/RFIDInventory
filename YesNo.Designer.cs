namespace RFID_Inventory
{
    partial class YesNo
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
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // yesButton
            // 
            this.yesButton.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.yesButton.Location = new System.Drawing.Point(32, 100);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(72, 20);
            this.yesButton.TabIndex = 0;
            this.yesButton.Text = "Yes";
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // noButton
            // 
            this.noButton.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.noButton.Location = new System.Drawing.Point(136, 100);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(72, 20);
            this.noButton.TabIndex = 1;
            this.noButton.Text = "No";
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label.Location = new System.Drawing.Point(0, 38);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(240, 20);
            this.label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // YesNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 214);
            this.ControlBox = false;
            this.Controls.Add(this.label);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Name = "YesNo";
            this.Text = "Inventory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Label label;
    }
}