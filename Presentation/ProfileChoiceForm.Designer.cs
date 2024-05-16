namespace Presentation
{
    partial class ProfileChoiceForm
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
            label1 = new Label();
            label2 = new Label();
            wireProfilesComboBox = new ComboBox();
            componentProfilesComboBox = new ComboBox();
            exportToExcelButton = new Button();
            exportProjectButton = new Button();
            bundlesListBox = new ListBox();
            selectAllBundlesButton = new Button();
            selectNoneBundlesButton = new Button();
            createPECheckBox = new CheckBox();
            createRCCheckBox = new CheckBox();
            createOCCheckBox = new CheckBox();
            currentlyOpenedLabel = new Label();
            saberCheckerButton = new Button();
            testResultsTextBox = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 66);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Wires:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(165, 66);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 1;
            label2.Text = "Components:";
            // 
            // wireProfilesComboBox
            // 
            wireProfilesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            wireProfilesComboBox.FormattingEnabled = true;
            wireProfilesComboBox.Location = new Point(12, 84);
            wireProfilesComboBox.Name = "wireProfilesComboBox";
            wireProfilesComboBox.Size = new Size(121, 23);
            wireProfilesComboBox.TabIndex = 2;
            // 
            // componentProfilesComboBox
            // 
            componentProfilesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            componentProfilesComboBox.FormattingEnabled = true;
            componentProfilesComboBox.Location = new Point(165, 84);
            componentProfilesComboBox.Name = "componentProfilesComboBox";
            componentProfilesComboBox.Size = new Size(121, 23);
            componentProfilesComboBox.TabIndex = 3;
            // 
            // exportToExcelButton
            // 
            exportToExcelButton.Location = new Point(12, 113);
            exportToExcelButton.Name = "exportToExcelButton";
            exportToExcelButton.Size = new Size(75, 23);
            exportToExcelButton.TabIndex = 4;
            exportToExcelButton.Text = "Export";
            exportToExcelButton.UseVisualStyleBackColor = true;
            exportToExcelButton.Click += exportToExcelButton_Click;
            // 
            // exportProjectButton
            // 
            exportProjectButton.Location = new Point(93, 113);
            exportProjectButton.Name = "exportProjectButton";
            exportProjectButton.Size = new Size(101, 23);
            exportProjectButton.TabIndex = 5;
            exportProjectButton.Text = "ExportProject";
            exportProjectButton.UseVisualStyleBackColor = true;
            exportProjectButton.Click += exportProjectButton_Click;
            // 
            // bundlesListBox
            // 
            bundlesListBox.FormattingEnabled = true;
            bundlesListBox.ItemHeight = 15;
            bundlesListBox.Location = new Point(13, 171);
            bundlesListBox.Name = "bundlesListBox";
            bundlesListBox.SelectionMode = SelectionMode.MultiSimple;
            bundlesListBox.Size = new Size(120, 199);
            bundlesListBox.TabIndex = 6;
            // 
            // selectAllBundlesButton
            // 
            selectAllBundlesButton.Location = new Point(12, 142);
            selectAllBundlesButton.Name = "selectAllBundlesButton";
            selectAllBundlesButton.Size = new Size(121, 23);
            selectAllBundlesButton.TabIndex = 7;
            selectAllBundlesButton.Text = "Select all";
            selectAllBundlesButton.UseVisualStyleBackColor = true;
            selectAllBundlesButton.Click += selectAllBundlesButton_Click;
            // 
            // selectNoneBundlesButton
            // 
            selectNoneBundlesButton.Location = new Point(12, 376);
            selectNoneBundlesButton.Name = "selectNoneBundlesButton";
            selectNoneBundlesButton.Size = new Size(121, 23);
            selectNoneBundlesButton.TabIndex = 8;
            selectNoneBundlesButton.Text = "Select none";
            selectNoneBundlesButton.UseVisualStyleBackColor = true;
            selectNoneBundlesButton.Click += selectNoneBundlesButton_Click;
            // 
            // createPECheckBox
            // 
            createPECheckBox.AutoSize = true;
            createPECheckBox.Location = new Point(139, 146);
            createPECheckBox.Name = "createPECheckBox";
            createPECheckBox.Size = new Size(39, 19);
            createPECheckBox.TabIndex = 9;
            createPECheckBox.Text = "PE";
            createPECheckBox.UseVisualStyleBackColor = true;
            // 
            // createRCCheckBox
            // 
            createRCCheckBox.AutoSize = true;
            createRCCheckBox.Location = new Point(139, 171);
            createRCCheckBox.Name = "createRCCheckBox";
            createRCCheckBox.Size = new Size(41, 19);
            createRCCheckBox.TabIndex = 10;
            createRCCheckBox.Text = "RC";
            createRCCheckBox.UseVisualStyleBackColor = true;
            // 
            // createOCCheckBox
            // 
            createOCCheckBox.AutoSize = true;
            createOCCheckBox.Location = new Point(139, 196);
            createOCCheckBox.Name = "createOCCheckBox";
            createOCCheckBox.Size = new Size(43, 19);
            createOCCheckBox.TabIndex = 11;
            createOCCheckBox.Text = "OC";
            createOCCheckBox.UseVisualStyleBackColor = true;
            // 
            // currentlyOpenedLabel
            // 
            currentlyOpenedLabel.AutoSize = true;
            currentlyOpenedLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            currentlyOpenedLabel.Location = new Point(12, 9);
            currentlyOpenedLabel.Margin = new Padding(4, 0, 4, 0);
            currentlyOpenedLabel.Name = "currentlyOpenedLabel";
            currentlyOpenedLabel.Size = new Size(91, 24);
            currentlyOpenedLabel.TabIndex = 15;
            currentlyOpenedLabel.Text = "Opened:";
            // 
            // saberCheckerButton
            // 
            saberCheckerButton.Location = new Point(165, 347);
            saberCheckerButton.Name = "saberCheckerButton";
            saberCheckerButton.Size = new Size(110, 23);
            saberCheckerButton.TabIndex = 16;
            saberCheckerButton.Text = "Perform checks";
            saberCheckerButton.UseVisualStyleBackColor = true;
            saberCheckerButton.Click += saberCheckerButton_Click;
            // 
            // testResultsTextBox
            // 
            testResultsTextBox.Location = new Point(165, 377);
            testResultsTextBox.Name = "testResultsTextBox";
            testResultsTextBox.ReadOnly = true;
            testResultsTextBox.Size = new Size(110, 23);
            testResultsTextBox.TabIndex = 17;
            // 
            // ProfileChoiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 540);
            Controls.Add(testResultsTextBox);
            Controls.Add(saberCheckerButton);
            Controls.Add(currentlyOpenedLabel);
            Controls.Add(createOCCheckBox);
            Controls.Add(createRCCheckBox);
            Controls.Add(createPECheckBox);
            Controls.Add(selectNoneBundlesButton);
            Controls.Add(selectAllBundlesButton);
            Controls.Add(bundlesListBox);
            Controls.Add(exportProjectButton);
            Controls.Add(exportToExcelButton);
            Controls.Add(componentProfilesComboBox);
            Controls.Add(wireProfilesComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProfileChoiceForm";
            Text = "ProfileChoiceForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox wireProfilesComboBox;
        private ComboBox componentProfilesComboBox;
        private Button exportToExcelButton;
        private Button exportProjectButton;
        private ListBox bundlesListBox;
        private Button selectAllBundlesButton;
        private Button selectNoneBundlesButton;
        private CheckBox createPECheckBox;
        private CheckBox createRCCheckBox;
        private CheckBox createOCCheckBox;
        private Label currentlyOpenedLabel;
        private Button saberCheckerButton;
        private TextBox testResultsTextBox;
    }
}