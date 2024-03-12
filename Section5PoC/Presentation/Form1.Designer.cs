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
            this.bundlesListLabel = new System.Windows.Forms.Label();
            this.bundlesListBox = new System.Windows.Forms.ListBox();
            this.searchBundlesTextBox = new System.Windows.Forms.TextBox();
            this.schematicsSearchTextBox = new System.Windows.Forms.TextBox();
            this.schematicsListBox = new System.Windows.Forms.ListBox();
            this.schematicsListLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bundlesListLabel
            // 
            this.bundlesListLabel.AutoSize = true;
            this.bundlesListLabel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.bundlesListLabel.Location = new System.Drawing.Point(23, 108);
            this.bundlesListLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.bundlesListLabel.Name = "bundlesListLabel";
            this.bundlesListLabel.Size = new System.Drawing.Size(94, 24);
            this.bundlesListLabel.TabIndex = 0;
            this.bundlesListLabel.Text = "Bundels:";
            // 
            // bundlesListBox
            // 
            this.bundlesListBox.Font = new System.Drawing.Font("Arial", 10F);
            this.bundlesListBox.FormattingEnabled = true;
            this.bundlesListBox.ItemHeight = 16;
            this.bundlesListBox.Location = new System.Drawing.Point(28, 177);
            this.bundlesListBox.Margin = new System.Windows.Forms.Padding(4);
            this.bundlesListBox.Name = "bundlesListBox";
            this.bundlesListBox.Size = new System.Drawing.Size(311, 452);
            this.bundlesListBox.TabIndex = 1;
            this.bundlesListBox.DoubleClick += new System.EventHandler(this.schematicsListBox_DoubleClick_1);
            // 
            // searchBundlesTextBox
            // 
            this.searchBundlesTextBox.Font = new System.Drawing.Font("Arial", 10F);
            this.searchBundlesTextBox.Location = new System.Drawing.Point(28, 142);
            this.searchBundlesTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.searchBundlesTextBox.Name = "searchBundlesTextBox";
            this.searchBundlesTextBox.Size = new System.Drawing.Size(311, 23);
            this.searchBundlesTextBox.TabIndex = 4;
            this.searchBundlesTextBox.Text = "search:";
            this.searchBundlesTextBox.TextChanged += new System.EventHandler(this.searchBundlesTextBox_TextChanged);
            this.searchBundlesTextBox.Enter += new System.EventHandler(this.searchBundlesTextBox_Enter);
            this.searchBundlesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBundlesTextBox_KeyDown);
            this.searchBundlesTextBox.Leave += new System.EventHandler(this.searchBundlesTextBox_Leave);
            // 
            // schematicsSearchTextBox
            // 
            this.schematicsSearchTextBox.Font = new System.Drawing.Font("Arial", 10F);
            this.schematicsSearchTextBox.Location = new System.Drawing.Point(477, 142);
            this.schematicsSearchTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.schematicsSearchTextBox.Name = "schematicsSearchTextBox";
            this.schematicsSearchTextBox.Size = new System.Drawing.Size(311, 23);
            this.schematicsSearchTextBox.TabIndex = 7;
            this.schematicsSearchTextBox.Text = "search:";
            this.schematicsSearchTextBox.TextChanged += new System.EventHandler(this.schematicsSearchTextBox_TextChanged);
            // 
            // schematicsListBox
            // 
            this.schematicsListBox.Font = new System.Drawing.Font("Arial", 10F);
            this.schematicsListBox.FormattingEnabled = true;
            this.schematicsListBox.ItemHeight = 16;
            this.schematicsListBox.Location = new System.Drawing.Point(477, 177);
            this.schematicsListBox.Margin = new System.Windows.Forms.Padding(4);
            this.schematicsListBox.Name = "schematicsListBox";
            this.schematicsListBox.Size = new System.Drawing.Size(311, 452);
            this.schematicsListBox.TabIndex = 6;
            this.schematicsListBox.DoubleClick += new System.EventHandler(this.schematicsListBox_DoubleClick);
            // 
            // schematicsListLabel
            // 
            this.schematicsListLabel.AutoSize = true;
            this.schematicsListLabel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.schematicsListLabel.Location = new System.Drawing.Point(472, 108);
            this.schematicsListLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.schematicsListLabel.Name = "schematicsListLabel";
            this.schematicsListLabel.Size = new System.Drawing.Size(95, 24);
            this.schematicsListLabel.TabIndex = 5;
            this.schematicsListLabel.Text = "Projects:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 661);
            this.Controls.Add(this.schematicsSearchTextBox);
            this.Controls.Add(this.schematicsListBox);
            this.Controls.Add(this.schematicsListLabel);
            this.Controls.Add(this.searchBundlesTextBox);
            this.Controls.Add(this.bundlesListBox);
            this.Controls.Add(this.bundlesListLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label bundlesListLabel;
        private System.Windows.Forms.ListBox bundlesListBox;
        private System.Windows.Forms.TextBox searchBundlesTextBox;
        private System.Windows.Forms.TextBox schematicsSearchTextBox;
        private System.Windows.Forms.ListBox schematicsListBox;
        private System.Windows.Forms.Label schematicsListLabel;
    }
}