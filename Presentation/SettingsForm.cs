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
    public partial class SettingsForm : Form
    {
        private Logger _logger;
        public SettingsForm(Logger logger)
        {
            InitializeComponent();

            _logger = logger;
            _logger.Log("Settings opened");
        }

        private void goToProfilesButton_Click(object sender, EventArgs e)
        {
            _logger.Log("goToProfilesButton_clicked");
            var newProfileForm = new ProfileCreator(_logger);
            newProfileForm.Show();
        }

        private void goToRefsetsButton_Click(object sender, EventArgs e)
        {
            _logger.Log("openrefSetFormButton_clicked");
            var RefSetForm = new RefSetForm(_logger);
            RefSetForm.Show();
        }
    }
}
