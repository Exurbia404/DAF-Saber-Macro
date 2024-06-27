namespace Presentation
{
    partial class ChoiceForm
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
            choiceLabel = new Label();
            goToBundlesButton = new Button();
            goToProjectsButton = new Button();
            SuspendLayout();
            // 
            // choiceLabel
            // 
            choiceLabel.AutoSize = true;
            choiceLabel.Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point);
            choiceLabel.Location = new Point(13, 9);
            choiceLabel.Margin = new Padding(4, 0, 4, 0);
            choiceLabel.Name = "choiceLabel";
            choiceLabel.Size = new Size(89, 24);
            choiceLabel.TabIndex = 27;
            choiceLabel.Text = "Choose:";
            // 
            // goToBundlesButton
            // 
            goToBundlesButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            goToBundlesButton.Location = new Point(13, 48);
            goToBundlesButton.Name = "goToBundlesButton";
            goToBundlesButton.Size = new Size(120, 60);
            goToBundlesButton.TabIndex = 28;
            goToBundlesButton.Text = "Bundles";
            goToBundlesButton.UseVisualStyleBackColor = true;
            goToBundlesButton.Click += goToBundlesButton_Click;
            // 
            // goToProjectsButton
            // 
            goToProjectsButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            goToProjectsButton.Location = new Point(139, 48);
            goToProjectsButton.Name = "goToProjectsButton";
            goToProjectsButton.Size = new Size(120, 60);
            goToProjectsButton.TabIndex = 29;
            goToProjectsButton.Text = "Dataset";
            goToProjectsButton.UseVisualStyleBackColor = true;
            goToProjectsButton.Click += goToProjectsButton_Click;
            // 
            // ChoiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 501);
            Controls.Add(goToProjectsButton);
            Controls.Add(goToBundlesButton);
            Controls.Add(choiceLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ChoiceForm";
            Text = "ChoiceForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label choiceLabel;
        private Button goToBundlesButton;
        private Button goToProjectsButton;
    }
}