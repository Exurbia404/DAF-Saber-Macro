using Data_Access;
using Logging;
using Logic;
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
    public partial class ProjectForm : Form
    {
        private RefSetHandler refsetHandler;
        private List<DSI_Reference> extractedReferences;
        private Logger _logger;
        private PanelForm panelForm;
        
        private string ProductionBuildOfMaterialsFolder = @"J:\SaberRelease\Production";


        public ProjectForm(Logger logger, PanelForm panelform)
        {
            InitializeComponent();

            _logger = logger;
            panelForm = panelform;

            refsetHandler = new RefSetHandler(_logger);

            extractedReferences = LoadRefSets();
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

        private void returnToProjectsButton_Click(object sender, EventArgs e)
        {
            List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
            AddSchematicsToListBox(schematicNames);
            currentProjectLabel.Text = "Select project";
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

        private void schematicsListBox_Click(object sender, EventArgs e)
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
                Extractor extractor = new Extractor(_logger);

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
    }
}
