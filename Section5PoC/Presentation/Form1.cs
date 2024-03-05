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

        private List<string> folderNames;
        private List<string> folderPaths;

        private Extractor extractor;
        private WCSPP_Convertor convertor;

        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static List<Bundle> extractedBundles;

        public Form1()
        {
            extractor = new Extractor();
            folderNames = new List<string>();
            folderPaths = new List<string>();
            try
            {
                InitializeComponent();
                GetImmediateSubfolders(BuildOfMaterialsFolder, out folderNames, out folderPaths);
                AddNamesToListBox();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

            

        private void AddNamesToListBox()
        {
            try
            {
                schematicsListBox.Items.Add("test");
                foreach (string name in folderNames)
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
                    folderNames.Add(folderName);
                    Console.WriteLine(folderName);

                    // Add to lists
                    folderPaths.Add(subfolder);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
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

            if (selectedIndex >= 0 && selectedIndex < folderPaths.Count)
            {
                // Get the corresponding folder path
                string selectedFolderPath = folderPaths[selectedIndex];

                // Search for the .txt files containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");

                if (selectedIndex < txtFiles.Length)
                {
                    // Extract and open Excel with the selected index
                    ExtractAndOpenExcel(txtFiles[selectedIndex]);
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
    }
}
