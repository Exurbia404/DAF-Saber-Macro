using Logging;
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
    public partial class ChoiceForm : Form
    {
        private Logger _logger;
        private PanelForm panelForm;

        public ChoiceForm(Logger logger, PanelForm panelform)
        {
            InitializeComponent();
            _logger = logger;
            panelForm = panelform;

        }

        private void goToBundlesButton_Click(object sender, EventArgs e)
        {
            BundlesForm bundleForm = new BundlesForm(_logger, panelForm);

            // Set the newProfileForm's TopLevel property to false
            bundleForm.TopLevel = false;

            (panelForm.Controls["panel"] as Panel).Controls.Clear();

            // Add the newProfileForm to the panel's controls of the parent form
            (panelForm.Controls["panel"] as Panel).Controls.Add(bundleForm);

            bundleForm.Dock = DockStyle.Fill;

            bundleForm.Show();
        }

        private void goToProjectsButton_Click(object sender, EventArgs e)
        {
            ProjectForm projectForm = new ProjectForm(_logger, panelForm);

            // Set the newProfileForm's TopLevel property to false
            projectForm.TopLevel = false;

            (panelForm.Controls["panel"] as Panel).Controls.Clear();

            // Add the newProfileForm to the panel's controls of the parent form
            (panelForm.Controls["panel"] as Panel).Controls.Add(projectForm);

            projectForm.Dock = DockStyle.Fill;

            projectForm.Show();
        }
    }
}
