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
            this.label1 = new System.Windows.Forms.Label();
            this.searchSchematicTextBox = new System.Windows.Forms.TextBox();
            this.searchVersionTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listLabel
            // 
            this.listLabel.AutoSize = true;
            this.listLabel.Location = new System.Drawing.Point(19, 20);
            this.listLabel.Name = "listLabel";
            this.listLabel.Size = new System.Drawing.Size(93, 13);
            this.listLabel.TabIndex = 0;
            this.listLabel.Text = "found schematics:";
            // 
            // schematicsListBox
            // 
            this.schematicsListBox.FormattingEnabled = true;
            this.schematicsListBox.Location = new System.Drawing.Point(22, 62);
            this.schematicsListBox.Name = "schematicsListBox";
            this.schematicsListBox.Size = new System.Drawing.Size(234, 381);
            this.schematicsListBox.TabIndex = 1;
            this.schematicsListBox.DoubleClick += new System.EventHandler(this.schematicsListBox_DoubleClick_1);
            // 
            // versionsListBox
            // 
            this.versionsListBox.FormattingEnabled = true;
            this.versionsListBox.Location = new System.Drawing.Point(262, 62);
            this.versionsListBox.Name = "versionsListBox";
            this.versionsListBox.Size = new System.Drawing.Size(234, 381);
            this.versionsListBox.TabIndex = 2;
            this.versionsListBox.DoubleClick += new System.EventHandler(this.versionsListBox_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "found versions:";
            // 
            // searchSchematicTextBox
            // 
            this.searchSchematicTextBox.Location = new System.Drawing.Point(22, 36);
            this.searchSchematicTextBox.Name = "searchSchematicTextBox";
            this.searchSchematicTextBox.Size = new System.Drawing.Size(234, 20);
            this.searchSchematicTextBox.TabIndex = 4;
            this.searchSchematicTextBox.TextChanged += new System.EventHandler(this.searchSchematicTextBox_TextChanged);
            // 
            // searchVersionTextBox
            // 
            this.searchVersionTextBox.Location = new System.Drawing.Point(262, 36);
            this.searchVersionTextBox.Name = "searchVersionTextBox";
            this.searchVersionTextBox.Size = new System.Drawing.Size(234, 20);
            this.searchVersionTextBox.TabIndex = 5;
            this.searchVersionTextBox.TextChanged += new System.EventHandler(this.searchVersionTextBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 464);
            this.Controls.Add(this.searchVersionTextBox);
            this.Controls.Add(this.searchSchematicTextBox);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchSchematicTextBox;
        private System.Windows.Forms.TextBox searchVersionTextBox;
    }
}