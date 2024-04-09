using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class SettingsForm : Form
    {
        private Logger _logger;
        private PanelForm panelForm;
        public SettingsForm(Logger logger, PanelForm panelform)
        {
            InitializeComponent();

            panelForm = panelform;
            _logger = logger;
            _logger.Log("Settings opened");
        }

        private void goToProfilesButton_Click(object sender, EventArgs e)
        {
            _logger.Log("goToProfilesButton_clicked");

            // Create a new instance of ProfileCreator form
            var newProfileForm = new ProfileCreator(_logger);

            // Set the newProfileForm's TopLevel property to false
            newProfileForm.TopLevel = false;

            // Clear the controls in the panel of the parent form
            (panelForm.Controls["panel"] as Panel).Controls.Clear();

            // Add the newProfileForm to the panel's controls of the parent form
            (panelForm.Controls["panel"] as Panel).Controls.Add(newProfileForm);

            // Set the Dock property of the newProfileForm to Fill
            newProfileForm.Dock = DockStyle.Fill;

            // Show the newProfileForm
            newProfileForm.Show();
        }

        private void goToRefsetsButton_Click(object sender, EventArgs e)
        {
            _logger.Log("openrefSetFormButton_clicked");

            // Create a new instance of RefSetForm
            var RefSetForm = new RefSetForm(_logger);

            // Set the RefSetForm's TopLevel property to false
            RefSetForm.TopLevel = false;

            // Clear the controls in the panel of the parent form
            (panelForm.Controls["panel"] as Panel).Controls.Clear();

            // Add the newProfileForm to the panel's controls of the parent form
            (panelForm.Controls["panel"] as Panel).Controls.Add(RefSetForm);

            // Set the Dock property of the RefSetForm to Fill
            RefSetForm.Dock = DockStyle.Fill;

            // Show the RefSetForm
            RefSetForm.Show();
        }

        private void clearTempDataButton_Click(object sender, EventArgs e)
        {
            // Get the path to the TempData directory
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Saber Tool Plus", "TempData");

            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                try
                {
                    // Get all files in the directory
                    string[] files = Directory.GetFiles(directoryPath);

                    // Delete each file
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    MessageBox.Show("Temp data cleared successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger.Log("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Temp data directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Log("Temp data directory does not exist");
            }
        }
    }
}
