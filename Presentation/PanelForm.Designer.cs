namespace Presentation
{
    partial class PanelForm
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
            panel = new Panel();
            versionLabel = new Label();
            label1 = new Label();
            programStatusButton = new Button();
            label2 = new Label();
            lastMessageTextBox = new TextBox();
            settingsButton = new Button();
            homeButton = new Button();
            SuspendLayout();
            // 
            // panel
            // 
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Location = new Point(12, 33);
            panel.Name = "panel";
            panel.Size = new Size(960, 540);
            panel.TabIndex = 0;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(863, 588);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(109, 15);
            versionLabel.TabIndex = 22;
            versionLabel.Text = "version: Alpha 0.1.2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(866, 8);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 24;
            label1.Text = "Messages:";
            // 
            // programStatusButton
            // 
            programStatusButton.Location = new Point(933, 3);
            programStatusButton.Name = "programStatusButton";
            programStatusButton.Size = new Size(43, 24);
            programStatusButton.TabIndex = 23;
            programStatusButton.UseVisualStyleBackColor = true;
            programStatusButton.Click += programStatusButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 588);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 26;
            label2.Text = "Status:";
            // 
            // lastMessageTextBox
            // 
            lastMessageTextBox.Location = new Point(54, 585);
            lastMessageTextBox.Name = "lastMessageTextBox";
            lastMessageTextBox.ReadOnly = true;
            lastMessageTextBox.Size = new Size(250, 23);
            lastMessageTextBox.TabIndex = 25;
            // 
            // settingsButton
            // 
            settingsButton.Location = new Point(93, 4);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(75, 23);
            settingsButton.TabIndex = 0;
            settingsButton.Text = "Settings";
            settingsButton.UseVisualStyleBackColor = true;
            settingsButton.Click += settingsButton_Click;
            // 
            // homeButton
            // 
            homeButton.Location = new Point(12, 4);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(75, 23);
            homeButton.TabIndex = 27;
            homeButton.Text = "Home";
            homeButton.UseVisualStyleBackColor = true;
            homeButton.Click += homeButton_Click;
            // 
            // PanelForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 612);
            Controls.Add(homeButton);
            Controls.Add(settingsButton);
            Controls.Add(label2);
            Controls.Add(lastMessageTextBox);
            Controls.Add(label1);
            Controls.Add(programStatusButton);
            Controls.Add(versionLabel);
            Controls.Add(panel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PanelForm";
            Text = "PanelForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel;
        private Label versionLabel;
        private Label label1;
        private Button programStatusButton;
        private Label label2;
        private TextBox lastMessageTextBox;
        private Button settingsButton;
        private Button homeButton;
    }
}