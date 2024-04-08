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
        public SettingsForm(Logger logger)
        {
            InitializeComponent();

            _logger = logger;
            _logger.Log("Settings opened");
        }

        private void goToProfilesButton_Click(object sender, EventArgs e)
        {
            _logger.Log("goToProfilesButton_clicked");
            var newProfileForm = new ProfileCreator(_logger);
            newProfileForm.Show();
        }

        private void goToRefsetsButton_Click(object sender, EventArgs e)
        {
            _logger.Log("openrefSetFormButton_clicked");
            var RefSetForm = new RefSetForm(_logger);
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
