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
            saveProfileButton = new Button();
            profilesListBox = new ListBox();
            profileNameTextBox = new TextBox();
            profileTypeComboBox = new ComboBox();
            deleteProfileButton = new Button();
            addHeaderButton = new Button();
            removeHeaderButton = new Button();
            newProfileButton = new Button();
            SuspendLayout();
            // 
            // saveProfileButton
            // 
            saveProfileButton.Location = new Point(79, 45);
            saveProfileButton.Margin = new Padding(4, 3, 4, 3);
            saveProfileButton.Name = "saveProfileButton";
            saveProfileButton.Size = new Size(42, 27);
            saveProfileButton.TabIndex = 50;
            saveProfileButton.Text = "Save";
            saveProfileButton.UseVisualStyleBackColor = true;
            saveProfileButton.Click += saveProfileButton_Click;
            // 
            // profilesListBox
            // 
            profilesListBox.FormattingEnabled = true;
            profilesListBox.ItemHeight = 15;
            profilesListBox.Location = new Point(18, 108);
            profilesListBox.Margin = new Padding(4, 3, 4, 3);
            profilesListBox.Name = "profilesListBox";
            profilesListBox.Size = new Size(180, 274);
            profilesListBox.TabIndex = 51;
            profilesListBox.SelectedIndexChanged += profilesListBox_SelectedIndexChanged;
            // 
            // profileNameTextBox
            // 
            profileNameTextBox.Location = new Point(18, 78);
            profileNameTextBox.Margin = new Padding(4, 3, 4, 3);
            profileNameTextBox.Name = "profileNameTextBox";
            profileNameTextBox.Size = new Size(180, 23);
            profileNameTextBox.TabIndex = 52;
            // 
            // profileTypeComboBox
            // 
            profileTypeComboBox.FormattingEnabled = true;
            profileTypeComboBox.Location = new Point(18, 14);
            profileTypeComboBox.Margin = new Padding(4, 3, 4, 3);
            profileTypeComboBox.Name = "profileTypeComboBox";
            profileTypeComboBox.Size = new Size(180, 23);
            profileTypeComboBox.TabIndex = 53;
            profileTypeComboBox.SelectedIndexChanged += profileTypeComboBox_SelectedIndexChanged;
            // 
            // deleteProfileButton
            // 
            deleteProfileButton.Location = new Point(18, 45);
            deleteProfileButton.Margin = new Padding(4, 3, 4, 3);
            deleteProfileButton.Name = "deleteProfileButton";
            deleteProfileButton.Size = new Size(53, 27);
            deleteProfileButton.TabIndex = 54;
            deleteProfileButton.Text = "Delete";
            deleteProfileButton.UseVisualStyleBackColor = true;
            deleteProfileButton.Click += deleteProfileButton_Click;
            // 
            // addHeaderButton
            // 
            addHeaderButton.Location = new Point(237, 14);
            addHeaderButton.Margin = new Padding(4, 3, 4, 3);
            addHeaderButton.Name = "addHeaderButton";
            addHeaderButton.Size = new Size(23, 23);
            addHeaderButton.TabIndex = 55;
            addHeaderButton.Text = "+";
            addHeaderButton.UseVisualStyleBackColor = true;
            addHeaderButton.Click += addHeaderButton_Click;
            // 
            // removeHeaderButton
            // 
            removeHeaderButton.Enabled = false;
            removeHeaderButton.Location = new Point(206, 14);
            removeHeaderButton.Margin = new Padding(4, 3, 4, 3);
            removeHeaderButton.Name = "removeHeaderButton";
            removeHeaderButton.Size = new Size(23, 23);
            removeHeaderButton.TabIndex = 56;
            removeHeaderButton.Text = "-";
            removeHeaderButton.UseVisualStyleBackColor = true;
            removeHeaderButton.Click += removeHeaderButton_Click;
            // 
            // newProfileButton
            // 
            newProfileButton.Location = new Point(123, 45);
            newProfileButton.Name = "newProfileButton";
            newProfileButton.Size = new Size(75, 27);
            newProfileButton.TabIndex = 57;
            newProfileButton.Text = "New";
            newProfileButton.UseVisualStyleBackColor = true;
            newProfileButton.Click += newProfileButton_Click;
            // 
            // ProfileCreator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1018, 493);
            Controls.Add(newProfileButton);
            Controls.Add(removeHeaderButton);
            Controls.Add(addHeaderButton);
            Controls.Add(deleteProfileButton);
            Controls.Add(profileTypeComboBox);
            Controls.Add(profileNameTextBox);
            Controls.Add(profilesListBox);
            Controls.Add(saveProfileButton);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "ProfileCreator";
            Text = "F";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button saveProfileButton;
        private System.Windows.Forms.ListBox profilesListBox;
        private System.Windows.Forms.TextBox profileNameTextBox;
        private System.Windows.Forms.ComboBox profileTypeComboBox;
        private System.Windows.Forms.Button deleteProfileButton;
        private System.Windows.Forms.Button addHeaderButton;
        private System.Windows.Forms.Button removeHeaderButton;
        private Button newProfileButton;
    }
}