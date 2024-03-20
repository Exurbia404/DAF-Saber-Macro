namespace Presentation
{
    partial class MainForm
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
            this.productProtoButton = new System.Windows.Forms.Button();
            this.reldasButton = new System.Windows.Forms.Button();
            this.designerButton = new System.Windows.Forms.Button();
            this.wipButton = new System.Windows.Forms.Button();
            this.releasedButton = new System.Windows.Forms.Button();
            this.goToProfilesButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bundlesListLabel
            // 
            this.bundlesListLabel.AutoSize = true;
            this.bundlesListLabel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.bundlesListLabel.Location = new System.Drawing.Point(9, 76);
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
            this.bundlesListBox.Location = new System.Drawing.Point(12, 175);
            this.bundlesListBox.Name = "bundlesListBox";
            this.bundlesListBox.Size = new System.Drawing.Size(251, 340);
            this.bundlesListBox.TabIndex = 1;
            this.bundlesListBox.DoubleClick += new System.EventHandler(this.bundlesListBox_DoubleClick);
            // 
            // searchBundlesTextBox
            // 
            this.searchBundlesTextBox.Font = new System.Drawing.Font("Arial", 10F);
            this.searchBundlesTextBox.Location = new System.Drawing.Point(12, 146);
            this.searchBundlesTextBox.Name = "searchBundlesTextBox";
            this.searchBundlesTextBox.Size = new System.Drawing.Size(251, 23);
            this.searchBundlesTextBox.TabIndex = 4;
            this.searchBundlesTextBox.Text = "search:";
            this.searchBundlesTextBox.Enter += new System.EventHandler(this.searchBundlesTextBox_Enter);
            this.searchBundlesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBundlesTextBox_KeyDown);
            this.searchBundlesTextBox.Leave += new System.EventHandler(this.searchBundlesTextBox_Leave);
            // 
            // schematicsSearchTextBox
            // 
            this.schematicsSearchTextBox.Font = new System.Drawing.Font("Arial", 10F);
            this.schematicsSearchTextBox.Location = new System.Drawing.Point(349, 146);
            this.schematicsSearchTextBox.Name = "schematicsSearchTextBox";
            this.schematicsSearchTextBox.Size = new System.Drawing.Size(251, 23);
            this.schematicsSearchTextBox.TabIndex = 7;
            this.schematicsSearchTextBox.Text = "search:";
            this.schematicsSearchTextBox.Enter += new System.EventHandler(this.schematicsSearchTextBox_Enter);
            this.schematicsSearchTextBox.Leave += new System.EventHandler(this.schematicsSearchTextBox_Leave);
            // 
            // schematicsListBox
            // 
            this.schematicsListBox.Font = new System.Drawing.Font("Arial", 10F);
            this.schematicsListBox.FormattingEnabled = true;
            this.schematicsListBox.ItemHeight = 16;
            this.schematicsListBox.Location = new System.Drawing.Point(349, 175);
            this.schematicsListBox.Name = "schematicsListBox";
            this.schematicsListBox.Size = new System.Drawing.Size(251, 340);
            this.schematicsListBox.TabIndex = 6;
            this.schematicsListBox.DoubleClick += new System.EventHandler(this.schematicsListBox_DoubleClick);
            // 
            // schematicsListLabel
            // 
            this.schematicsListLabel.AutoSize = true;
            this.schematicsListLabel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.schematicsListLabel.Location = new System.Drawing.Point(346, 76);
            this.schematicsListLabel.Name = "schematicsListLabel";
            this.schematicsListLabel.Size = new System.Drawing.Size(95, 24);
            this.schematicsListLabel.TabIndex = 5;
            this.schematicsListLabel.Text = "Projects:";
            // 
            // productProtoButton
            // 
            this.productProtoButton.BackColor = System.Drawing.Color.Gray;
            this.productProtoButton.Location = new System.Drawing.Point(12, 122);
            this.productProtoButton.Margin = new System.Windows.Forms.Padding(2);
            this.productProtoButton.Name = "productProtoButton";
            this.productProtoButton.Size = new System.Drawing.Size(83, 20);
            this.productProtoButton.TabIndex = 8;
            this.productProtoButton.Text = "Production/Proto";
            this.productProtoButton.UseVisualStyleBackColor = false;
            this.productProtoButton.Click += new System.EventHandler(this.productProtoButton_Click);
            // 
            // reldasButton
            // 
            this.reldasButton.BackColor = System.Drawing.Color.Gray;
            this.reldasButton.Location = new System.Drawing.Point(95, 122);
            this.reldasButton.Margin = new System.Windows.Forms.Padding(2);
            this.reldasButton.Name = "reldasButton";
            this.reldasButton.Size = new System.Drawing.Size(83, 20);
            this.reldasButton.TabIndex = 9;
            this.reldasButton.Text = "Release portal";
            this.reldasButton.UseVisualStyleBackColor = false;
            this.reldasButton.Click += new System.EventHandler(this.reldasButton_Click);
            // 
            // designerButton
            // 
            this.designerButton.BackColor = System.Drawing.Color.White;
            this.designerButton.Location = new System.Drawing.Point(179, 122);
            this.designerButton.Margin = new System.Windows.Forms.Padding(2);
            this.designerButton.Name = "designerButton";
            this.designerButton.Size = new System.Drawing.Size(83, 20);
            this.designerButton.TabIndex = 10;
            this.designerButton.Text = "WiP";
            this.designerButton.UseVisualStyleBackColor = false;
            this.designerButton.Click += new System.EventHandler(this.designerButton_Click);
            // 
            // wipButton
            // 
            this.wipButton.BackColor = System.Drawing.Color.Gray;
            this.wipButton.Enabled = false;
            this.wipButton.Location = new System.Drawing.Point(432, 122);
            this.wipButton.Margin = new System.Windows.Forms.Padding(2);
            this.wipButton.Name = "wipButton";
            this.wipButton.Size = new System.Drawing.Size(83, 20);
            this.wipButton.TabIndex = 12;
            this.wipButton.Text = "WiP";
            this.wipButton.UseVisualStyleBackColor = false;
            this.wipButton.Click += new System.EventHandler(this.wipButton_Click);
            // 
            // releasedButton
            // 
            this.releasedButton.BackColor = System.Drawing.Color.White;
            this.releasedButton.Location = new System.Drawing.Point(349, 122);
            this.releasedButton.Margin = new System.Windows.Forms.Padding(2);
            this.releasedButton.Name = "releasedButton";
            this.releasedButton.Size = new System.Drawing.Size(83, 20);
            this.releasedButton.TabIndex = 11;
            this.releasedButton.Text = "Released";
            this.releasedButton.UseVisualStyleBackColor = false;
            this.releasedButton.Click += new System.EventHandler(this.releasedButton_Click);
            // 
            // goToProfilesButton
            // 
            this.goToProfilesButton.Location = new System.Drawing.Point(12, 530);
            this.goToProfilesButton.Name = "goToProfilesButton";
            this.goToProfilesButton.Size = new System.Drawing.Size(75, 23);
            this.goToProfilesButton.TabIndex = 13;
            this.goToProfilesButton.Text = "Profiles";
            this.goToProfilesButton.UseVisualStyleBackColor = true;
            this.goToProfilesButton.Click += new System.EventHandler(this.goToProfilesButton_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 14;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(93, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 15;
            this.button2.Text = "Release";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(174, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 16;
            this.button3.Text = "Compare";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 568);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.goToProfilesButton);
            this.Controls.Add(this.wipButton);
            this.Controls.Add(this.releasedButton);
            this.Controls.Add(this.designerButton);
            this.Controls.Add(this.reldasButton);
            this.Controls.Add(this.productProtoButton);
            this.Controls.Add(this.schematicsSearchTextBox);
            this.Controls.Add(this.schematicsListBox);
            this.Controls.Add(this.schematicsListLabel);
            this.Controls.Add(this.searchBundlesTextBox);
            this.Controls.Add(this.bundlesListBox);
            this.Controls.Add(this.bundlesListLabel);
            this.Name = "MainForm";
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
        private System.Windows.Forms.Button productProtoButton;
        private System.Windows.Forms.Button reldasButton;
        private System.Windows.Forms.Button designerButton;
        private System.Windows.Forms.Button wipButton;
        private System.Windows.Forms.Button releasedButton;
        private System.Windows.Forms.Button goToProfilesButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}