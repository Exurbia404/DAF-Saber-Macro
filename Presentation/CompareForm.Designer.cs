namespace Presentation
{
    partial class CompareForm
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
            b1WiPButton = new Button();
            b1ReleasePortalButton = new Button();
            b1ReleasedButton = new Button();
            bundle1TextBox = new TextBox();
            b2WiPButton = new Button();
            b2ReleasePortalButton = new Button();
            b2ReleasedButton = new Button();
            bundle2TextBox = new TextBox();
            bundlesListLabel = new Label();
            label1 = new Label();
            compareButton = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // b1WiPButton
            // 
            b1WiPButton.BackColor = Color.White;
            b1WiPButton.Location = new Point(209, 91);
            b1WiPButton.Margin = new Padding(2);
            b1WiPButton.Name = "b1WiPButton";
            b1WiPButton.Size = new Size(97, 23);
            b1WiPButton.TabIndex = 17;
            b1WiPButton.Text = "WiP";
            b1WiPButton.UseVisualStyleBackColor = false;
            b1WiPButton.Click += b1WiPButton_Click;
            // 
            // b1ReleasePortalButton
            // 
            b1ReleasePortalButton.BackColor = Color.Gray;
            b1ReleasePortalButton.Location = new Point(111, 91);
            b1ReleasePortalButton.Margin = new Padding(2);
            b1ReleasePortalButton.Name = "b1ReleasePortalButton";
            b1ReleasePortalButton.Size = new Size(97, 23);
            b1ReleasePortalButton.TabIndex = 16;
            b1ReleasePortalButton.Text = "Release portal";
            b1ReleasePortalButton.UseVisualStyleBackColor = false;
            b1ReleasePortalButton.Click += b1ReleaseButton_Click;
            // 
            // b1ReleasedButton
            // 
            b1ReleasedButton.BackColor = Color.Gray;
            b1ReleasedButton.Location = new Point(14, 91);
            b1ReleasedButton.Margin = new Padding(2);
            b1ReleasedButton.Name = "b1ReleasedButton";
            b1ReleasedButton.Size = new Size(97, 23);
            b1ReleasedButton.TabIndex = 15;
            b1ReleasedButton.Text = "Released";
            b1ReleasedButton.UseVisualStyleBackColor = false;
            b1ReleasedButton.Click += b1ReleasedButton_Click;
            // 
            // bundle1TextBox
            // 
            bundle1TextBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            bundle1TextBox.Location = new Point(16, 119);
            bundle1TextBox.Margin = new Padding(4, 3, 4, 3);
            bundle1TextBox.Name = "bundle1TextBox";
            bundle1TextBox.Size = new Size(292, 23);
            bundle1TextBox.TabIndex = 14;
            bundle1TextBox.Text = "search:";
            bundle1TextBox.Enter += bundle1TextBox_Enter;
            bundle1TextBox.Leave += bundle1TextBox_Leave;
            // 
            // b2WiPButton
            // 
            b2WiPButton.BackColor = Color.White;
            b2WiPButton.Location = new Point(211, 224);
            b2WiPButton.Margin = new Padding(2);
            b2WiPButton.Name = "b2WiPButton";
            b2WiPButton.Size = new Size(97, 23);
            b2WiPButton.TabIndex = 21;
            b2WiPButton.Text = "WiP";
            b2WiPButton.UseVisualStyleBackColor = false;
            b2WiPButton.Click += b2WiPButton_Click;
            // 
            // b2ReleasePortalButton
            // 
            b2ReleasePortalButton.BackColor = Color.Gray;
            b2ReleasePortalButton.Location = new Point(113, 224);
            b2ReleasePortalButton.Margin = new Padding(2);
            b2ReleasePortalButton.Name = "b2ReleasePortalButton";
            b2ReleasePortalButton.Size = new Size(97, 23);
            b2ReleasePortalButton.TabIndex = 20;
            b2ReleasePortalButton.Text = "Release portal";
            b2ReleasePortalButton.UseVisualStyleBackColor = false;
            b2ReleasePortalButton.Click += b2ReleasePortal_Click;
            // 
            // b2ReleasedButton
            // 
            b2ReleasedButton.BackColor = Color.Gray;
            b2ReleasedButton.Location = new Point(16, 224);
            b2ReleasedButton.Margin = new Padding(2);
            b2ReleasedButton.Name = "b2ReleasedButton";
            b2ReleasedButton.Size = new Size(97, 23);
            b2ReleasedButton.TabIndex = 19;
            b2ReleasedButton.Text = "Released";
            b2ReleasedButton.UseVisualStyleBackColor = false;
            b2ReleasedButton.Click += b2ReleasedButton_Click;
            // 
            // bundle2TextBox
            // 
            bundle2TextBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            bundle2TextBox.Location = new Point(18, 252);
            bundle2TextBox.Margin = new Padding(4, 3, 4, 3);
            bundle2TextBox.Name = "bundle2TextBox";
            bundle2TextBox.Size = new Size(292, 23);
            bundle2TextBox.TabIndex = 18;
            bundle2TextBox.Text = "search:";
            bundle2TextBox.Enter += bundle2TextBox_Enter;
            bundle2TextBox.Leave += bundle2TextBox_Leave;
            // 
            // bundlesListLabel
            // 
            bundlesListLabel.AutoSize = true;
            bundlesListLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            bundlesListLabel.Location = new Point(16, 65);
            bundlesListLabel.Margin = new Padding(4, 0, 4, 0);
            bundlesListLabel.Name = "bundlesListLabel";
            bundlesListLabel.Size = new Size(100, 24);
            bundlesListLabel.TabIndex = 23;
            bundlesListLabel.Text = "Bundle 1:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(14, 198);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 24);
            label1.TabIndex = 24;
            label1.Text = "Bundle 2:";
            // 
            // compareButton
            // 
            compareButton.Location = new Point(18, 302);
            compareButton.Name = "compareButton";
            compareButton.Size = new Size(93, 23);
            compareButton.TabIndex = 25;
            compareButton.Text = "Compare";
            compareButton.UseVisualStyleBackColor = true;
            compareButton.Click += compareButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 9);
            label2.Name = "label2";
            label2.Size = new Size(271, 15);
            label2.TabIndex = 26;
            label2.Text = "Type in the full name of the bundle i.e. 2403632-04";
            // 
            // CompareForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 540);
            Controls.Add(label2);
            Controls.Add(compareButton);
            Controls.Add(label1);
            Controls.Add(bundlesListLabel);
            Controls.Add(b2WiPButton);
            Controls.Add(b2ReleasePortalButton);
            Controls.Add(b2ReleasedButton);
            Controls.Add(bundle2TextBox);
            Controls.Add(b1WiPButton);
            Controls.Add(b1ReleasePortalButton);
            Controls.Add(b1ReleasedButton);
            Controls.Add(bundle1TextBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CompareForm";
            Text = "CompareForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button b1WiPButton;
        private Button b1ReleasePortalButton;
        private Button b1ReleasedButton;
        private TextBox bundle1TextBox;
        private Button b2WiPButton;
        private Button b2ReleasePortalButton;
        private Button b2ReleasedButton;
        private TextBox bundle2TextBox;
        private Label bundlesListLabel;
        private Label label1;
        private Button compareButton;
        private Label label2;
    }
}