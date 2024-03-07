using Section5PoC.DAL;
using Section5PoC.Logic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private ExcelImporter excelImporter;

        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static List<Bundle> extractedBundles;
        private static List<DSI_Reference> extractedReferences;

        public Form1()
        {
            InitializeComponent();

            string computerName = Environment.MachineName;
            Console.WriteLine($"Computer Name: {computerName}");

            excelImporter = new ExcelImporter();
            extractor = new Extractor();

            folderNames = new List<string>();
            folderPaths = new List<string>();
            loadedVersionPaths = new List<string>();
            extractedReferences = excelImporter.DSIReferences;

            searchBundlesTextBox_SetText();

            try
            {
                if(computerName == "EXURBIA")
                {
                    BuildOfMaterialsFolder = LocalBuildOfMaterialsFolder;
                }
                GetImmediateSubfolders(BuildOfMaterialsFolder, out folderNames, out folderPaths);
                AddNamesToListBox(folderPaths, folderNames);

                List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
                AddSchematicsToListBox(schematicNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

            

        private void AddNamesToListBox(List<string> filteredFolderPaths, List<string> filteredFolderNames)
        {
            bundlesListBox.Items.Clear();
            try
            {
                //Sorts the files by last update time for user convenience
                filteredFolderPaths = filteredFolderPaths.OrderByDescending(f => new DirectoryInfo(f).LastWriteTime).ToList();
                filteredFolderNames = filteredFolderNames.Select(path => Path.GetFileName(path)).ToList();

                foreach (string name in filteredFolderNames)
                {
                    bundlesListBox.Items.Add(name);
                }
                bundlesListBox.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void AddSchematicsToListBox(List<string> foundReferences)
        {
            schematicsListBox.Items.Clear();
            try
            {
                // Use a HashSet to store unique ProjectNames
                HashSet<string> uniqueProjectNames = new HashSet<string>();

                foreach (string reference in foundReferences)
                {
                    // Check if the ProjectName is not already in the HashSet
                    if (uniqueProjectNames.Add(reference))
                    {
                        // If it's a new ProjectName, add it to the ListBox
                        schematicsListBox.Items.Add(reference);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GetImmediateSubfolders(string folderPath, out List<string> folderNames, out List<string> folderPaths)
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
            int selectedIndex = bundlesListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < folderPaths.Count)
            {
                // Get the corresponding folder path
                string selectedFolderPath = folderPaths[selectedIndex];

                // Search for the latest .txt file containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");

                if (txtFiles.Length > 0)
                {
                    // Sort files by creation time and get the latest one
                    string latestTxtFile = txtFiles.OrderByDescending(f => new FileInfo(f).CreationTime).First();
                    ExtractAndOpenExcel(latestTxtFile);
                    // Do something with the latest .txt file, for example, display its path
                    Console.WriteLine($"Latest .txt file in {selectedFolderPath} is: {latestTxtFile}");
                }
                else
                {
                    MessageBox.Show($"No matching .txt files found in {selectedFolderPath}");
                }
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

        private void searchBundlesTextBox_Enter(object sender, EventArgs e)
        {
            if (searchBundlesTextBox.ForeColor == Color.Black)
                return;
            searchBundlesTextBox.Text = "";
            searchBundlesTextBox.ForeColor = Color.Black;
        }

        protected void searchBundlesTextBox_SetText()
        {
            searchBundlesTextBox.Text = "Search:";
            searchBundlesTextBox.ForeColor = Color.Gray;
        }

        private void searchBundlesTextBox_Leave(object sender, EventArgs e)
        {
            if (searchBundlesTextBox.Text.Trim() == "")
                searchBundlesTextBox_SetText();
        }

        private void searchBundlesTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchText;
            // Get the search text
            if(searchBundlesTextBox.Text == "Search:")
            {
                searchText = "";
            }
            else
            {
                searchText = searchBundlesTextBox.Text.ToLower(); // Convert to lowercase for case-insensitive comparison
            }
             
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

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            bundlesListBox.ClearSelected();
        }

        private void schematicsSearchTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void schematicsListBox_DoubleClick(object sender, EventArgs e)
        {
            // Get the selected ProjectName from schematicsListBox
            string selectedSchematic = schematicsListBox.SelectedItem?.ToString();

            if (selectedSchematic != null)
            {
                // Filter the extractedReferences based on the selected ProjectName
                List<string> bundleNumbers = extractedReferences
                    .Where(reference => reference.ProjectName == selectedSchematic)
                    .Select(reference => reference.BundleNumber)
                    .ToList();

                // Call AddSchematicsToListBox with the list of BundleNumbers
                AddSchematicsToListBox(bundleNumbers);
            }
        }
    }
}
