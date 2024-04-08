namespace Presentation
{
    partial class MessageViewer
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
            messagesListBox = new ListBox();
            SuspendLayout();
            // 
            // messagesListBox
            // 
            messagesListBox.FormattingEnabled = true;
            messagesListBox.HorizontalScrollbar = true;
            messagesListBox.ItemHeight = 15;
            messagesListBox.Location = new Point(12, 12);
            messagesListBox.Name = "messagesListBox";
            messagesListBox.ScrollAlwaysVisible = true;
            messagesListBox.Size = new Size(552, 634);
            messagesListBox.TabIndex = 0;
            // 
            // MessageViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 655);
            Controls.Add(messagesListBox);
            Name = "MessageViewer";
            Text = "MessageViewer";
            Resize += MessageViewer_Resize;
            ResumeLayout(false);
        }

        #endregion

        private ListBox messagesListBox;
    }
}