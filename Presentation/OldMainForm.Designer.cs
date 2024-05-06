namespace Presentation
{
    partial class OldMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OldMainForm));
            bundlesListLabel = new Label();
            bundlesListBox = new ListBox();
            searchBundlesTextBox = new TextBox();
            schematicsSearchTextBox = new TextBox();
            schematicsListBox = new ListBox();
            schematicsListLabel = new Label();
            productProtoButton = new Button();
            reldasButton = new Button();
            designerButton = new Button();
            releasedButton = new Button();
            currentProjectLabel = new Label();
            returnToProjectsButton = new Button();
            SuspendLayout();
            // 
            // bundlesListLabel
            // 
            bundlesListLabel.AutoSize = true;
            bundlesListLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            bundlesListLabel.Location = new Point(13, 9);
            bundlesListLabel.Margin = new Padding(4, 0, 4, 0);
            bundlesListLabel.Name = "bundlesListLabel";
            bundlesListLabel.Size = new Size(94, 24);
            bundlesListLabel.TabIndex = 0;
            bundlesListLabel.Text = "Bundles:";
            // 
            // bundlesListBox
            // 
            bundlesListBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            bundlesListBox.FormattingEnabled = true;
            bundlesListBox.ItemHeight = 16;
            bundlesListBox.Location = new Point(17, 142);
            bundlesListBox.Margin = new Padding(4, 3, 4, 3);
            bundlesListBox.Name = "bundlesListBox";
            bundlesListBox.Size = new Size(292, 340);
            bundlesListBox.TabIndex = 1;
            bundlesListBox.SelectedIndexChanged += bundlesListBox_SelectedIndexChanged;
            bundlesListBox.DoubleClick += bundlesListBox_DoubleClick;
            // 
            // searchBundlesTextBox
            // 
            searchBundlesTextBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            searchBundlesTextBox.Location = new Point(17, 108);
            searchBundlesTextBox.Margin = new Padding(4, 3, 4, 3);
            searchBundlesTextBox.Name = "searchBundlesTextBox";
            searchBundlesTextBox.Size = new Size(292, 23);
            searchBundlesTextBox.TabIndex = 4;
            searchBundlesTextBox.Text = "search:";
            searchBundlesTextBox.Enter += searchBundlesTextBox_Enter;
            searchBundlesTextBox.KeyDown += searchBundlesTextBox_KeyDown;
            searchBundlesTextBox.Leave += searchBundlesTextBox_Leave;
            // 
            // schematicsSearchTextBox
            // 
            schematicsSearchTextBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            schematicsSearchTextBox.Location = new Point(410, 108);
            schematicsSearchTextBox.Margin = new Padding(4, 3, 4, 3);
            schematicsSearchTextBox.Name = "schematicsSearchTextBox";
            schematicsSearchTextBox.Size = new Size(292, 23);
            schematicsSearchTextBox.TabIndex = 7;
            schematicsSearchTextBox.Text = "search:";
            schematicsSearchTextBox.Enter += schematicsSearchTextBox_Enter;
            schematicsSearchTextBox.Leave += schematicsSearchTextBox_Leave;
            // 
            // schematicsListBox
            // 
            schematicsListBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            schematicsListBox.FormattingEnabled = true;
            schematicsListBox.ItemHeight = 16;
            schematicsListBox.Location = new Point(410, 142);
            schematicsListBox.Margin = new Padding(4, 3, 4, 3);
            schematicsListBox.Name = "schematicsListBox";
            schematicsListBox.Size = new Size(292, 340);
            schematicsListBox.TabIndex = 6;
            schematicsListBox.SelectedIndexChanged += schematicsListBox_SelectedIndexChanged;
            schematicsListBox.DoubleClick += schematicsListBox_DoubleClick;
            // 
            // schematicsListLabel
            // 
            schematicsListLabel.AutoSize = true;
            schematicsListLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            schematicsListLabel.Location = new Point(410, 9);
            schematicsListLabel.Margin = new Padding(4, 0, 4, 0);
            schematicsListLabel.Name = "schematicsListLabel";
            schematicsListLabel.Size = new Size(95, 24);
            schematicsListLabel.TabIndex = 5;
            schematicsListLabel.Text = "Projects:";
            // 
            // productProtoButton
            // 
            productProtoButton.BackColor = Color.Gray;
            productProtoButton.Location = new Point(17, 81);
            productProtoButton.Margin = new Padding(2);
            productProtoButton.Name = "productProtoButton";
            productProtoButton.Size = new Size(97, 23);
            productProtoButton.TabIndex = 8;
            productProtoButton.Text = "Production/Proto";
            productProtoButton.UseVisualStyleBackColor = false;
            productProtoButton.Click += productProtoButton_Click;
            // 
            // reldasButton
            // 
            reldasButton.BackColor = Color.Gray;
            reldasButton.Location = new Point(114, 81);
            reldasButton.Margin = new Padding(2);
            reldasButton.Name = "reldasButton";
            reldasButton.Size = new Size(97, 23);
            reldasButton.TabIndex = 9;
            reldasButton.Text = "Release portal";
            reldasButton.UseVisualStyleBackColor = false;
            reldasButton.Click += reldasButton_Click;
            // 
            // designerButton
            // 
            designerButton.BackColor = Color.White;
            designerButton.Location = new Point(212, 81);
            designerButton.Margin = new Padding(2);
            designerButton.Name = "designerButton";
            designerButton.Size = new Size(97, 23);
            designerButton.TabIndex = 10;
            designerButton.Text = "WiP";
            designerButton.UseVisualStyleBackColor = false;
            designerButton.Click += designerButton_Click;
            // 
            // releasedButton
            // 
            releasedButton.BackColor = Color.White;
            releasedButton.Location = new Point(410, 44);
            releasedButton.Margin = new Padding(2);
            releasedButton.Name = "releasedButton";
            releasedButton.Size = new Size(97, 23);
            releasedButton.TabIndex = 11;
            releasedButton.Text = "Released";
            releasedButton.UseVisualStyleBackColor = false;
            releasedButton.Click += releasedButton_Click;
            // 
            // currentProjectLabel
            // 
            currentProjectLabel.AutoSize = true;
            currentProjectLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            currentProjectLabel.Location = new Point(410, 79);
            currentProjectLabel.Name = "currentProjectLabel";
            currentProjectLabel.Size = new Size(100, 20);
            currentProjectLabel.TabIndex = 24;
            currentProjectLabel.Text = "Select project";
            // 
            // returnToProjectsButton
            // 
            returnToProjectsButton.Location = new Point(627, 79);
            returnToProjectsButton.Name = "returnToProjectsButton";
            returnToProjectsButton.Size = new Size(75, 23);
            returnToProjectsButton.TabIndex = 25;
            returnToProjectsButton.Text = "Back";
            returnToProjectsButton.UseVisualStyleBackColor = true;
            returnToProjectsButton.Click += returnToProjectsButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 501);
            Controls.Add(returnToProjectsButton);
            Controls.Add(currentProjectLabel);
            Controls.Add(releasedButton);
            Controls.Add(designerButton);
            Controls.Add(reldasButton);
            Controls.Add(productProtoButton);
            Controls.Add(schematicsSearchTextBox);
            Controls.Add(schematicsListBox);
            Controls.Add(schematicsListLabel);
            Controls.Add(searchBundlesTextBox);
            Controls.Add(bundlesListBox);
            Controls.Add(bundlesListLabel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "Main form";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label bundlesListLabel;
        private System.Windows.Forms.ListBox bundlesListBox;
        private System.Windows.Forms.TextBox searchBundlesTextBox;
        private System.Windows.Forms.TextBox schematicsSearchTextBox;
        private System.Windows.Forms.ListBox schematicsListBox;
        private System.Windows.Forms.Label schematicsListLabel;
        private System.Windows.Forms.Button productProtoButton;
        private System.Windows.Forms.Button reldasButton;
        private System.Windows.Forms.Button designerButton;
        private System.Windows.Forms.Button releasedButton;
        private Label currentProjectLabel;
        private Button returnToProjectsButton;
    }
}