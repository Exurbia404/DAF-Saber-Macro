using Section5PoC.DAL;
using Section5PoC.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private string ProductionBuildOfMaterialsFolder = @"J:\SaberRelease\Production";
        private string ReldasBuildOfMaterialsFolder = @"J:\SaberRelease\Designer\Boms";
        private string DesignerBuildOfMaterialsFolder = @"J:\SaberWiP\2_Users\designs\BSA\Boms";

        private string LocalBuildOfMaterialsFolder = @"C:\Users\tomvh\Documents\School\S5 - Internship\boms";

        private List<string> folderPaths;

        private Extractor extractor;
        private WCSPP_Convertor convertor;
        private ExcelImporter excelImporter;

        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static List<Bundle> extractedBundles;
        private static List<DSI_Reference> extractedReferences;

        private List<System.Windows.Forms.Button> bundlesToggleButtons = new List<System.Windows.Forms.Button>();
        private List<System.Windows.Forms.Button> projectsToggleButtons = new List<System.Windows.Forms.Button>();

        public Form1()
        {
            InitializeComponent();

            string computerName = Environment.MachineName;
            Console.WriteLine($"Computer Name: {computerName}");

            excelImporter = new ExcelImporter();
            extractor = new Extractor();

            folderPaths = new List<string>();
            extractedReferences = excelImporter.DSIReferences;

            //These are the buttons for toggling the working directory:
            //Bundles:
            bundlesToggleButtons.Add(productProtoButton);
            bundlesToggleButtons.Add(reldasButton);
            bundlesToggleButtons.Add(designerButton);

            //Projects
            projectsToggleButtons.Add(wipButton);
            projectsToggleButtons.Add(releasedButton);

            searchBundlesTextBox_SetText();

            try
            {
                if(computerName == "EXURBIA")
                {
                    DesignerBuildOfMaterialsFolder = LocalBuildOfMaterialsFolder;
                }
                folderPaths = Task.Run(() => GetImmediateSubfoldersAsync(DesignerBuildOfMaterialsFolder)).Result;
                AddNamesToBundlesListBox(folderPaths);

                List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
                AddSchematicsToListBox(schematicNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



        private void AddNamesToBundlesListBox(IEnumerable<string> filteredFolderPaths)
        {
            bundlesListBox.Items.Clear();
            try
            {
                // Sorts the folder names by their numeric value in descending order
                filteredFolderPaths = filteredFolderPaths.OrderByDescending(f => int.Parse(Path.GetFileName(f))).ToList();

                foreach (string fullPath in filteredFolderPaths)
                {
                    string folderName = Path.GetFileName(fullPath);
                    bundlesListBox.Items.Add(folderName);
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

        private async Task<List<string>> GetImmediateSubfoldersAsync(string folderPath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> folderPaths = new List<string>();

            try
            {
                await Task.Run(() =>
                {
                    // Get all immediate subfolders
                    string[] subfolders = Directory.GetDirectories(folderPath);

                    foreach (string subfolder in subfolders)
                    {
                        // Check if the folder has any files
                        
                        // Extract folder name and path
                        string folderName = Path.GetFileName(subfolder);

                        // Check if the folder name is 7 or 8 numbers long
                        if (IsNumeric(folderName) && (folderName.Length == 7 || folderName.Length == 8))
                        {
                            // Add to lists
                            folderPaths.Add(subfolder);
                        }
                        
                    }
                });

                stopwatch.Stop();
                Console.WriteLine("Folders retrieved in: " + stopwatch.Elapsed.TotalMilliseconds + "ms");
                return folderPaths;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
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
            string selectedBundle = bundlesListBox.SelectedItem.ToString();

            OpenLatestBundleFile(selectedBundle);
            
        }

        private void OpenLatestBundleFile(string bundleName)
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
                Console.WriteLine($"Latest .txt file in {selectedFolderPath} is: {latestTxtFile}");
            }
            else
            {
                MessageBox.Show($"No matching .txt files found in {selectedFolderPath}");
            }
        }

        private string GetFilePath(string inputName)
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
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            bundlesListBox.ClearSelected();
            if (extractedReferences != null)
            {
                List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
                AddSchematicsToListBox(schematicNames);
            }
            
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
                    searchText = searchBundlesTextBox.Text.ToLower(); // Convert to lowercase for case-insensitive comparison
                }

                // Filter folderPaths based on the search text
                IEnumerable<string> filteredFolderPaths = folderPaths
                    .Where(path => Path.GetFileName(path).IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1);

                // If there's only one option, open the latest bundle file
                if (filteredFolderPaths.Count() == 1)
                {
                    OpenLatestBundleFile(filteredFolderPaths.First());
                }
                else
                {
                    // Call AddNamesToListBox with the filtered lists
                    AddNamesToBundlesListBox(filteredFolderPaths);
                }
            }            
        }



        //Buttons to choose directory:
        private void BundlesToggleButton(System.Windows.Forms.Button clickedButton)
        {
            // Toggle the clicked button to its opposite state
            clickedButton.BackColor = clickedButton.BackColor == Color.Gray ? Color.White : Color.Gray;

            // Set other toggle buttons to their default color
            foreach (System.Windows.Forms.Button button in bundlesToggleButtons)
            {
                if (button != clickedButton)
                {
                    button.BackColor = Color.Gray;
                }
            }
        }


        //Bundles
        private void productProtoButton_Click(object sender, EventArgs e)
        {
            BundlesToggleButton(productProtoButton);

            folderPaths = Task.Run(() => GetImmediateSubfoldersAsync(ProductionBuildOfMaterialsFolder)).Result;
            AddNamesToBundlesListBox(folderPaths);
        }

        private void reldasButton_Click(object sender, EventArgs e)
        {
            BundlesToggleButton(reldasButton);

            folderPaths = Task.Run(() => GetImmediateSubfoldersAsync(ReldasBuildOfMaterialsFolder)).Result;
            AddNamesToBundlesListBox(folderPaths);
        }

        private void designerButton_Click(object sender, EventArgs e)
        {
            BundlesToggleButton(designerButton);

            folderPaths = Task.Run(() => GetImmediateSubfoldersAsync(DesignerBuildOfMaterialsFolder)).Result;
            AddNamesToBundlesListBox(folderPaths);
        }

        private void ProjectsToggleButton(System.Windows.Forms.Button clickedButton)
        {
            // Toggle the clicked button to its opposite state
            clickedButton.BackColor = clickedButton.BackColor == Color.Gray ? Color.White : Color.Gray;

            // Set other toggle buttons to their default color
            foreach (System.Windows.Forms.Button button in projectsToggleButtons)
            {
                if (button != clickedButton)
                {
                    button.BackColor = Color.Gray;
                }
            }
        }

        //Projects:

        private void releasedButton_Click(object sender, EventArgs e)
        {
            ProjectsToggleButton(releasedButton);
        }

        private void wipButton_Click(object sender, EventArgs e)
        {
            ProjectsToggleButton(wipButton);
        }
    }
}
