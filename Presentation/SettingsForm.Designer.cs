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
            SuspendLayout();
            // 
            // goToProfilesButton
            // 
            goToProfilesButton.Location = new Point(45, 76);
            goToProfilesButton.Name = "goToProfilesButton";
            goToProfilesButton.Size = new Size(75, 23);
            goToProfilesButton.TabIndex = 0;
            goToProfilesButton.Text = "Profiles";
            goToProfilesButton.UseVisualStyleBackColor = true;
            goToProfilesButton.Click += goToProfilesButton_Click;
            // 
            // goToRefsetsButton
            // 
            goToRefsetsButton.Location = new Point(155, 81);
            goToRefsetsButton.Name = "goToRefsetsButton";
            goToRefsetsButton.Size = new Size(75, 23);
            goToRefsetsButton.TabIndex = 1;
            goToRefsetsButton.Text = "Refsets";
            goToRefsetsButton.UseVisualStyleBackColor = true;
            goToRefsetsButton.Click += goToRefsetsButton_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 501);
            Controls.Add(goToRefsetsButton);
            Controls.Add(goToProfilesButton);
            Name = "SettingsForm";
            Text = "SettingsForm";
            ResumeLayout(false);
        }

        #endregion

        private Button goToProfilesButton;
        private Button goToRefsetsButton;
    }
}