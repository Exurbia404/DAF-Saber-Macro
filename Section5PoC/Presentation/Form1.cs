using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Section5PoC.Presentation
{
    public partial class Form1 : Form
    {
        private string BuildOfMaterialsFolder = @"U:\Data\SaberWiP\2_Users\designs\BSA\Boms";
        private string LocalBuildOfMaterialsFolder = @"C:\Users\tomvh\Documents\School\S5 - Internship\boms";

        private List<string> folderNames;
        private List<string> folderPaths;

        private List<string> loadedVersionPaths;

        private Extractor extractor;
        private WCSPP_Convertor convertor;

        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static List<Bundle> extractedBundles;

        public Form1()
        {
            string computerName = Environment.MachineName;
            Console.WriteLine($"Computer Name: {computerName}");

            extractor = new Extractor();
            folderNames = new List<string>();
            folderPaths = new List<string>();
            loadedVersionPaths = new List<string>();

            try
            {
                if(computerName == "EXURBIA")
                {
                    BuildOfMaterialsFolder = LocalBuildOfMaterialsFolder;
                }
                InitializeComponent();
                GetImmediateSubfolders(BuildOfMaterialsFolder, out folderNames, out folderPaths);
                AddNamesToListBox(folderPaths, folderNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

            

        private void AddNamesToListBox(List<string> filteredFolderPaths, List<string> filteredFolderNames)
        {
            schematicsListBox.Items.Clear();
            try
            {
                //Sorts the files by last update time for user convenience
                filteredFolderPaths = filteredFolderPaths.OrderByDescending(f => new DirectoryInfo(f).LastWriteTime).ToList();
                filteredFolderNames = filteredFolderNames.Select(path => Path.GetFileName(path)).ToList();

                foreach (string name in filteredFolderNames)
                {
                    schematicsListBox.Items.Add(name);
                }
                schematicsListBox.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void GetImmediateSubfolders(string folderPath, out List<string> folderNames, out List<string> folderPaths)
        {
            folderNames = new List<string>();
            folderPaths = new List<string>();

            try
            {
                // Get all immediate subfolders
                string[] subfolders = Directory.GetDirectories(folderPath);

                foreach (string subfolder in subfolders)
                {
                    // Extract folder name and path
                    string folderName = Path.GetFileName(subfolder);

                    // Check if the folder name is 7 or 8 numbers long
                    if (IsNumeric(folderName) && (folderName.Length == 7 || folderName.Length == 8))
                    {
                        folderNames.Add(folderName);

                        // Add to lists
                        folderPaths.Add(subfolder);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Helper method to check if a string is numeric
        private static bool IsNumeric(string str)
        {
            return int.TryParse(str, out _);
        }


        private void schematicsListBox_DoubleClick_1(object sender, EventArgs e)
        {
            // Get the selected index
            int selectedIndex = schematicsListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < folderPaths.Count)
            {
                // Get the corresponding folder path
                string selectedFolderPath = folderPaths[selectedIndex];

                // Search for the latest .txt file containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");

                if (txtFiles.Length > 0)
                {
                    

                    if(txtFiles.Length == 1)
                    {
                        // Sort files by creation time and get the latest one
                        string latestTxtFile = txtFiles.OrderByDescending(f => new FileInfo(f).CreationTime).First();
                        ExtractAndOpenExcel(latestTxtFile);
                        // Do something with the latest .txt file, for example, display its path
                        Console.WriteLine($"Latest .txt file in {selectedFolderPath} is: {latestTxtFile}");
                    }
                    else 
                    {
                        ShowDifferentVersions(txtFiles);
                    }
                    
                }
                else
                {
                    MessageBox.Show($"No matching .txt files found in {selectedFolderPath}");
                }
            }
        }

        private void ShowDifferentVersions(string[] foundVersions)
        {
            try
            {
                foreach (string version in foundVersions)
                {
                    loadedVersionPaths.Add(version);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(version);
                    string modifiedFileName = fileNameWithoutExtension.Replace("_DSI", "");

                    versionsListBox.Items.Add(modifiedFileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async void ExtractAndOpenExcel(string textFilePath)
        {
            try
            {
                // Show a loading indicator or notify the user about the ongoing operation
                // (e.g., display a ProgressBar or disable UI controls)

                // Run the time-consuming code asynchronously
                await Task.Run(() =>
                {
                    extractedWires = extractor.ExtractWiresFromFile(textFilePath);
                    extractedComponents = extractor.ExtractComponentsFromFile(textFilePath);
                    extractedBundles = extractor.ExtractBundlesFromFile(textFilePath);

                    convertor = new WCSPP_Convertor(extractedWires, extractedComponents);

                    convertor.ConvertListToWCSPPExcelFile(extractedWires, extractedComponents, extractedBundles);
                });

                // Update the UI or perform any post-operation tasks
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void versionsListBox_DoubleClick(object sender, EventArgs e)
        {
            // Get the selected index
            int selectedIndex = versionsListBox.SelectedIndex;
            int selectedSchematicIndex = schematicsListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < loadedVersionPaths.Count)
            {
                // Get the corresponding folder path
                string selectedFolderPath = folderPaths[selectedSchematicIndex];

                // Search for the .txt files containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");
                string selectedVersion = txtFiles[selectedSchematicIndex];

                if (selectedVersion != null)
                {
                    // Extract and open Excel with the selected index
                    ExtractAndOpenExcel(selectedVersion);
                    Console.WriteLine($"Opening .txt file at index {selectedIndex} in {selectedFolderPath}");
                }
                else
                {
                    MessageBox.Show($"Invalid index selected.");
                }
            }
            else
            {
                MessageBox.Show($"Invalid index or folder path.");
            }
        }

        private void searchSchematicTextBox_TextChanged(object sender, EventArgs e)
        {
            // Get the search text
            string searchText = searchSchematicTextBox.Text.ToLower(); // Convert to lowercase for case-insensitive comparison

            // Filter folderPaths and folderNames based on the search text
            List<string> filteredFolderPaths = folderPaths
                .Where(path => Path.GetFileName(path).ToLower().Contains(searchText))
                .ToList();

            List<string> filteredFolderNames = folderNames
                .Where(name => name.ToLower().Contains(searchText))
                .ToList();

            // Call AddNamesToListBox with the filtered lists
            AddNamesToListBox(filteredFolderPaths, filteredFolderNames);
        }

        private void searchVersionTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
