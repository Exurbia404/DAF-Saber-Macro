namespace Section5PoC.Presentation
{
    partial class Form1
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
            this.listLabel = new System.Windows.Forms.Label();
            this.schematicsListBox = new System.Windows.Forms.ListBox();
            this.versionsListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listLabel
            // 
            this.listLabel.AutoSize = true;
            this.listLabel.Location = new System.Drawing.Point(19, 27);
            this.listLabel.Name = "listLabel";
            this.listLabel.Size = new System.Drawing.Size(93, 13);
            this.listLabel.TabIndex = 0;
            this.listLabel.Text = "found schematics:";
            // 
            // schematicsListBox
            // 
            this.schematicsListBox.FormattingEnabled = true;
            this.schematicsListBox.Location = new System.Drawing.Point(22, 43);
            this.schematicsListBox.Name = "schematicsListBox";
            this.schematicsListBox.Size = new System.Drawing.Size(234, 381);
            this.schematicsListBox.TabIndex = 1;
            this.schematicsListBox.DoubleClick += new System.EventHandler(this.schematicsListBox_DoubleClick_1);
            // 
            // versionsListBox
            // 
            this.versionsListBox.FormattingEnabled = true;
            this.versionsListBox.Location = new System.Drawing.Point(262, 43);
            this.versionsListBox.Name = "versionsListBox";
            this.versionsListBox.Size = new System.Drawing.Size(234, 381);
            this.versionsListBox.TabIndex = 2;
            this.versionsListBox.DoubleClick += new System.EventHandler(this.versionsListBox_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.versionsListBox);
            this.Controls.Add(this.schematicsListBox);
            this.Controls.Add(this.listLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label listLabel;
        private System.Windows.Forms.ListBox schematicsListBox;
        private System.Windows.Forms.ListBox versionsListBox;
    }
}