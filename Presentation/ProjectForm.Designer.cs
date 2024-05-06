namespace Presentation
{
    partial class ProjectForm
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
            returnToProjectsButton = new Button();
            currentProjectLabel = new Label();
            schematicsSearchTextBox = new TextBox();
            schematicsListBox = new ListBox();
            schematicsListLabel = new Label();
            SuspendLayout();
            // 
            // returnToProjectsButton
            // 
            returnToProjectsButton.Location = new Point(292, 86);
            returnToProjectsButton.Name = "returnToProjectsButton";
            returnToProjectsButton.Size = new Size(75, 23);
            returnToProjectsButton.TabIndex = 30;
            returnToProjectsButton.Text = "Back";
            returnToProjectsButton.UseVisualStyleBackColor = true;
            returnToProjectsButton.Click += returnToProjectsButton_Click;
            // 
            // currentProjectLabel
            // 
            currentProjectLabel.AutoSize = true;
            currentProjectLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            currentProjectLabel.Location = new Point(13, 86);
            currentProjectLabel.Name = "currentProjectLabel";
            currentProjectLabel.Size = new Size(100, 20);
            currentProjectLabel.TabIndex = 29;
            currentProjectLabel.Text = "Select project";
            // 
            // schematicsSearchTextBox
            // 
            schematicsSearchTextBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            schematicsSearchTextBox.Location = new Point(13, 115);
            schematicsSearchTextBox.Margin = new Padding(4, 3, 4, 3);
            schematicsSearchTextBox.Name = "schematicsSearchTextBox";
            schematicsSearchTextBox.Size = new Size(354, 23);
            schematicsSearchTextBox.TabIndex = 28;
            schematicsSearchTextBox.Text = "search:";
            schematicsSearchTextBox.Enter += schematicsSearchTextBox_Enter;
            schematicsSearchTextBox.Leave += schematicsSearchTextBox_Leave;
            // 
            // schematicsListBox
            // 
            schematicsListBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            schematicsListBox.FormattingEnabled = true;
            schematicsListBox.ItemHeight = 16;
            schematicsListBox.Location = new Point(13, 149);
            schematicsListBox.Margin = new Padding(4, 3, 4, 3);
            schematicsListBox.Name = "schematicsListBox";
            schematicsListBox.Size = new Size(354, 372);
            schematicsListBox.TabIndex = 27;
            schematicsListBox.Click += schematicsListBox_Click;
            // 
            // schematicsListLabel
            // 
            schematicsListLabel.AutoSize = true;
            schematicsListLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            schematicsListLabel.Location = new Point(13, 16);
            schematicsListLabel.Margin = new Padding(4, 0, 4, 0);
            schematicsListLabel.Name = "schematicsListLabel";
            schematicsListLabel.Size = new Size(95, 24);
            schematicsListLabel.TabIndex = 26;
            schematicsListLabel.Text = "Projects:";
            // 
            // ProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 540);
            Controls.Add(returnToProjectsButton);
            Controls.Add(currentProjectLabel);
            Controls.Add(schematicsSearchTextBox);
            Controls.Add(schematicsListBox);
            Controls.Add(schematicsListLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProjectForm";
            Text = "ProjectForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button returnToProjectsButton;
        private Label currentProjectLabel;
        private TextBox schematicsSearchTextBox;
        private ListBox schematicsListBox;
        private Label schematicsListLabel;
    }
}