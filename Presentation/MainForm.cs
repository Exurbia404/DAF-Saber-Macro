using Logic;
using OfficeOpenXml.FormulaParsing.Logging;
using System.Data;
using System.Diagnostics;
using Component = Logic.Component;
using Logging;
using Data_Access;
using OfficeOpenXml;

namespace Presentation
{
    public partial class MainForm : Form
    {
        private string ProductionBuildOfMaterialsFolder = @"J:\SaberRelease\Production";
        private string ReldasBuildOfMaterialsFolder = @"J:\SaberRelease\Designer\Boms";
        private string DesignerBuildOfMaterialsFolder = @"J:\SaberWiP\2_Users\designs\BSA\Boms";

        private string LocalBuildOfMaterialsFolder = @"C:\Users\tomvh\Documents\School\S5 - Internship\boms";

        private List<string> folderPaths;
        private List<Button> bundlesToggleButtons = new List<Button>();
        private List<Button> projectsToggleButtons = new List<Button>();

        private Extractor extractor;
        private WCSPP_Convertor convertor;
        private Logger _logger;

        private ExcelImporter excelImporter;
        private ExcelHandler excelHandler;

        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static List<Bundle> extractedBundles;
        private static List<DSI_Reference> extractedReferences;



        public MainForm(Logger logger)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            _logger = logger;
            InitializeComponent();

            string computerName = Environment.MachineName;
            _logger.Log($"Computer Name: {computerName}");

            //Commented out for replacement with localSettingsFiles
            //excelImporter = new ExcelImporter(_logger);
            extractor = new Extractor(_logger);
            excelHandler = new ExcelHandler(_logger);

            folderPaths = new List<string>();
            //extractedReferences = excelImporter.DSIReferences;
            extractedReferences = LoadRefSets();

            //These are the buttons for toggling the working directory:
            //Bundles:
            bundlesToggleButtons.Add(productProtoButton);
            bundlesToggleButtons.Add(reldasButton);
            bundlesToggleButtons.Add(designerButton);

            //Projects
            projectsToggleButtons.Add(wipButton);
            projectsToggleButtons.Add(releasedButton);

            searchBundlesTextBox_SetText();
            schematicsSearchTextBox_SetText();

            try
            {
                if (computerName == "EXURBIA")
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
                _logger.Log($"Error: {ex.Message}");
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
                _logger.Log($"Error: {ex.Message}");
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
                _logger.Log($"Error: {ex.Message}");
            }
        }

