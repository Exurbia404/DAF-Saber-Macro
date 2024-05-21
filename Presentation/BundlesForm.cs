using Data_Access;
using Logging;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;

namespace Presentation
{
    public partial class BundlesForm : Form
    {
        private Logger _logger;

        private PanelForm panelForm;

        //TODO: these can probably be declared in the functions there used
        private List<Bundle> extractedBundles;
        private List<DSI_Tube> extractedTubes;
        private List<DSI_Wire> extractedWires;
        private List<DSI_Component> extractedComponents;

        private string ProductionBuildOfMaterialsFolder;
        private string ReldasBuildOfMaterialsFolder;
        private string DesignerBuildOfMaterialsFolder;

        private List<string> folderPaths;

        public BundlesForm(Logger logger, PanelForm panelform)
        {
            InitializeComponent();

            panelForm = panelform;
            _logger = logger;

            try
            {
                if (Environment.MachineName == "EXURBIA")
                {
                    DesignerBuildOfMaterialsFolder = new FolderPaths(logger).ExurbiaLocal;
                }
                folderPaths = GetImmediateSubfolders(DesignerBuildOfMaterialsFolder);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }

            _logger.Log("current directory is: WiP");
        }

        private List<string> GetImmediateSubfolders(string folderPath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> folderPaths = new List<string>();

            try
            {
                // Get all immediate subfolders synchronously
                string[] subfolders = Directory.GetDirectories(folderPath);

                // Process subfolders
                foreach (string subfolder in subfolders)
                {
                    try
                    {
                        // Check if the folder has any files

                        // Extract folder name and path
                        string folderName = Path.GetFileName(subfolder);

                        // Check if the folder name is 7 or 8 numbers long
                        if (IsNumeric(folderName) && (folderName.Length == 7 || folderName.Length == 8))
                        {
                            // Add to list
                            folderPaths.Add(subfolder);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Log($"Error processing folder '{subfolder}': {ex.Message}");
                    }
                }

                stopwatch.Stop();
                _logger.Log("Folders retrieved in: " + stopwatch.Elapsed.TotalMilliseconds + "ms");
                return folderPaths;
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
                return null;
            }
        }
        private static bool IsNumeric(string str)
        {
            return int.TryParse(str, out _);
        }

        private void searchBundlesTextBox_SetText()
        {
            searchBundlesTextBox.Text = "Search:";
            searchBundlesTextBox.ForeColor = Color.Gray;
        }

        private void searchBundlesTextBox_Enter_1(object sender, EventArgs e)
        {
            if (searchBundlesTextBox.ForeColor == Color.Black)
                return;
            searchBundlesTextBox.Text = "";
            searchBundlesTextBox.ForeColor = Color.Black;
        }

        private void searchBundlesTextBox_Leave_1(object sender, EventArgs e)
        {
            if (searchBundlesTextBox.Text.Trim() == "")
                searchBundlesTextBox_SetText();
        }

        private void searchBundlesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                string searchText;

                // Get the search text
                if (searchBundlesTextBox.Text == "Search:")
                {
                    searchText = "";
                }
                else
                {
                    // Convert to lowercase for case-insensitive comparison
                    searchText = searchBundlesTextBox.Text.ToLower();
                }

                // Filter folderPaths based on the search text
                IEnumerable<string> filteredFolderPaths = folderPaths
                    .Where(path => Path.GetFileName(path).IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1);

                // If there's only one option, open the latest bundle file
                if (filteredFolderPaths.Count() == 1)
                {
                    _logger.Log("Opening file");
                    OpenLatestBundleFile(filteredFolderPaths.First());
                }
                else
                {
                    MessageBox.Show("The specified bundle was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenLatestBundleFile(string bundleName)
        {
            try
            {
                // Get the corresponding folder path
                string selectedFolderPath = GetFilePath(bundleName);

                // Search for the latest .txt file containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");
                if (txtFiles.Length > 0)
                {
                    // Sort files by creation time and get the latest one
                    string latestTxtFile = txtFiles.OrderByDescending(f => new FileInfo(f).CreationTime).First();

                    ExtractAndOpenExcel(latestTxtFile);
                    // Do something with the latest .txt file, for example, display its path
                    _logger.Log($"Latest .txt file in {selectedFolderPath} is: {latestTxtFile}");
                }
                else
                {
                    _logger.Log($"No matching .txt files found in {selectedFolderPath}");
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
            }

        }

        private string GetFilePath(string inputName)
        {
            try
            {
                inputName = Path.GetFileName(inputName);

                foreach (string folderPath in folderPaths)
                {
                    string folderName = Path.GetFileName(folderPath);

                    // Case-insensitive comparison
                    if (string.Equals(folderName, inputName, StringComparison.OrdinalIgnoreCase))
                    {
                        return folderPath;
                    }
                }

                // No matching folder path found
                return null;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                return null;
            }
        }

        private void ExtractAndOpenExcel(string textFilePath)
        {
            try
            {
                _logger.Log("Trying to open file");
                Extractor extractor = new Extractor(_logger);

                extractor.ExtractBundleFromFilePath(textFilePath);

                extractedBundles = extractor.Bundles;
                extractedTubes = extractor.ExtractDSITubes(textFilePath);
                FileHandler fileHandler = new FileHandler(_logger);

                List<Converted_Component> convertedComponents = extractor.Components;
                List<Converted_Wire> convertedWires = extractor.Wires;


                string bundleNumber = GetFileName(textFilePath);
                string filePath = GetFolderPath(textFilePath);

                //Only WriteToFile when in WiP folder
                if (designerButton.BackColor == Color.White) //TODO: crude fix, find something more elegant
                {
                    //TODO: this function is still gettin its data from WCSPP_Convertor. shouldn't it be implemented through the new extractor_copy?
                    fileHandler.WriteToFile(convertedWires.Cast<Data_Interfaces.iConverted_Wire>().ToList(), convertedComponents.Cast<Data_Interfaces.iConverted_Component>().ToList(), extractedBundles.Cast<Data_Interfaces.iBundle>().ToList(), bundleNumber, filePath);
                }

                ProfileChoiceForm pcForm = new ProfileChoiceForm(_logger, bundleNumber);

                // Set the newProfileForm's TopLevel property to false
                pcForm.TopLevel = false;

                (panelForm.Controls["panel"] as Panel).Controls.Clear();

                // Add the newProfileForm to the panel's controls of the parent form
                (panelForm.Controls["panel"] as Panel).Controls.Add(pcForm);

                pcForm.Dock = DockStyle.Fill;

                pcForm.SetBundleData(convertedWires, convertedComponents, extractedTubes, extractedBundles);

                pcForm.Show();

            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
        }

        private string GetFileName(string filePath)
        {
            try
            {
                // Get the file name without the extension
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

                // Remove the substring "_DSI" from the file name
                string cleanedFileName = fileNameWithoutExtension.Replace("_DSI", "");

                return fileNameWithoutExtension;
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as invalid file paths
                _logger.Log($"Error occurred: {ex.Message}");
                return null;
            }
        }

        private string GetFolderPath(string filePath)
        {
            try
            {
                // Get the directory name containing the file
                return Path.GetDirectoryName(filePath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as invalid file paths
                _logger.Log($"Error occurred: {ex.Message}");
                return null;
            }
        }

        private void productProtoButton_Click(object sender, EventArgs e)
        {
            _logger.Log("current directory is: Production/Proto");
            folderPaths = GetImmediateSubfolders(ProductionBuildOfMaterialsFolder);
        }

        private void reldasButton_Click(object sender, EventArgs e)
        {
            _logger.Log("current directory is: Release");
            folderPaths = GetImmediateSubfolders(ReldasBuildOfMaterialsFolder);
        }

        private void designerButton_Click(object sender, EventArgs e)
        {
            _logger.Log("current directory is: WiP");
            folderPaths = GetImmediateSubfolders(DesignerBuildOfMaterialsFolder);
        }
    }
}
