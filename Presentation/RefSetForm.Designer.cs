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
            bundleNumberTextBox = new TextBox();
            projectNameTextBox = new TextBox();
            descriptionTextBox = new TextBox();
            addReferenceButton = new Button();
            deleteReferenceButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // projectsListBox
            // 
            projectsListBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
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
            referencesListBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            referencesListBox.FormattingEnabled = true;
            referencesListBox.ItemHeight = 21;
            referencesListBox.Location = new Point(237, 21);
            referencesListBox.Name = "referencesListBox";
            referencesListBox.Size = new Size(515, 382);
            referencesListBox.TabIndex = 1;
            referencesListBox.DoubleClick += referencesListBox_DoubleClick;
            // 
            // yearWeekTextBox
            // 
            yearWeekTextBox.Location = new Point(12, 433);
            yearWeekTextBox.Name = "yearWeekTextBox";
            yearWeekTextBox.Size = new Size(140, 23);
            yearWeekTextBox.TabIndex = 2;
            // 
            // bundleNumberTextBox
            // 
            bundleNumberTextBox.Location = new Point(158, 433);
            bundleNumberTextBox.Name = "bundleNumberTextBox";
            bundleNumberTextBox.Size = new Size(140, 23);
            bundleNumberTextBox.TabIndex = 3;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 409);
            label1.Name = "label1";
            label1.Size = new Size(80, 21);
            label1.TabIndex = 8;
            label1.Text = "Year week";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(158, 409);
            label2.Name = "label2";
            label2.Size = new Size(112, 21);
            label2.TabIndex = 9;
            label2.Text = "Refset number";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(304, 409);
            label3.Name = "label3";
            label3.Size = new Size(101, 21);
            label3.TabIndex = 10;
            label3.Text = "Project name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(450, 409);
            label4.Name = "label4";
            label4.Size = new Size(89, 21);
            label4.TabIndex = 11;
            label4.Text = "Description";
            // 
            // RefSetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(781, 481);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(deleteReferenceButton);
            Controls.Add(addReferenceButton);
            Controls.Add(descriptionTextBox);
            Controls.Add(projectNameTextBox);
            Controls.Add(bundleNumberTextBox);
            Controls.Add(yearWeekTextBox);
            Controls.Add(referencesListBox);
            Controls.Add(projectsListBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RefSetForm";
            Text = "RefSetForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox projectsListBox;
        private ListBox referencesListBox;
        private TextBox yearWeekTextBox;
        private TextBox bundleNumberTextBox;
        private TextBox projectNameTextBox;
        private TextBox descriptionTextBox;
        private Button addReferenceButton;
        private Button deleteReferenceButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}