        private async Task<List<string>> GetImmediateSubfoldersAsync(string folderPath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> folderPaths = new List<string>();

            try
            {
                // Get all immediate subfolders asynchronously
                string[] subfolders = await Task.Run(() => Directory.GetDirectories(folderPath));

                // Process subfolders in parallel
                await Task.WhenAll(subfolders.Select(async subfolder =>
                {
                    try
                    {
                        // Check if the folder has any files

                        // Extract folder name and path
                        string folderName = Path.GetFileName(subfolder);

                        // Check if the folder name is 7 or 8 numbers long
                        if (IsNumeric(folderName) && (folderName.Length == 7 || folderName.Length == 8))
                        {
                            // Add to list (ensure thread-safe access)
                            lock (folderPaths)
                            {
                                folderPaths.Add(subfolder);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Log($"Error processing folder '{subfolder}': {ex.Message}");
                    }
                }));

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

        // Helper method to check if a string is numeric
        private static bool IsNumeric(string str)
        {
            return int.TryParse(str, out _);
        }

        private void bundlesListBox_DoubleClick(object sender, EventArgs e)
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
                _logger.Log($"Latest .txt file in {selectedFolderPath} is: {latestTxtFile}");
            }
            else
            {
                _logger.Log($"No matching .txt files found in {selectedFolderPath}");
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
                _logger.Log($"Error: {ex.Message}");
            }
        }

        private void searchBundlesTextBox_Enter(object sender, EventArgs e)
        {
            if (searchBundlesTextBox.ForeColor == Color.Black)
                return;
            searchBundlesTextBox.Text = "";
            searchBundlesTextBox.ForeColor = Color.Black;
        }

        private void searchBundlesTextBox_SetText()
        {
            searchBundlesTextBox.Text = "Search:";
            searchBundlesTextBox.ForeColor = Color.Gray;
        }

        private void searchBundlesTextBox_Leave(object sender, EventArgs e)
        {
            if (searchBundlesTextBox.Text.Trim() == "")
                searchBundlesTextBox_SetText();
        }

        private void schematicsSearchTextBox_Enter(object sender, EventArgs e)
        {
            if (schematicsSearchTextBox.ForeColor == Color.Black)
                return;
            schematicsSearchTextBox.Text = "";
            schematicsSearchTextBox.ForeColor = Color.Black;
        }

        private void schematicsSearchTextBox_Leave(object sender, EventArgs e)
        {
            if (schematicsSearchTextBox.Text.Trim() == "")
                schematicsSearchTextBox_SetText();
        }

        private void schematicsSearchTextBox_SetText()
        {
            schematicsSearchTextBox.Text = "Search:";
            schematicsSearchTextBox.ForeColor = Color.Gray;
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

        private void schematicsListBox_DoubleClick(object sender, EventArgs e)
        {
            // Get the selected ProjectName from schematicsListBox
            string selectedSchematic = schematicsListBox.SelectedItem?.ToString();

            if ((selectedSchematic != null) && (!IsRefSetNumber(selectedSchematic)))
            {
                // Filter the extractedReferences based on the selected ProjectName
                List<string> bundleNumbers = extractedReferences
                    .Where(reference => reference.ProjectName == selectedSchematic)
                    .Select(reference => reference.BundleNumber)
                    .ToList();

                // Call AddSchematicsToListBox with the list of BundleNumbers
                AddSchematicsToListBox(bundleNumbers);
            }
            else if (IsRefSetNumber(selectedSchematic))
            {
                OpenRefSetInExcel(selectedSchematic);
            }
        }

        private bool IsRefSetNumber(string selectedSchematic)
        {
            // Check if the string contains only numeric characters and has a length of 7 or 8
            return selectedSchematic.All(char.IsDigit) && (selectedSchematic.Length == 7 || selectedSchematic.Length == 8);
        }

        private void OpenRefSetInExcel(string selectedSchematic)
        {
            // Construct the full path to the folder based on the ProductionBuildOfMaterialsFolder
            string folderPath = Path.Combine(ProductionBuildOfMaterialsFolder, selectedSchematic);

            // Check if the folder exists
            if (Directory.Exists(folderPath))
            {
                // Search for _comp.txt and _wires.txt files in the folder
                string[] compFilePaths = Directory.GetFiles(folderPath, "*_comp*.txt");
                string[] wiresFilePaths = Directory.GetFiles(folderPath, "*_wires*.txt");

                if ((compFilePaths.Length > 0) && (wiresFilePaths.Length > 0))
                {
                    // Sort files by creation time and get the latest one
                    string latestCompFile = compFilePaths.OrderByDescending(f => new FileInfo(f).CreationTime).First();
                    string latestWiresFile = wiresFilePaths.OrderByDescending(f => new FileInfo(f).CreationTime).First();

                    Project_ExtractAndOpenExcel(latestCompFile, latestWiresFile);
                    // Do something with the latest .txt file, for example, display its path
                }
            }
        }

        private void Project_ExtractAndOpenExcel(string compFilePath, string wiresFilePath)
        {
            List<Project_Component> foundComponents = extractor.Project_ExtractComponentFromComponentFile(compFilePath);
            List<Project_Wire> foundWires = extractor.Project_ExtractWiresFromWireFile(wiresFilePath);

            excelHandler.CreateProjectExcelSheet(foundWires, foundComponents);
        }

        //
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

        //Projects:
        private void ProjectsToggleButton(Button clickedButton)
        {
            // Toggle the clicked button to its opposite state
            clickedButton.BackColor = clickedButton.BackColor == Color.Gray ? Color.White : Color.Gray;

            // Set other toggle buttons to their default color
            foreach (Button button in projectsToggleButtons)
            {
                if (button != clickedButton)
                {
                    button.BackColor = Color.Gray;
                }
            }
        }

        private void releasedButton_Click(object sender, EventArgs e)
        {
            ProjectsToggleButton(releasedButton);
        }

        private void wipButton_Click(object sender, EventArgs e)
        {
            ProjectsToggleButton(wipButton);
        }

        private void goToProfilesButton_Click(object sender, EventArgs e)
        {
            var newProfileForm = new ProfileCreator(_logger);
            newProfileForm.Show();
        }

        private void openRefSetFormButton_Click(object sender, EventArgs e)
        {
            var RefSetForm = new RefSetForm(_logger);
            
            // Subscribe to the FormClosed event
            RefSetForm.FormClosed += RefSetForm_FormClosed;

            RefSetForm.Show();
        }

        //When the refset form is closed you should update the refsets present in the document
        private void RefSetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Call your function here
            extractedReferences = LoadRefSets();

            List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
            AddSchematicsToListBox(schematicNames);
        }

        //TODO: this should be a class!
        private List<DSI_Reference> LoadRefSets()
        {
            List<DSI_Reference> references = new List<DSI_Reference>();

            // Retrieve the serialized references from application settings
            string serializedReferences = Properties.Settings.Default.RefSet;

            if (!string.IsNullOrEmpty(serializedReferences))
            {
                // Split the serialized references string by the delimiter
                string[] referenceStrings = serializedReferences.Split(';');

                foreach (string referenceString in referenceStrings)
                {
                    // Split each reference string into its components
                    string[] parts = referenceString.Split(':');
                    if (parts.Length == 4) // Ensure all components are present
                    {
                        try
                        {
                            // Create a DSI_Reference object from the parts
                            DSI_Reference reference = new DSI_Reference
                            {
                                YearWeek = parts[0],
                                BundleNumber = parts[1],
                                ProjectName = parts[2],
                                Description = parts[3]
                            };
                            references.Add(reference);
                        }
                        catch (Exception ex)
                        {
                            // Handle any exceptions that occur during object creation
                            Console.WriteLine($"Error creating DSI_Reference object: {ex.Message}");
                        }
                    }
                    else
                    {
                        // Handle incorrect format error
                        Console.WriteLine("Invalid reference format: " + referenceString);
                    }
                }
            }

            return references;
        }
    }
}
