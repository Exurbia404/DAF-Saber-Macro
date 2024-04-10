using Data_Access;
using Logging;
using Logic;
using Logic_Layer;
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

        public ProfileChoiceForm(Logger logger, string filename)
        {
            InitializeComponent();
            fileName = filename;
            exporter = new ExcelExporter(logger);
            profileController = new ProfileController(new FileHandler(logger));

            profileList = profileController.allProfiles;
            SetProfilesToComboBoxes();
        }

        public void SetBundleData(List<Converted_Wire> wires, List<Converted_Component> components)
        {
            converted_components = components;
            converted_wires = wires;
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

        private void ExportBundleToExcel(List<Profile> profiles)
        {
            List<iConverted_Wire> wiresToExport = converted_wires.Cast<iConverted_Wire>().ToList();
            List<iConverted_Component> componentsToExport = converted_components.Cast<iConverted_Component>().ToList();

            exporter.CreateExcelSheet(wiresToExport, componentsToExport, fileName, profiles);
        }

        private void ExportProjectToExcel()
        {
            List<iProject_Wire> wiresToExport = project_wires.Cast<iProject_Wire>().ToList();
            List<IProject_Component> componentsToExport = project_components.Cast<IProject_Component>().ToList();

            exporter.CreateProjectExcelSheet(wiresToExport, componentsToExport, fileName);
        }

        private void exportToExcelButton_Click(object sender, EventArgs e)
        {
            // Get selected profile names from combo boxes
            string selectedWireProfileName = wireProfilesComboBox.SelectedItem?.ToString();
            string selectedComponentProfileName = componentProfilesComboBox.SelectedItem?.ToString();

            // Find selected profiles from the profile list
            Profile selectedWireProfile = profileList.FirstOrDefault(p => p.Name == selectedWireProfileName);
            Profile selectedComponentProfile = profileList.FirstOrDefault(p => p.Name == selectedComponentProfileName);

            // Create a list containing wires at index 0 and components at index 1
            List<Profile> selectedProfiles = new List<Profile> { selectedWireProfile, selectedComponentProfile };

            // Call the method to export to Excel with the selected profiles
            ExportBundleToExcel(selectedProfiles);
        }
    }
}
