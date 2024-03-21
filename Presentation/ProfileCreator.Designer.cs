namespace Presentation
{
    partial class ProfileCreator
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
            this.saveProfileButton = new System.Windows.Forms.Button();
            this.profilesListBox = new System.Windows.Forms.ListBox();
            this.profileNameTextBox = new System.Windows.Forms.TextBox();
            this.profileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.deleteProfileButton = new System.Windows.Forms.Button();
            this.addHeaderButton = new System.Windows.Forms.Button();
            this.removeHeaderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveProfileButton
            // 
            this.saveProfileButton.Location = new System.Drawing.Point(96, 39);
            this.saveProfileButton.Name = "saveProfileButton";
            this.saveProfileButton.Size = new System.Drawing.Size(75, 23);
            this.saveProfileButton.TabIndex = 50;
            this.saveProfileButton.Text = "Save";
            this.saveProfileButton.UseVisualStyleBackColor = true;
            this.saveProfileButton.Click += new System.EventHandler(this.saveProfileButton_Click);
            // 
            // profilesListBox
            // 
            this.profilesListBox.FormattingEnabled = true;
            this.profilesListBox.Location = new System.Drawing.Point(15, 87);
            this.profilesListBox.Name = "profilesListBox";
            this.profilesListBox.Size = new System.Drawing.Size(155, 238);
            this.profilesListBox.TabIndex = 51;
            this.profilesListBox.SelectedIndexChanged += new System.EventHandler(this.profilesListBox_SelectedIndexChanged);
            // 
            // profileNameTextBox
            // 
            this.profileNameTextBox.Location = new System.Drawing.Point(15, 61);
            this.profileNameTextBox.Name = "profileNameTextBox";
            this.profileNameTextBox.Size = new System.Drawing.Size(155, 20);
            this.profileNameTextBox.TabIndex = 52;
            // 
            // profileTypeComboBox
            // 
            this.profileTypeComboBox.FormattingEnabled = true;
            this.profileTypeComboBox.Location = new System.Drawing.Point(15, 12);
            this.profileTypeComboBox.Name = "profileTypeComboBox";
            this.profileTypeComboBox.Size = new System.Drawing.Size(155, 21);
            this.profileTypeComboBox.TabIndex = 53;
            this.profileTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.profileTypeComboBox_SelectedIndexChanged);
            // 
            // deleteProfileButton
            // 
            this.deleteProfileButton.Location = new System.Drawing.Point(15, 39);
            this.deleteProfileButton.Name = "deleteProfileButton";
            this.deleteProfileButton.Size = new System.Drawing.Size(75, 23);
            this.deleteProfileButton.TabIndex = 54;
            this.deleteProfileButton.Text = "Delete";
            this.deleteProfileButton.UseVisualStyleBackColor = true;
            this.deleteProfileButton.Click += new System.EventHandler(this.deleteProfileButton_Click);
            // 
            // addHeaderButton
            // 
            this.addHeaderButton.Location = new System.Drawing.Point(455, 45);
            this.addHeaderButton.Name = "addHeaderButton";
            this.addHeaderButton.Size = new System.Drawing.Size(26, 26);
            this.addHeaderButton.TabIndex = 55;
            this.addHeaderButton.Text = "+";
            this.addHeaderButton.UseVisualStyleBackColor = true;
            this.addHeaderButton.Click += new System.EventHandler(this.addHeaderButton_Click);
            // 
            // removeHeaderButton
            // 
            this.removeHeaderButton.Location = new System.Drawing.Point(415, 45);
            this.removeHeaderButton.Name = "removeHeaderButton";
            this.removeHeaderButton.Size = new System.Drawing.Size(26, 26);
            this.removeHeaderButton.TabIndex = 56;
            this.removeHeaderButton.Text = "-";
            this.removeHeaderButton.UseVisualStyleBackColor = true;
            this.removeHeaderButton.Click += new System.EventHandler(this.removeHeaderButton_Click);
            // 
            // ProfileCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 427);
            this.Controls.Add(this.removeHeaderButton);
            this.Controls.Add(this.addHeaderButton);
            this.Controls.Add(this.deleteProfileButton);
            this.Controls.Add(this.profileTypeComboBox);
            this.Controls.Add(this.profileNameTextBox);
            this.Controls.Add(this.profilesListBox);
            this.Controls.Add(this.saveProfileButton);
            this.Name = "ProfileCreator";
            this.Text = "F";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button saveProfileButton;
        private System.Windows.Forms.ListBox profilesListBox;
        private System.Windows.Forms.TextBox profileNameTextBox;
        private System.Windows.Forms.ComboBox profileTypeComboBox;
        private System.Windows.Forms.Button deleteProfileButton;
        private System.Windows.Forms.Button addHeaderButton;
        private System.Windows.Forms.Button removeHeaderButton;
    }
}