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
using System.Windows.Forms.Design;
using UI_Interfaces;

namespace Presentation
{
    public partial class ProfileChoiceForm : Form
    {

        private Logger _logger;
        private ExcelExporter exporter;
        private ProfileController profileController;
        private List<Profile> profileList;

        private string fileName;

        private List<Converted_Component> converted_components;
        private List<Converted_Wire> converted_wires;

        private List<Project_Component> project_components;
        private List<Project_Wire> project_wires;

        private List<DSI_Tube> tubesList;
        private List<Bundle> bundlesList;

        public ProfileChoiceForm(Logger logger, string filename)
        {
            _logger = logger;
            InitializeComponent();
            fileName = filename;
            exporter = new ExcelExporter(logger);
            profileController = new ProfileController(new FileHandler(logger));

            profileList = profileController.userProfiles;
            currentlyOpenedLabel.Text = "Opened: " + fileName;
            SetProfilesToComboBoxes();
        }

        public void SetBundleData(List<Converted_Wire> wires, List<Converted_Component> components, List<DSI_Tube> tubes, List<Bundle> bundles)
        {
            converted_components = components;
            converted_wires = wires;
            tubesList = tubes;
            bundlesList = bundles;
            SetBundleListBoxData(bundlesList);
        }

        private void SetBundleListBoxData(List<Bundle> bundles)
        {
            foreach (Bundle bundle in bundles)
            {
                bundlesListBox.Items.Add(bundle.VariantNumber);
            }

            //Auto set if there is only 1 bundle variant present i.e. non modularized
            if (bundlesListBox.Items.Count == 1)
            {
                bundlesListBox.SetSelected(0, true);
            }
        }

        public void SetProjectData(List<Project_Wire> wires, List<Project_Component> components)
        {
            project_components = components;
            project_wires = wires;
        }

        private void SetProfilesToComboBoxes()
        {
            foreach (Profile profile in profileList)
            {
                wireProfilesComboBox.Items.Add(profile.Name);
                componentProfilesComboBox.Items.Add(profile.Name);
            }
        }

        private void ExportBundleToExcel(List<Profile> profiles, List<Bundle> selectedBundles, List<bool> selectedSheets)
        {
            List<iConverted_Wire> wiresToExport = converted_wires.Cast<iConverted_Wire>().ToList();
            List<iConverted_Component> componentsToExport = converted_components.Cast<iConverted_Component>().ToList();

            exporter.CreateExcelSheet(wiresToExport, componentsToExport, fileName, profiles, tubesList, selectedBundles, selectedSheets);
        }

        private void ExportProjectToExcel(List<Profile> profile)
        {
            List<iProject_Wire> wiresToExport = project_wires.Cast<iProject_Wire>().ToList();
            List<IProject_Component> componentsToExport = project_components.Cast<IProject_Component>().ToList();

            exporter.CreateProjectExcelSheet(wiresToExport, componentsToExport, fileName, profile);
        }

        private void exportToExcelButton_Click(object sender, EventArgs e)
        {
            // Get selected profile names from combo boxes
            string selectedWireProfileName = wireProfilesComboBox.SelectedItem?.ToString();
            string selectedComponentProfileName = componentProfilesComboBox.SelectedItem?.ToString();

            // Find selected profiles from the profile list
            Profile selectedWireProfile = profileList.FirstOrDefault(p => p.Name == selectedWireProfileName);
            Profile selectedComponentProfile = profileList.FirstOrDefault(p => p.Name == selectedComponentProfileName);


            //If no user profiles have been selected use the defaults for bundles
            if (selectedWireProfile == null)
            {
                selectedWireProfile = profileController.defaultProfiles[0];
            }

            if (selectedComponentProfile == null)
            {
                selectedComponentProfile = profileController.defaultProfiles[3];
            }


            List<Bundle> selectedBundles = GetSelectedBundles();
            //_logger.Log(bundlesList.ToString());

            List<bool> selectedSheets = GetSelectedSheets();
            //_logger.Log(selectedSheets.ToString());

            // Create a list containing wires at index 0 and components at index 1
            List<Profile> selectedProfiles = new List<Profile> { selectedWireProfile, selectedComponentProfile };

            // Call the method to export to Excel with the selected profiles
            ExportBundleToExcel(selectedProfiles, selectedBundles, selectedSheets);
        }

        private List<Bundle> GetSelectedBundles()
        {
            List<Bundle> selectedBundles = new List<Bundle>();

            foreach (int selectedIndex in bundlesListBox.SelectedIndices)
            {
                // Get the VariantNumber from the ListBox
                string variantNumber = bundlesListBox.Items[selectedIndex].ToString();

                // Find the corresponding Bundle object with the matching VariantNumber
                Bundle bundle = FindBundleByVariantNumber(variantNumber);

                // Add the found Bundle to the list
                if (bundle != null)
                {
                    selectedBundles.Add(bundle);
                }
            }

            return selectedBundles;
        }

        // Helper method to find a Bundle by its VariantNumber
        private Bundle FindBundleByVariantNumber(string variantNumber)
        {
            // Assuming bundlesList is a List<Bundle> containing all Bundle objects
            return bundlesList.FirstOrDefault(bundle => bundle.VariantNumber == variantNumber);
        }

        private List<bool> GetSelectedSheets()
        {
            List<bool> selectedSheets = new List<bool>
            {
                //Get the selected sheets
                createPECheckBox.Checked,
                createRCCheckBox.Checked,
                createOCCheckBox.Checked
            };

            return selectedSheets;
        }

        private void exportProjectButton_Click(object sender, EventArgs e)
        {
            // Get selected profile names from combo boxes
            string selectedWireProfileName = wireProfilesComboBox.SelectedItem?.ToString();
            string selectedComponentProfileName = componentProfilesComboBox.SelectedItem?.ToString();

            // Find selected profiles from the profile list
            Profile selectedWireProfile = profileList.FirstOrDefault(p => p.Name == selectedWireProfileName);
            Profile selectedComponentProfile = profileList.FirstOrDefault(p => p.Name == selectedComponentProfileName);

            //If no user profiles have been selected use the defaults for projects
            if (selectedWireProfile == null)
            {
                selectedWireProfile = profileController.defaultProfiles[2];
            }

            if (selectedComponentProfile == null)
            {
                selectedComponentProfile = profileController.defaultProfiles[5];
            }

            // Create a list containing wires at index 0 and components at index 1
            List<Profile> selectedProfiles = new List<Profile> { selectedWireProfile, selectedComponentProfile };

            // Call the method to export to Excel with the selected profiles
            ExportProjectToExcel(selectedProfiles);
        }

        private void selectAllBundlesButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bundlesListBox.Items.Count; i++)
            {
                bundlesListBox.SetSelected(i, true);
            }
        }

        private void selectNoneBundlesButton_Click(object sender, EventArgs e)
        {
            bundlesListBox.ClearSelected();
        }

        private void saberCheckerButton_Click(object sender, EventArgs e)
        {
            SaberChecker saberChecker = new SaberChecker(_logger, converted_components, converted_wires);

            int totalTests = saberChecker.TestResults.Count;

            if (saberChecker.TestResults == null || totalTests == 0)
            {
                // No tests or results available
                testResultsTextBox.Text = "No test results available";
            }

            
            int succeededTests = saberChecker.TestResults.Count(result => result); // Counting true values

            double successPercentage = (double)succeededTests / totalTests * 100;
            double roundedPercentage = Math.Round(successPercentage, MidpointRounding.AwayFromZero);
            testResultsTextBox.Text = "Results: " + roundedPercentage + "% succeeded"; // Displaying rounded percentage
        }
    }
}
