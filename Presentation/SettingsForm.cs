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
            new FolderPaths(logger);
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
            _logger.Log("Attempting to clear cache");
            // Get the path to the TempData directory
            string TempData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Saber Tool Plus", "TempData");
            string LogsData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Saber Tool Plus", "Logs");

            // Check if the directory exists
            if (Directory.Exists(TempData))
            {
                try
                {
                    // Get all files each directory
                    string[] tempDataFiles = Directory.GetFiles(TempData);
                    string[] tempLogsFiles = Directory.GetFiles(LogsData);

                    string[] files = Directory.GetFiles(TempData).Concat(Directory.GetFiles(LogsData)).ToArray();
                    _logger.Log("Number of files found: " + files.Count().ToString());

                    // Delete each file
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    MessageBox.Show("Temp data cleared successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _logger.Log("Cache cleared succesfully");
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

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (panelForm.Settings.FolderPaths.HasFoundDAF)
            {
                dafFileServerButton.Enabled = true;
            }
            if (panelForm.Settings.FolderPaths.HasFoundLEY)
            {
                leylandFileServerButton.Enabled = true;
            }
        }

        private void dafFileServerButton_Click(object sender, EventArgs e)
        {
            panelForm.Settings.SwitchFileServer(Settings.FileServers.DAF);
        }

        private void leylandFileServerButton_Click(object sender, EventArgs e)
        {
            panelForm.Settings.SwitchFileServer(Settings.FileServers.Leyland);
        }

        private void selectNewFolderButton_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {

                }
            }
        }

        private void setCompareItLocationButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select CompareIt Executable";
                openFileDialog.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string selectedFilePath = openFileDialog.FileName;

                    // Update the CompareItFilepath property (assuming CompareItFilepath is defined)
                    panelForm.Settings.CompareItFilepath = selectedFilePath;

                    // Optionally display the selected file path or perform other actions
                    MessageBox.Show($"CompareIt location set to: {selectedFilePath}", "CompareIt Location Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void updateCocoDatabaseButton_Click(object sender, EventArgs e)
        {
            try
            {
                CoCoHandler newCocohandler = new CoCoHandler(_logger);

                newCocohandler.UpdateCoCoUsingExcel();
                _logger.Log("CoCo database succesfully updated");
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }
    }
}
