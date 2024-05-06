namespace Presentation
{
    partial class BundleForm
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
            searchBundlesTextBox = new TextBox();
            designerButton = new Button();
            reldasButton = new Button();
            productProtoButton = new Button();
            SuspendLayout();
            // 
            // searchBundlesTextBox
            // 
            searchBundlesTextBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            searchBundlesTextBox.Location = new Point(13, 69);
            searchBundlesTextBox.Margin = new Padding(4, 3, 4, 3);
            searchBundlesTextBox.Name = "searchBundlesTextBox";
            searchBundlesTextBox.Size = new Size(292, 23);
            searchBundlesTextBox.TabIndex = 5;
            searchBundlesTextBox.Text = "search:";
            searchBundlesTextBox.Enter += searchBundlesTextBox_Enter_1;
            searchBundlesTextBox.KeyDown += searchBundlesTextBox_KeyDown;
            searchBundlesTextBox.Leave += searchBundlesTextBox_Leave_1;
            // 
            // designerButton
            // 
            designerButton.BackColor = Color.White;
            designerButton.Location = new Point(206, 41);
            designerButton.Margin = new Padding(2);
            designerButton.Name = "designerButton";
            designerButton.Size = new Size(97, 23);
            designerButton.TabIndex = 13;
            designerButton.Text = "WiP";
            designerButton.UseVisualStyleBackColor = false;
            designerButton.Click += designerButton_Click;
            // 
            // reldasButton
            // 
            reldasButton.BackColor = Color.Gray;
            reldasButton.Location = new Point(108, 41);
            reldasButton.Margin = new Padding(2);
            reldasButton.Name = "reldasButton";
            reldasButton.Size = new Size(97, 23);
            reldasButton.TabIndex = 12;
            reldasButton.Text = "Release portal";
            reldasButton.UseVisualStyleBackColor = false;
            reldasButton.Click += reldasButton_Click;
            // 
            // productProtoButton
            // 
            productProtoButton.BackColor = Color.Gray;
            productProtoButton.Location = new Point(11, 41);
            productProtoButton.Margin = new Padding(2);
            productProtoButton.Name = "productProtoButton";
            productProtoButton.Size = new Size(97, 23);
            productProtoButton.TabIndex = 11;
            productProtoButton.Text = "Production/Proto";
            productProtoButton.UseVisualStyleBackColor = false;
            productProtoButton.Click += productProtoButton_Click;
            // 
            // BundleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(designerButton);
            Controls.Add(reldasButton);
            Controls.Add(productProtoButton);
            Controls.Add(searchBundlesTextBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BundleForm";
            Text = "BundleForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox searchBundlesTextBox;
        private Button designerButton;
        private Button reldasButton;
        private Button productProtoButton;
    }
}