namespace Presentation
{
    partial class RefSetForm
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
            projectsListBox = new ListBox();
            referencesListBox = new ListBox();
            yearWeekTextBox = new TextBox();
            specNumberTextBox = new TextBox();
            projectNameTextBox = new TextBox();
            descriptionTextBox = new TextBox();
            addReferenceButton = new Button();
            deleteReferenceButton = new Button();
            SuspendLayout();
            // 
            // projectsListBox
            // 
            projectsListBox.Font = new Font("Segoe UI", 12F);
            projectsListBox.FormattingEnabled = true;
            projectsListBox.ItemHeight = 21;
            projectsListBox.Location = new Point(12, 21);
            projectsListBox.Name = "projectsListBox";
            projectsListBox.Size = new Size(200, 382);
            projectsListBox.TabIndex = 0;
            projectsListBox.DoubleClick += projectsListBox_DoubleClick;
            // 
            // referencesListBox
            // 
            referencesListBox.Font = new Font("Segoe UI", 12F);
            referencesListBox.FormattingEnabled = true;
            referencesListBox.ItemHeight = 21;
            referencesListBox.Location = new Point(237, 21);
            referencesListBox.Name = "referencesListBox";
            referencesListBox.Size = new Size(515, 382);
            referencesListBox.TabIndex = 1;
            // 
            // yearWeekTextBox
            // 
            yearWeekTextBox.Location = new Point(12, 433);
            yearWeekTextBox.Name = "yearWeekTextBox";
            yearWeekTextBox.Size = new Size(140, 23);
            yearWeekTextBox.TabIndex = 2;
            // 
            // specNumberTextBox
            // 
            specNumberTextBox.Location = new Point(158, 433);
            specNumberTextBox.Name = "specNumberTextBox";
            specNumberTextBox.Size = new Size(140, 23);
            specNumberTextBox.TabIndex = 3;
            // 
            // projectNameTextBox
            // 
            projectNameTextBox.Location = new Point(304, 433);
            projectNameTextBox.Name = "projectNameTextBox";
            projectNameTextBox.Size = new Size(140, 23);
            projectNameTextBox.TabIndex = 4;
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Location = new Point(450, 433);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.Size = new Size(140, 23);
            descriptionTextBox.TabIndex = 5;
            // 
            // addReferenceButton
            // 
            addReferenceButton.Location = new Point(596, 432);
            addReferenceButton.Name = "addReferenceButton";
            addReferenceButton.Size = new Size(75, 24);
            addReferenceButton.TabIndex = 6;
            addReferenceButton.Text = "Add";
            addReferenceButton.UseVisualStyleBackColor = true;
            addReferenceButton.Click += addReferenceButton_Click;
            // 
            // deleteReferenceButton
            // 
            deleteReferenceButton.Location = new Point(677, 432);
            deleteReferenceButton.Name = "deleteReferenceButton";
            deleteReferenceButton.Size = new Size(75, 24);
            deleteReferenceButton.TabIndex = 7;
            deleteReferenceButton.Text = "Delete";
            deleteReferenceButton.UseVisualStyleBackColor = true;
            deleteReferenceButton.Click += deleteReferenceButton_Click;
            // 
            // RefSetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(781, 481);
            Controls.Add(deleteReferenceButton);
            Controls.Add(addReferenceButton);
            Controls.Add(descriptionTextBox);
            Controls.Add(projectNameTextBox);
            Controls.Add(specNumberTextBox);
            Controls.Add(yearWeekTextBox);
            Controls.Add(referencesListBox);
            Controls.Add(projectsListBox);
            Name = "RefSetForm";
            Text = "RefSetForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox projectsListBox;
        private ListBox referencesListBox;
        private TextBox yearWeekTextBox;
        private TextBox specNumberTextBox;
        private TextBox projectNameTextBox;
        private TextBox descriptionTextBox;
        private Button addReferenceButton;
        private Button deleteReferenceButton;
    }
}