using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access;
using Logging;
using Logic;

namespace Presentation
{
    public partial class RefSetForm : Form
    {
        //private SettingsHandler settingsHandler

        private Logger _logger;

        private static List<DSI_Reference> extractedReferences;
        private ExcelImporter excelImporter;

        public RefSetForm(Logger logger)
        {
            InitializeComponent();

            _logger = logger;
            //excelImporter = new ExcelImporter(_logger);

            //This will load from DATASET
            //extractedReferences = excelImporter.DSIReferences;

            extractedReferences = LoadRefSets();

            List<string> schematicNames = extractedReferences.Select(reference => reference.ProjectName).ToList();
            LoadProjectsListBox(schematicNames);
        }

        private void addReferenceButton_Click(object sender, EventArgs e)
        {
            DSI_Reference newReference = new DSI_Reference(yearWeekTextBox.Text, bundleNumberTextBox.Text, projectNameTextBox.Text, descriptionTextBox.Text);
            extractedReferences.Add(newReference);

            SaveRefSets(extractedReferences);

            // Get the selected ProjectName from schematicsListBox
            string selectedSchematic = projectsListBox.SelectedItem?.ToString();

            // Filter the extractedReferences based on the selected ProjectName
            List<DSI_Reference> filteredReferences = extractedReferences
                .Where(reference => reference.ProjectName == selectedSchematic)
                .ToList();

            // Call AddSchematicsToListBox with the list of BundleNumbers
            LoadRefSetsListBox(filteredReferences);
        }

        private void deleteReferenceButton_Click(object sender, EventArgs e)
        {
            // Check if any item is selected in the ListBox
            if (projectsListBox.SelectedItem != null)
            {
                // Get the selected reference
                DSI_Reference selectedReference = new DSI_Reference(
                    yearWeekTextBox.Text,
                    bundleNumberTextBox.Text,
                    projectNameTextBox.Text,
                    descriptionTextBox.Text
                );

                // Remove the selected reference from extractedReferences
                extractedReferences.RemoveAll(reference =>
                    reference.YearWeek == selectedReference.YearWeek &&
                    reference.BundleNumber == selectedReference.BundleNumber &&
                    reference.ProjectName == selectedReference.ProjectName &&
                    reference.Description == selectedReference.Description
                );

                // Save the updated reference sets
                SaveRefSets(extractedReferences);

                // Reload the projects ListBox with the updated references

                // Get the selected ProjectName from schematicsListBox
                string selectedSchematic = projectsListBox.SelectedItem?.ToString();

                // Filter the extractedReferences based on the selected ProjectName
                List<DSI_Reference> filteredReferences = extractedReferences
                    .Where(reference => reference.ProjectName == selectedSchematic)
                    .ToList();

                // Call AddSchematicsToListBox with the list of BundleNumbers
                LoadRefSetsListBox(filteredReferences);
            }
            else
            {
                MessageBox.Show("Please select a project to delete.", "No Project Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadProjectsListBox(List<string> foundReferences)
        {
            projectsListBox.Items.Clear();
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
                        projectsListBox.Items.Add(reference);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
        }

        private void projectsListBox_DoubleClick(object sender, EventArgs e)
        {
            // Get the selected ProjectName from schematicsListBox
            string selectedSchematic = projectsListBox.SelectedItem?.ToString();

            // Filter the extractedReferences based on the selected ProjectName
            List<DSI_Reference> filteredReferences = extractedReferences
                .Where(reference => reference.ProjectName == selectedSchematic)
                .ToList();

            // Call AddSchematicsToListBox with the list of BundleNumbers
            LoadRefSetsListBox(filteredReferences);

            projectNameTextBox.Text = selectedSchematic;
        }

        private void LoadRefSetsListBox(List<DSI_Reference> references)
        {
            referencesListBox.Items.Clear();
            try
            {
                // Use a HashSet to store unique ProjectNames
                HashSet<DSI_Reference> uniqueProjectNames = new HashSet<DSI_Reference>();

                foreach (DSI_Reference reference in references)
                {
                    // Check if the ProjectName is not already in the HashSet
                    if (uniqueProjectNames.Add(reference))
                    {
                        // If it's a new ProjectName, add it to the ListBox
                        referencesListBox.Items.Add(reference);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
            }
        }

        private void SaveRefSets(List<DSI_Reference> references)
        {
            // Convert list of DSI_Reference objects to a list of strings
            List<string> referenceStrings = references.Select(reference => $"{reference.YearWeek}:{reference.BundleNumber}:{reference.ProjectName}:{reference.Description}").ToList();

            // Join the list of strings into a single string with a delimiter
            string serializedReferences = string.Join(";", referenceStrings);

            // Save the serialized references to application settings
            Properties.Settings.Default.RefSet = serializedReferences;

            // Save the changes to the settings
            Properties.Settings.Default.Save();
        }

        private List<DSI_Reference> LoadRefSets()
        {
            List<DSI_Reference> references = new List<DSI_Reference>();

            // Start the stopwatch
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
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
                                _logger.Log($"Error creating DSI_Reference object: {ex.Message}");
                            }
                        }
                        else
                        {
                            // Handle incorrect format error
                            _logger.Log("Invalid reference format: " + referenceString);
                        }
                    }
                }
            }
            finally
            {
                // Stop the stopwatch
                stopwatch.Stop();

                // Log the elapsed time in milliseconds
                _logger.Log($"LoadRefSets function executed in {stopwatch.ElapsedMilliseconds} ms");
            }

            return references;
        }

        private void referencesListBox_DoubleClick(object sender, EventArgs e)
        {
            // Check if any item is selected in the ListBox
            if (referencesListBox.SelectedItem != null)
            {
                // Get the selected reference
                DSI_Reference selectedReference = (DSI_Reference)referencesListBox.SelectedItem;

                // Set the text of text boxes based on the selected reference
                yearWeekTextBox.Text = selectedReference.YearWeek;
                bundleNumberTextBox.Text = selectedReference.BundleNumber;
                projectNameTextBox.Text = selectedReference.ProjectName;
                descriptionTextBox.Text = selectedReference.Description;
            }
        }
    }
}
