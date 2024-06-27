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
            bundlesListBox = new ListBox();
            selectAllBundlesButton = new Button();
            selectNoneBundlesButton = new Button();
            createPECheckBox = new CheckBox();
            createRCCheckBox = new CheckBox();
            createOCCheckBox = new CheckBox();
            currentlyOpenedLabel = new Label();
            saberCheckerButton = new Button();
            testResultsTextBox = new TextBox();
            customSheetComboBox = new ComboBox();
            createCustomCheckBox = new CheckBox();
            releaseBundleButton = new Button();
            checkDataButton = new Button();
            label3 = new Label();
            label4 = new Label();
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
            // bundlesListBox
            // 
            bundlesListBox.FormattingEnabled = true;
            bundlesListBox.ItemHeight = 15;
            bundlesListBox.Location = new Point(13, 171);
            bundlesListBox.Name = "bundlesListBox";
            bundlesListBox.SelectionMode = SelectionMode.MultiSimple;
            bundlesListBox.Size = new Size(120, 229);
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
            selectNoneBundlesButton.Location = new Point(12, 409);
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
            saberCheckerButton.Location = new Point(180, 307);
            saberCheckerButton.Name = "saberCheckerButton";
            saberCheckerButton.Size = new Size(130, 23);
            saberCheckerButton.TabIndex = 16;
            saberCheckerButton.Text = "Perform DRC ";
            saberCheckerButton.UseVisualStyleBackColor = true;
            saberCheckerButton.Click += saberCheckerButton_Click;
            // 
            // testResultsTextBox
            // 
            testResultsTextBox.Location = new Point(180, 336);
            testResultsTextBox.Name = "testResultsTextBox";
            testResultsTextBox.ReadOnly = true;
            testResultsTextBox.Size = new Size(130, 23);
            testResultsTextBox.TabIndex = 17;
            // 
            // customSheetComboBox
            // 
            customSheetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            customSheetComboBox.Enabled = false;
            customSheetComboBox.FormattingEnabled = true;
            customSheetComboBox.Location = new Point(213, 221);
            customSheetComboBox.Name = "customSheetComboBox";
            customSheetComboBox.Size = new Size(130, 23);
            customSheetComboBox.TabIndex = 18;
            // 
            // createCustomCheckBox
            // 
            createCustomCheckBox.AutoSize = true;
            createCustomCheckBox.Location = new Point(139, 221);
            createCustomCheckBox.Name = "createCustomCheckBox";
            createCustomCheckBox.Size = new Size(68, 19);
            createCustomCheckBox.TabIndex = 19;
            createCustomCheckBox.Text = "Custom";
            createCustomCheckBox.UseVisualStyleBackColor = true;
            createCustomCheckBox.CheckedChanged += createCustomCheckBox_CheckedChanged;
            // 
            // releaseBundleButton
            // 
            releaseBundleButton.Enabled = false;
            releaseBundleButton.Location = new Point(180, 409);
            releaseBundleButton.Name = "releaseBundleButton";
            releaseBundleButton.Size = new Size(163, 23);
            releaseBundleButton.TabIndex = 20;
            releaseBundleButton.Text = "Transfer to release portal";
            releaseBundleButton.UseVisualStyleBackColor = true;
            releaseBundleButton.Click += releaseBundleButton_Click;
            // 
            // checkDataButton
            // 
            checkDataButton.Location = new Point(180, 380);
            checkDataButton.Name = "checkDataButton";
            checkDataButton.Size = new Size(163, 23);
            checkDataButton.TabIndex = 21;
            checkDataButton.Text = "Check before release";
            checkDataButton.UseVisualStyleBackColor = true;
            checkDataButton.Click += checkDataButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(180, 362);
            label3.Name = "label3";
            label3.Size = new Size(84, 15);
            label3.TabIndex = 22;
            label3.Text = "Releasing files:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(180, 289);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 23;
            label4.Text = "Checking integrity:";
            // 
            // ProfileChoiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 540);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(checkDataButton);
            Controls.Add(releaseBundleButton);
            Controls.Add(createCustomCheckBox);
            Controls.Add(customSheetComboBox);
            Controls.Add(testResultsTextBox);
            Controls.Add(saberCheckerButton);
            Controls.Add(currentlyOpenedLabel);
            Controls.Add(createOCCheckBox);
            Controls.Add(createRCCheckBox);
            Controls.Add(createPECheckBox);
            Controls.Add(selectNoneBundlesButton);
            Controls.Add(selectAllBundlesButton);
            Controls.Add(bundlesListBox);
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
        private ListBox bundlesListBox;
        private Button selectAllBundlesButton;
        private Button selectNoneBundlesButton;
        private CheckBox createPECheckBox;
        private CheckBox createRCCheckBox;
        private CheckBox createOCCheckBox;
        private Label currentlyOpenedLabel;
        private Button saberCheckerButton;
        private TextBox testResultsTextBox;
        private ComboBox customSheetComboBox;
        private CheckBox createCustomCheckBox;
        private Button releaseBundleButton;
        private Button checkDataButton;
        private Label label3;
        private Label label4;
    }
}