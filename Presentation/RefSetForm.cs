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
        private RefSetHandler refsetHandler;

        public RefSetForm(Logger logger)
        {
            InitializeComponent();

            _logger = logger;
            refsetHandler = new RefSetHandler(_logger);
            //excelImporter = new ExcelImporter(_logger);

            //This will load from DATASET
            //extractedReferences = excelImporter.DSIReferences;
            //refsetHandler.SaveRefSets(extractedReferences);

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
            refsetHandler.SaveRefSets(references);
        }

        private List<DSI_Reference> LoadRefSets()
        {
            return refsetHandler.LoadRefSets();
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
