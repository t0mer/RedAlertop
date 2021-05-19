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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedAlertop));
            this.Logger = new RedAlrtop.BetterRichTextBox();
            this.SuspendLayout();
            // 
            // Logger
            // 
            this.Logger.BackColor = System.Drawing.Color.Black;
            this.Logger.ForeColor = System.Drawing.Color.White;
            this.Logger.Location = new System.Drawing.Point(12, 96);
            this.Logger.Name = "Logger";
            this.Logger.Size = new System.Drawing.Size(776, 342);
            this.Logger.TabIndex = 0;
            this.Logger.Text = "";
            // 
            // RedAlertop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Logger);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RedAlertop";
            this.Text = "RedAlertop";
            this.Load += new System.EventHandler(this.RedAlertop_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BetterRichTextBox Logger;
    }
}

