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
        private MessageViewer messageViewerForm;

        private ExcelImporter excelImporter;
        private ExcelExporter excelHandler;

        private static List<DSI_Wire> extractedWires;
        private static List<DSI_Component> extractedComponents;
        private static List<Bundle> extractedBundles;
        private static List<DSI_Reference> extractedReferences;
        
        private int messageCounter;
        private string version = "Alpha 0.1";

        public MainForm(Logger logger)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            _logger = logger;
            InitializeComponent();

            string computerName = Environment.MachineName;
            _logger.Log($"Computer Name: {computerName}");

            _logger.LogEvent += Logger_LogEvent;

            messageCounter = 0;

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

            versionLabel.Text = "Version: " + version;

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
            SetStatusBar(10);
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                _logger.Log(ex.ToString());
                return null;
            }            
        }

        private async void ExtractAndOpenExcel(string textFilePath)
        {
            try
            {
                SetStatusBar(60);
                // Run the time-consuming code asynchronously
                await Task.Run(() =>
                {
                    extractedWires = extractor.ExtractWiresFromFile(textFilePath);
                    extractedComponents = extractor.ExtractComponentsFromFile(textFilePath);
                    extractedBundles = extractor.ExtractBundlesFromFile(textFilePath);

                    ExcelExporter excelExporter = new ExcelExporter(_logger);

                    convertor = new WCSPP_Convertor(extractedWires, extractedComponents, excelExporter);
                    SetStatusBar(80);
                    convertor.ConvertListToWCSPPExcelFile(extractedWires, extractedComponents, extractedBundles);
                });
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

            excelHandler.CreateProjectExcelSheet(foundWires.Cast<iProject_Wire>().ToList(), foundComponents.Cast<IProject_Component>().ToList()); ;
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
            _logger.Log("productbutton_clicked");
            BundlesToggleButton(productProtoButton);

            folderPaths = GetImmediateSubfolders(ProductionBuildOfMaterialsFolder);
            AddNamesToBundlesListBox(folderPaths);
        }

        private void reldasButton_Click(object sender, EventArgs e)
        {
            _logger.Log("reldasbutton_clicked");
            BundlesToggleButton(reldasButton);

            folderPaths = GetImmediateSubfolders(ReldasBuildOfMaterialsFolder);
            AddNamesToBundlesListBox(folderPaths);
        }

        private void designerButton_Click(object sender, EventArgs e)
        {
            _logger.Log("designerButton_clicked");
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

        private void goToProfilesButton_Click(object sender, EventArgs e)
        {
            _logger.Log("goToProfilesButton_clicked");
            var newProfileForm = new ProfileCreator(_logger);
            newProfileForm.Show();
        }

        private void openRefSetFormButton_Click(object sender, EventArgs e)
        {
            _logger.Log("openrefSetFormButton_clicked");
            var RefSetForm = new RefSetForm(_logger);

            // Subscribe to the FormClosed event
            RefSetForm.FormClosed += RefSetForm_FormClosed;

            RefSetForm.Show();
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
                _logger.Log("refsets found in settings: " + refsetHandler.LoadRefSets().Count.ToString());
                //Check whether or not the refsets are empty
                if (refsetHandler.LoadRefSets().Count == 0)
                {
                    ExcelImporter excelImporter = new ExcelImporter(_logger);
                    _logger.Log("refsets found in Excel: " + excelImporter.DSIReferences.Count.ToString());
                    return excelImporter.DSIReferences;
                }
                return refsetHandler.LoadRefSets();
            }
            catch(Exception ex)
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
            // Check if MessageViewer form is open
            if (messageViewerForm != null && !messageViewerForm.IsDisposed)
            {
                messageViewerForm.Close(); // Close it
            }

            // Open MessageViewer form
            messageViewerForm = new MessageViewer(_logger);
            messageViewerForm.Show();
        }

        private void Logger_LogEvent(object sender, string message)
        {
            //get recent message count
            messageCounter = _logger.messages.Count;

            // Update programStatusButton.Text on the UI thread
            if (programStatusButton.InvokeRequired)
            {
                // If the current thread is not the UI thread, invoke this method on the UI thread
                programStatusButton.BeginInvoke(new Action(() =>
                {
                    programStatusButton.Text = messageCounter.ToString();
                }));
            }
            else
            {
                // If the current thread is the UI thread, update the programStatusButton.Text directly
                programStatusButton.Text = messageCounter.ToString();
            }
        }

        private void bundlesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
