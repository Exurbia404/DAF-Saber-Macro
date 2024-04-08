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
        private ProfileController profiles;

        private string filename;

        private List<Converted_Component> converted_components;
        private List<Converted_Wire> converted_wires;

        private List<Project_Component> project_components;
        private List<Project_Wire> project_wires;

        public ProfileChoiceForm(Logger logger, string fileName)
        {
            InitializeComponent();
            exporter = new ExcelExporter(logger);
            profiles = new ProfileController();


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

        private void ExportBundleToExcel()
        {
            List<string> placeHolder = new List<string>();
            List<iConverted_Wire> wiresToExport = converted_wires.Cast<iConverted_Wire>().ToList();
            List<iConverted_Component> componentsToExport = converted_wires.Cast<iConverted_Component>().ToList();
    
            exporter.CreateExcelSheet(wiresToExport, componentsToExport, filename, placeHolder);
        }

        private void ExportProjectToExcel()
        {
            List<iProject_Wire> wiresToExport = converted_wires.Cast<iProject_Wire>().ToList();
            List<IProject_Component> componentsToExport = converted_wires.Cast<IProject_Component>().ToList();

            exporter.CreateProjectExcelSheet(wiresToExport, componentsToExport, filename);
        }
    }
}
