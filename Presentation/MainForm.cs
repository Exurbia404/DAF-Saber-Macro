using Logic;
using OfficeOpenXml.FormulaParsing.Logging;
using System.Data;
using System.Diagnostics;
using DSI_Component = Logic.DSI_Component;
using Logging;
using Data_Access;
using OfficeOpenXml;
using UI_Interfaces;
using Microsoft.Graph.Models.TermStore;

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
        private RefSetHandler refsetHandler;

        private ExcelImporter excelImporter;
        private ExcelExporter excelHandler;

        private List<DSI_Wire> extractedWires;
        private List<DSI_Component> extractedComponents;
        private List<Bundle> extractedBundles;
        private List<DSI_Reference> extractedReferences;
        private List<DSI_Tube> extractedTubes;

        private string computerName = Environment.MachineName;
        private PanelForm panelForm;

        public MainForm(Logger logger, PanelForm panelform)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            panelForm = panelform;
            _logger = logger;
            InitializeComponent();


            //Commented out for replacement with localSettingsFiles
            //excelImporter = new ExcelImporter(_logger);
            extractor = new Extractor(_logger);
            excelHandler = new ExcelExporter(_logger);
            refsetHandler = new RefSetHandler(_logger);

            folderPaths = new List<string>();

            //Commented out for replacement with localSettingsFiles
            //extractedReferences = excelImporter.DSIReferences;
            extractedReferences = LoadRefSets();

            //These are the buttons for toggling the working directory:
            //Bundles:
            bundlesToggleButtons.Add(productProtoButton);
            bundlesToggleButtons.Add(reldasButton);
            bundlesToggleButtons.Add(designerButton);

            //Projects
            projectsToggleButtons.Add(releasedButton);
            searchBundlesTextBox_SetText();
            schematicsSearchTextBox_SetText();

            try
            {
                if (computerName == "EXURBIA")
                {
                    DesignerBuildOfMaterialsFolder = LocalBuildOfMaterialsFolder;
                }
                folderPaths = GetImmediateSubfolders(DesignerBuildOfMaterialsFolder);
                AddNamesToBundlesListBox(folderPaths);

                List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
                AddSchematicsToListBox(schematicNames);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }

            _logger.Log("current directory is: WiP");
        }



        private void AddNamesToBundlesListBox(IEnumerable<string> filteredFolderPaths)
        {
            bundlesListBox.Items.Clear();
            try
            {
                // Sort the folder paths by creation date
                //TODO: fix this, it never sorts by creationtime
                filteredFolderPaths = filteredFolderPaths.OrderByDescending(f => Directory.GetCreationTime(f)).ToList();

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
            try
            {
                // Get the corresponding folder path
                string selectedFolderPath = GetFilePath(bundleName);

                // Search for the latest .txt file containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");
                SetStatusBar(30);
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
                Extractor_Copy copyExtractor = new Extractor_Copy(_logger);

                copyExtractor.ExtractBundleFromFilePath(textFilePath);

                extractedBundles = copyExtractor.Bundles;
                extractedTubes = extractor.ExtractDSITubes(textFilePath);

                FileHandler fileHandler = new FileHandler(_logger);

                convertor = new WCSPP_Convertor(extractedWires, extractedComponents, fileHandler);

                List<Converted_Component> convertedComponents = copyExtractor.Components;
                List<Converted_Wire> convertedWires = copyExtractor.Wires;


                string bundleNumber = GetFileName(textFilePath);
                string filePath = GetFolderPath(textFilePath);

                fileHandler.WriteToFile(convertedWires.Cast<Data_Interfaces.iConverted_Wire>().ToList(), convertedComponents.Cast<Data_Interfaces.iConverted_Component>().ToList(), extractedBundles.Cast<Data_Interfaces.iBundle>().ToList(), bundleNumber, filePath);

                ProfileChoiceForm pcForm = new ProfileChoiceForm(_logger, bundleNumber);

                // Set the newProfileForm's TopLevel property to false
                pcForm.TopLevel = false;

                (panelForm.Controls["panel"] as Panel).Controls.Clear();

                // Add the newProfileForm to the panel's controls of the parent form
                (panelForm.Controls["panel"] as Panel).Controls.Add(pcForm);

                pcForm.Dock = DockStyle.Fill;

                pcForm.SetBundleData(convertedWires, convertedComponents, extractedTubes);

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
                // Get the directory name containing the file
                string directoryName = Path.GetDirectoryName(filePath);

                // Get the directory name from the full path
                string folderName = new DirectoryInfo(directoryName).Name;

                // Extract the number from the folder name
                string number = new String(folderName.Where(Char.IsDigit).ToArray());

                return number;
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

            //Check if a project or a yearweek has been selected
            if ((selectedSchematic != null) && (!IsRefSetNumber(selectedSchematic)))
            {
                // Filter the extractedReferences based on the selected ProjectName
                List<string> bundleNumbers = extractedReferences
                .Where(reference => reference.ProjectName == selectedSchematic)
                .Select(reference => $"{reference.YearWeek} - {reference.BundleNumber}")
                .ToList();

                // Call AddSchematicsToListBox with the list of BundleNumbers
                currentProjectLabel.Text = selectedSchematic;
                _logger.Log("Loading project: " + selectedSchematic);

                AddSchematicsToListBox(bundleNumbers);
            }
            else if (IsRefSetNumber(selectedSchematic))
            {
                _logger.Log("Loading following schematic :" + selectedSchematic);
                OpenRefSetInExcel(TrimString(selectedSchematic));
            }
        }

        //Gets the value after the - and returns it
        private string TrimString(string input)
        {
            // Find the index of '-' character
            int dashIndex = input.IndexOf('-');

            // If '-' is found and it's not the last character
            if (dashIndex != -1 && dashIndex != input.Length - 1)
            {
                // Extract the part after '-' character
                return input.Substring(dashIndex + 1).Trim();
            }

            // If '-' is not found or it's the last character, return null
            return null;
        }

        private bool IsRefSetNumber(string selectedSchematic)
        {
            // Trim the selectedSchematic to remove leading and trailing whitespace
            string trimmedSchematic = selectedSchematic.Trim();

            // Call TrimString to extract the part after '-' character
            string bundleNumber = TrimString(trimmedSchematic);

            // If bundleNumber is not null, check if it contains only numeric characters
            if (bundleNumber != null)
            {
                return bundleNumber.All(char.IsDigit);
            }

            // If bundleNumber is null, return false
            return false;
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

                    Project_ExtractAndOpenExcel(latestCompFile, latestWiresFile, selectedSchematic);
                    // Do something with the latest .txt file, for example, display its path
                }
            }
        }

        private void Project_ExtractAndOpenExcel(string compFilePath, string wiresFilePath, string bundleNumber)
        {
            try
            {
                _logger.Log("Opening project in Excel");
                List<Project_Wire> projectWire = extractor.Project_ExtractWiresFromWireFile(wiresFilePath);
                List<Project_Component> projectComponent = extractor.Project_ExtractComponentFromComponentFile(compFilePath);


                ProfileChoiceForm pcForm = new ProfileChoiceForm(_logger, bundleNumber);

                // Set the newProfileForm's TopLevel property to false
                pcForm.TopLevel = false;

                (panelForm.Controls["panel"] as Panel).Controls.Clear();

                // Add the newProfileForm to the panel's controls of the parent form
                (panelForm.Controls["panel"] as Panel).Controls.Add(pcForm);

                pcForm.Dock = DockStyle.Fill;

                pcForm.SetProjectData(projectWire, projectComponent);

                pcForm.Show();
            }
            catch
            {

            }
           }


        //Buttons to choose directory:
        private void BundlesToggleButton(Button clickedButton)
        {
            // Toggle the clicked button to its opposite state
            clickedButton.BackColor = clickedButton.BackColor == Color.Gray ? Color.White : Color.Gray;

            // Set other toggle buttons to their default color
            foreach (Button button in bundlesToggleButtons)
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
            _logger.Log("current directory is: Production/Proto");
            BundlesToggleButton(productProtoButton);

            folderPaths = GetImmediateSubfolders(ProductionBuildOfMaterialsFolder);
            AddNamesToBundlesListBox(folderPaths);
        }

        private void reldasButton_Click(object sender, EventArgs e)
        {
            _logger.Log("current directory is: Release");
            BundlesToggleButton(reldasButton);

            folderPaths = GetImmediateSubfolders(ReldasBuildOfMaterialsFolder);
            AddNamesToBundlesListBox(folderPaths);
        }

        private void designerButton_Click(object sender, EventArgs e)
        {
            _logger.Log("current directory is: WiP");
            BundlesToggleButton(designerButton);

            folderPaths = GetImmediateSubfolders(DesignerBuildOfMaterialsFolder);
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

        }





        //When the refset form is closed you should update the refsets present in the document
        private void RefSetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            extractedReferences = LoadRefSets();

            List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
            AddSchematicsToListBox(schematicNames);
        }

        private List<DSI_Reference> LoadRefSets()
        {
            try
            {
                _logger.Log("refsets found: " + refsetHandler.LoadRefSets().Count.ToString());
                //Check whether or not the refsets are empty
                if (refsetHandler.LoadRefSets().Count == 0)
                {
                    ExcelImporter excelImporter = new ExcelImporter(_logger);
                    _logger.Log("refsets found in Excel: " + excelImporter.DSIReferences.Count.ToString());
                    return excelImporter.DSIReferences;
                }
                return refsetHandler.LoadRefSets();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                return null;
            }

        }

        private void SetStatusBar(int percentage)
        {
            //if (progressBar.InvokeRequired)
            //{
            //    // If the current thread is not the UI thread, invoke this method on the UI thread
            //    progressBar.BeginInvoke(new Action<int>(SetStatusBar), percentage);
            //}
            //else
            //{
            //    // If the current thread is the UI thread, update the progress bar directly
            //    progressBar.Value = percentage;
            //}
        }

        private void programStatusButton_Click(object sender, EventArgs e)
        {

        }



        private void bundlesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void returnToProjectsButton_Click(object sender, EventArgs e)
        {
            List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
            AddSchematicsToListBox(schematicNames);
            currentProjectLabel.Text = "Select project";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
