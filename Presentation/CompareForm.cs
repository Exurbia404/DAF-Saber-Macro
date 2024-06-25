using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.Playback;

namespace Presentation
{
    public partial class CompareForm : Form
    {
        private Logger _logger;
        private PanelForm panelForm;

        private string ProductionBuildOfMaterialsFolder;
        private string ReldasBuildOfMaterialsFolder;
        private string DesignerBuildOfMaterialsFolder;
        
        private string Directory1;
        private string Directory2;

        public CompareForm(Logger logger, PanelForm panelform)
        {
            InitializeComponent();

            _logger = logger;
            panelForm = panelform;

            //Released
            ProductionBuildOfMaterialsFolder = panelform.Settings.ProductionFolder;
            
            //Release portal
            ReldasBuildOfMaterialsFolder = panelform.Settings.ReldasFolder;
            
            //WiP
            DesignerBuildOfMaterialsFolder = panelform.Settings.DesignerFolder;

            // Check CompareIt executable path
            if (!File.Exists(panelForm.Settings.CompareItFilepath))
            {
                MessageBox.Show("CompareIt executable not found or path is invalid.", "CompareIt Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            try
            {
                if (Environment.MachineName == "EXURBIA")
                {
                    DesignerBuildOfMaterialsFolder = new FolderPaths(logger).Exurbia_Designer;
                }

                SetDirectory1(0);
                SetDirectory2(0);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
        }

        private void b1WiPButton_Click(object sender, EventArgs e)
        {
            b1WiPButton.BackColor = Color.White;
            b1ReleasedButton.BackColor = Color.Gray;
            b1ReleasePortalButton.BackColor = Color.Gray;

            SetDirectory1(0);
        }

        private void b1ReleaseButton_Click(object sender, EventArgs e)
        {
            b1ReleasePortalButton.BackColor = Color.White;
            b1WiPButton.BackColor = Color.Gray;
            b1ReleasedButton.BackColor = Color.Gray;

            SetDirectory1(1);

        }

        private void SetDirectory1(int index)
        {
            switch(index)
            {
                case 0:
                    Directory1 = DesignerBuildOfMaterialsFolder;
                    break;
                case 1:
                    Directory1 = ReldasBuildOfMaterialsFolder;
                    break;
                case 2:
                    Directory1 = ProductionBuildOfMaterialsFolder;
                    break;
            }
        }

        private void b1ReleasedButton_Click(object sender, EventArgs e)
        {
            b1ReleasedButton.BackColor = Color.White;
            b1WiPButton.BackColor = Color.Gray;
            b1ReleasePortalButton.BackColor = Color.Gray;

            SetDirectory1(2);
        }

        private void b2WiPButton_Click(object sender, EventArgs e)
        {
            b2WiPButton.BackColor = Color.White;
            b2ReleasedButton.BackColor = Color.Gray;
            b2ReleasePortalButton.BackColor = Color.Gray;

            SetDirectory2(0);
        }

        private void b2ReleasePortal_Click(object sender, EventArgs e)
        {
            b2ReleasePortalButton.BackColor = Color.White;
            b2WiPButton.BackColor = Color.Gray;
            b2ReleasedButton.BackColor = Color.Gray;

            SetDirectory2(1);
        }

        private void b2ReleasedButton_Click(object sender, EventArgs e)
        {
            b2ReleasedButton.BackColor = Color.White;
            b2WiPButton.BackColor = Color.Gray;
            b2ReleasePortalButton.BackColor = Color.Gray;

            SetDirectory2(2);
        }

        private void SetDirectory2(int index)
        {
            switch (index)
            {
                case 0:
                    Directory2 = DesignerBuildOfMaterialsFolder;
                    break;
                case 1:
                    Directory2 = ReldasBuildOfMaterialsFolder;
                    break;
                case 2:
                    Directory2 = ProductionBuildOfMaterialsFolder;
                    break;
            }
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            try
            {
                string bundle1 = bundle1TextBox.Text;
                string bundle2 = bundle2TextBox.Text;

                string bundle1FolderName = bundle1.Split("-")[0];
                string bundle2FolderName = bundle2.Split("-")[0];

                string bundle1FolderPath = FindFolderPath(bundle1FolderName, Directory1);
                string bundle2FolderPath = FindFolderPath(bundle2FolderName, Directory2);

                if (string.IsNullOrEmpty(bundle1FolderPath))
                {
                    MessageBox.Show($"Folder {bundle1FolderName} not found in Directory1", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (string.IsNullOrEmpty(bundle2FolderPath))
                {
                    MessageBox.Show($"Folder {bundle2FolderName} not found in Directory2", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                string chosenBundle1 = GetBundleFromFolder(bundle1FolderPath, bundle1);
                string chosenBundle2 = GetBundleFromFolder(bundle2FolderPath, bundle2);

                OpenComparison(chosenBundle1, chosenBundle2);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
            
        }

        private string GetBundleFromFolder(string folderPath, string bundleNumber)
        {
            // Search for files in the folder that contain "DSI" in their name
            string[] files = Directory.GetFiles(folderPath, $"*{bundleNumber}*DSI*", SearchOption.TopDirectoryOnly);

            // Check if any files matching the pattern were found
            if (files.Length > 0)
            {
                // Return the first file found (you can modify logic to handle multiple matches if needed)
                return files[0];
            }
            else
            {
                // Handle case where no matching file was found
                throw new FileNotFoundException($"File with DSI in its name not found in folder: {folderPath}");
            }
        }

        private void OpenComparison(string fileLocation1, string fileLocation2)
        {
            try
            {
                string compareItPath = panelForm.Settings.CompareItFilepath; // Replace with the actual path to CompareIt!

                // Ensure CompareIt! exists at the specified path
                if (!File.Exists(compareItPath))
                {
                    throw new FileNotFoundException("CompareIt! program not found at specified path.");
                }

                // Ensure both files exist before attempting to open them
                if (!File.Exists(fileLocation1))
                {
                    throw new FileNotFoundException($"File not found: {fileLocation1}");
                }

                if (!File.Exists(fileLocation2))
                {
                    throw new FileNotFoundException($"File not found: {fileLocation2}");
                }

                // Prepare arguments for CompareIt!
                string arguments = $"\"{fileLocation1}\" \"{fileLocation2}\"";

                // Start CompareIt! with the specified files for comparison
                ProcessStartInfo psi = new ProcessStartInfo(compareItPath);
                psi.Arguments = arguments;

                Process.Start(psi);
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
            }
            
        }

        private string FindFolderPath(string folderName, string baseDirectory)
        {
            string[] directories = Directory.GetDirectories(baseDirectory, folderName);
            if (directories.Length > 0)
            {
                return directories[0]; // Return the first matching directory found
            }
            else
            {
                return null; // Return null if directory not found
            }
        }

        private void bundle1TextBox_Enter(object sender, EventArgs e)
        {
            if (bundle1TextBox.ForeColor == Color.Black)
                return;
            bundle1TextBox.Text = "";
            bundle1TextBox.ForeColor = Color.Black;
        }

        private void bundle1TextBox_Leave(object sender, EventArgs e)
        {
            if (bundle1TextBox.Text.Trim() == "")
                bundle1TextBox_SetText();
        }

        private void bundle1TextBox_SetText()
        {
            bundle1TextBox.Text = "Search:";
            bundle1TextBox.ForeColor = Color.Gray;
        }

        private void bundle2TextBox_Enter(object sender, EventArgs e)
        {
            if (bundle2TextBox.ForeColor == Color.Black)
                return;
            bundle2TextBox.Text = "";
            bundle2TextBox.ForeColor = Color.Black;
        }

        private void bundle2TextBox_Leave(object sender, EventArgs e)
        {
            if (bundle2TextBox.Text.Trim() == "")
                bundle2TextBox_SetText();
        }

        private void bundle2TextBox_SetText()
        {
            bundle2TextBox.Text = "Search:";
            bundle2TextBox.ForeColor = Color.Gray;
        }


    }
}
