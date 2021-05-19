namespace RedAlrtop
{
    partial class RedAlertop
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedAlertop));
            this.label1 = new System.Windows.Forms.Label();
            this.AutoStart = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AlertRegion = new System.Windows.Forms.TextBox();
            this.SoundFileLabel = new System.Windows.Forms.Label();
            this.SoundFilePath = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.SoundFileBrowser = new System.Windows.Forms.Button();
            this.SoundFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Logger = new RedAlrtop.BetterRichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(10, 409);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Settings";
            // 
            // AutoStart
            // 
            this.AutoStart.AutoSize = true;
            this.AutoStart.Location = new System.Drawing.Point(10, 439);
            this.AutoStart.Name = "AutoStart";
            this.AutoStart.Size = new System.Drawing.Size(195, 17);
            this.AutoStart.TabIndex = 2;
            this.AutoStart.Text = "Start RedAlertop at windows startup";
            this.AutoStart.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 472);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Region:";
            // 
            // AlertRegion
            // 
            this.AlertRegion.Location = new System.Drawing.Point(78, 469);
            this.AlertRegion.Name = "AlertRegion";
            this.AlertRegion.Size = new System.Drawing.Size(446, 20);
            this.AlertRegion.TabIndex = 4;
            // 
            // SoundFileLabel
            // 
            this.SoundFileLabel.AutoSize = true;
            this.SoundFileLabel.Location = new System.Drawing.Point(12, 508);
            this.SoundFileLabel.Name = "SoundFileLabel";
            this.SoundFileLabel.Size = new System.Drawing.Size(60, 13);
            this.SoundFileLabel.TabIndex = 5;
            this.SoundFileLabel.Text = "Sound File:";
            // 
            // SoundFilePath
            // 
            this.SoundFilePath.Location = new System.Drawing.Point(78, 504);
            this.SoundFilePath.Name = "SoundFilePath";
            this.SoundFilePath.ReadOnly = true;
            this.SoundFilePath.Size = new System.Drawing.Size(446, 20);
            this.SoundFilePath.TabIndex = 6;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(247, 533);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(98, 25);
            this.Save.TabIndex = 7;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // SoundFileBrowser
            // 
            this.SoundFileBrowser.Location = new System.Drawing.Point(529, 501);
            this.SoundFileBrowser.Name = "SoundFileBrowser";
            this.SoundFileBrowser.Size = new System.Drawing.Size(36, 26);
            this.SoundFileBrowser.TabIndex = 8;
            this.SoundFileBrowser.Text = "...";
            this.SoundFileBrowser.UseVisualStyleBackColor = true;
            this.SoundFileBrowser.Click += new System.EventHandler(this.SoundFileBrowser_Click);
            // 
            // SoundFileDialog
            // 
            this.SoundFileDialog.Filter = "Audio|*.wav;*.mp3;*.mp4";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Red Alert";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // Logger
            // 
            this.Logger.BackColor = System.Drawing.Color.Black;
            this.Logger.ForeColor = System.Drawing.Color.White;
            this.Logger.Location = new System.Drawing.Point(11, 12);
            this.Logger.Name = "Logger";
            this.Logger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Logger.Size = new System.Drawing.Size(889, 384);
            this.Logger.TabIndex = 0;
            this.Logger.Text = "";
            // 
            // RedAlertop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 577);
            this.Controls.Add(this.SoundFileBrowser);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.SoundFilePath);
            this.Controls.Add(this.SoundFileLabel);
            this.Controls.Add(this.AlertRegion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AutoStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Logger);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RedAlertop";
            this.Text = "RedAlertop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RedAlertop_FormClosing);
            this.Load += new System.EventHandler(this.RedAlertop_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BetterRichTextBox Logger;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox AutoStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AlertRegion;
        private System.Windows.Forms.Label SoundFileLabel;
        private System.Windows.Forms.TextBox SoundFilePath;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button SoundFileBrowser;
        private System.Windows.Forms.OpenFileDialog SoundFileDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

