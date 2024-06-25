namespace Presentation
{
    partial class SettingsForm
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
            goToProfilesButton = new Button();
            goToRefsetsButton = new Button();
            clearTempDataButton = new Button();
            dafFileServerButton = new Button();
            leylandFileServerButton = new Button();
            label1 = new Label();
            openCacheFolderButton = new Button();
            label2 = new Label();
            selectNewFolderButton = new Button();
            setCompareItLocationButton = new Button();
            SuspendLayout();
            // 
            // goToProfilesButton
            // 
            goToProfilesButton.Location = new Point(12, 12);
            goToProfilesButton.Name = "goToProfilesButton";
            goToProfilesButton.Size = new Size(75, 23);
            goToProfilesButton.TabIndex = 0;
            goToProfilesButton.Text = "Profiles";
            goToProfilesButton.UseVisualStyleBackColor = true;
            goToProfilesButton.Click += goToProfilesButton_Click;
            // 
            // goToRefsetsButton
            // 
            goToRefsetsButton.Location = new Point(93, 12);
            goToRefsetsButton.Name = "goToRefsetsButton";
            goToRefsetsButton.Size = new Size(75, 23);
            goToRefsetsButton.TabIndex = 1;
            goToRefsetsButton.Text = "Refsets";
            goToRefsetsButton.UseVisualStyleBackColor = true;
            goToRefsetsButton.Click += goToRefsetsButton_Click;
            // 
            // clearTempDataButton
            // 
            clearTempDataButton.Location = new Point(12, 282);
            clearTempDataButton.Name = "clearTempDataButton";
            clearTempDataButton.Size = new Size(96, 23);
            clearTempDataButton.TabIndex = 2;
            clearTempDataButton.Text = "Clear cache";
            clearTempDataButton.UseVisualStyleBackColor = true;
            clearTempDataButton.Click += clearTempDataButton_Click;
            // 
            // dafFileServerButton
            // 
            dafFileServerButton.Enabled = false;
            dafFileServerButton.Location = new Point(12, 108);
            dafFileServerButton.Name = "dafFileServerButton";
            dafFileServerButton.Size = new Size(75, 23);
            dafFileServerButton.TabIndex = 3;
            dafFileServerButton.Text = "DAF";
            dafFileServerButton.UseVisualStyleBackColor = true;
            dafFileServerButton.Click += dafFileServerButton_Click;
            // 
            // leylandFileServerButton
            // 
            leylandFileServerButton.Enabled = false;
            leylandFileServerButton.Location = new Point(12, 137);
            leylandFileServerButton.Name = "leylandFileServerButton";
            leylandFileServerButton.Size = new Size(75, 23);
            leylandFileServerButton.TabIndex = 4;
            leylandFileServerButton.Text = "Leyland";
            leylandFileServerButton.UseVisualStyleBackColor = true;
            leylandFileServerButton.Click += leylandFileServerButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 90);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 5;
            label1.Text = "Select server:";
            // 
            // openCacheFolderButton
            // 
            openCacheFolderButton.Location = new Point(12, 253);
            openCacheFolderButton.Name = "openCacheFolderButton";
            openCacheFolderButton.Size = new Size(121, 23);
            openCacheFolderButton.TabIndex = 6;
            openCacheFolderButton.Text = "Open cache folder";
            openCacheFolderButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 235);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 7;
            label2.Text = "Cache location:";
            // 
            // selectNewFolderButton
            // 
            selectNewFolderButton.Location = new Point(139, 253);
            selectNewFolderButton.Name = "selectNewFolderButton";
            selectNewFolderButton.Size = new Size(117, 23);
            selectNewFolderButton.TabIndex = 8;
            selectNewFolderButton.Text = "Select new folder";
            selectNewFolderButton.UseVisualStyleBackColor = true;
            selectNewFolderButton.Click += selectNewFolderButton_Click;
            // 
            // setCompareItLocationButton
            // 
            setCompareItLocationButton.Location = new Point(139, 108);
            setCompareItLocationButton.Name = "setCompareItLocationButton";
            setCompareItLocationButton.Size = new Size(145, 23);
            setCompareItLocationButton.TabIndex = 9;
            setCompareItLocationButton.Text = "Set CompareIt Location";
            setCompareItLocationButton.UseVisualStyleBackColor = true;
            setCompareItLocationButton.Click += setCompareItLocationButton_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 540);
            Controls.Add(setCompareItLocationButton);
            Controls.Add(selectNewFolderButton);
            Controls.Add(label2);
            Controls.Add(openCacheFolderButton);
            Controls.Add(label1);
            Controls.Add(leylandFileServerButton);
            Controls.Add(dafFileServerButton);
            Controls.Add(clearTempDataButton);
            Controls.Add(goToRefsetsButton);
            Controls.Add(goToProfilesButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SettingsForm";
            Text = "SettingsForm";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button goToProfilesButton;
        private Button goToRefsetsButton;
        private Button clearTempDataButton;
        private Button dafFileServerButton;
        private Button leylandFileServerButton;
        private Label label1;
        private Button openCacheFolderButton;
        private Label label2;
        private Button selectNewFolderButton;
        private Button setCompareItLocationButton;
    }
}