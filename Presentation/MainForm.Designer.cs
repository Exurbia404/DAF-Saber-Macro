namespace Presentation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            goToProfilesButton = new Button();
            button2 = new Button();
            button3 = new Button();
            openRefSetFormButton = new Button();
            programStatusButton = new Button();
            label1 = new Label();
            versionLabel = new Label();
            lastMessageTextBox = new TextBox();
            label2 = new Label();
            currentProjectLabel = new Label();
            returnToProjectsButton = new Button();
            SuspendLayout();
            // 
            // bundlesListLabel
            // 
            bundlesListLabel.AutoSize = true;
            bundlesListLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            bundlesListLabel.Location = new Point(10, 69);
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
            bundlesListBox.Location = new Point(14, 202);
            bundlesListBox.Margin = new Padding(4, 3, 4, 3);
            bundlesListBox.Name = "bundlesListBox";
            bundlesListBox.Size = new Size(292, 388);
            bundlesListBox.TabIndex = 1;
            bundlesListBox.SelectedIndexChanged += bundlesListBox_SelectedIndexChanged;
            bundlesListBox.DoubleClick += bundlesListBox_DoubleClick;
            // 
            // searchBundlesTextBox
            // 
            searchBundlesTextBox.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point);
            searchBundlesTextBox.Location = new Point(14, 168);
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
            schematicsSearchTextBox.Location = new Point(407, 168);
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
            schematicsListBox.Location = new Point(407, 202);
            schematicsListBox.Margin = new Padding(4, 3, 4, 3);
            schematicsListBox.Name = "schematicsListBox";
            schematicsListBox.Size = new Size(292, 388);
            schematicsListBox.TabIndex = 6;
            schematicsListBox.DoubleClick += schematicsListBox_DoubleClick;
            // 
            // schematicsListLabel
            // 
            schematicsListLabel.AutoSize = true;
            schematicsListLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            schematicsListLabel.Location = new Point(407, 69);
            schematicsListLabel.Margin = new Padding(4, 0, 4, 0);
            schematicsListLabel.Name = "schematicsListLabel";
            schematicsListLabel.Size = new Size(95, 24);
            schematicsListLabel.TabIndex = 5;
            schematicsListLabel.Text = "Projects:";
            // 
            // productProtoButton
            // 
            productProtoButton.BackColor = Color.Gray;
            productProtoButton.Location = new Point(14, 141);
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
            reldasButton.Location = new Point(111, 141);
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
            designerButton.Location = new Point(209, 141);
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
            releasedButton.Location = new Point(407, 104);
            releasedButton.Margin = new Padding(2);
            releasedButton.Name = "releasedButton";
            releasedButton.Size = new Size(97, 23);
            releasedButton.TabIndex = 11;
            releasedButton.Text = "Released";
            releasedButton.UseVisualStyleBackColor = false;
            releasedButton.Click += releasedButton_Click;
            // 
            // goToProfilesButton
            // 
            goToProfilesButton.Enabled = false;
            goToProfilesButton.Location = new Point(14, 612);
            goToProfilesButton.Margin = new Padding(4, 3, 4, 3);
            goToProfilesButton.Name = "goToProfilesButton";
            goToProfilesButton.Size = new Size(88, 27);
            goToProfilesButton.TabIndex = 13;
            goToProfilesButton.Text = "Profiles";
            goToProfilesButton.UseVisualStyleBackColor = true;
            goToProfilesButton.Click += goToProfilesButton_Click;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(10, 6);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(88, 35);
            button2.TabIndex = 15;
            button2.Text = "Release";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(105, 6);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(88, 35);
            button3.TabIndex = 16;
            button3.Text = "Compare";
            button3.UseVisualStyleBackColor = true;
            // 
            // openRefSetFormButton
            // 
            openRefSetFormButton.Location = new Point(111, 612);
            openRefSetFormButton.Name = "openRefSetFormButton";
            openRefSetFormButton.Size = new Size(88, 27);
            openRefSetFormButton.TabIndex = 17;
            openRefSetFormButton.Text = "RefSets";
            openRefSetFormButton.UseVisualStyleBackColor = true;
            openRefSetFormButton.Click += openRefSetFormButton_Click;
            // 
            // programStatusButton
            // 
            programStatusButton.Location = new Point(666, 11);
            programStatusButton.Name = "programStatusButton";
            programStatusButton.Size = new Size(43, 24);
            programStatusButton.TabIndex = 19;
            programStatusButton.UseVisualStyleBackColor = true;
            programStatusButton.Click += programStatusButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(599, 16);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 20;
            label1.Text = "Messages:";
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(590, 638);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(109, 15);
            versionLabel.TabIndex = 21;
            versionLabel.Text = "version: Alpha 0.1.2";
            // 
            // lastMessageTextBox
            // 
            lastMessageTextBox.Location = new Point(449, 612);
            lastMessageTextBox.Name = "lastMessageTextBox";
            lastMessageTextBox.ReadOnly = true;
            lastMessageTextBox.Size = new Size(250, 23);
            lastMessageTextBox.TabIndex = 22;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(407, 615);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 23;
            label2.Text = "Status:";
            // 
            // currentProjectLabel
            // 
            currentProjectLabel.AutoSize = true;
            currentProjectLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            currentProjectLabel.Location = new Point(407, 139);
            currentProjectLabel.Name = "currentProjectLabel";
            currentProjectLabel.Size = new Size(100, 20);
            currentProjectLabel.TabIndex = 24;
            currentProjectLabel.Text = "Select project";
            // 
            // returnToProjectsButton
            // 
            returnToProjectsButton.Location = new Point(624, 139);
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
            ClientSize = new Size(721, 666);
            Controls.Add(returnToProjectsButton);
            Controls.Add(currentProjectLabel);
            Controls.Add(label2);
            Controls.Add(lastMessageTextBox);
            Controls.Add(versionLabel);
            Controls.Add(label1);
            Controls.Add(programStatusButton);
            Controls.Add(openRefSetFormButton);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(goToProfilesButton);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "Main form";
            MouseClick += Form1_MouseClick;
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
        private System.Windows.Forms.Button goToProfilesButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private Button openRefSetFormButton;
        private Button programStatusButton;
        private Label label1;
        private Label versionLabel;
        private TextBox lastMessageTextBox;
        private Label label2;
        private Label currentProjectLabel;
        private Button returnToProjectsButton;
    }
}