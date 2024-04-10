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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Wires:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(165, 9);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 1;
            label2.Text = "Components:";
            // 
            // wireProfilesComboBox
            // 
            wireProfilesComboBox.FormattingEnabled = true;
            wireProfilesComboBox.Location = new Point(12, 27);
            wireProfilesComboBox.Name = "wireProfilesComboBox";
            wireProfilesComboBox.Size = new Size(121, 23);
            wireProfilesComboBox.TabIndex = 2;
            // 
            // componentProfilesComboBox
            // 
            componentProfilesComboBox.FormattingEnabled = true;
            componentProfilesComboBox.Location = new Point(165, 27);
            componentProfilesComboBox.Name = "componentProfilesComboBox";
            componentProfilesComboBox.Size = new Size(121, 23);
            componentProfilesComboBox.TabIndex = 3;
            // 
            // exportToExcelButton
            // 
            exportToExcelButton.Location = new Point(379, 27);
            exportToExcelButton.Name = "exportToExcelButton";
            exportToExcelButton.Size = new Size(75, 23);
            exportToExcelButton.TabIndex = 4;
            exportToExcelButton.Text = "Export";
            exportToExcelButton.UseVisualStyleBackColor = true;
            exportToExcelButton.Click += exportToExcelButton_Click;
            // 
            // ProfileChoiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}