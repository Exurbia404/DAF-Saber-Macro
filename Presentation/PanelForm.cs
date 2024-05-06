using Logging;
using Microsoft.Kiota.Http.Generated;
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
    public partial class PanelForm : Form
    {
        private Logger _logger;
        private int messageCounter;
        private MessageViewer messageViewerForm;


        private string version = "Beta 1.3";

        public PanelForm(Logger logger)
        {
            InitializeComponent();

            _logger = logger;
            _logger.Log($"Computer Name: {Environment.MachineName}");
            _logger.LogEvent += Logger_LogEvent;

            messageCounter = 0;
            versionLabel.Text = "Version: " + version;

            ChoiceForm choiceForm = new ChoiceForm(_logger, this);
            choiceForm.TopLevel = false;

            panel.Controls.Add(choiceForm);
            choiceForm.Show();

            AutoSizeControls();
            //MainForm mainForm = new MainForm(_logger, this);
            //mainForm.TopLevel = false;

            //panel.Controls.Add(mainForm);
            //mainForm.Show();
            _logger.Log(this.Size.ToString());
            _logger.Log(panel.Size.ToString());
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
                    lastMessageTextBox.Text = message;
                }));
            }
            else
            {
                // If the current thread is the UI thread, update the programStatusButton.Text directly
                programStatusButton.Text = messageCounter.ToString();
                lastMessageTextBox.Text = message;
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(_logger, this);
            settingsForm.TopLevel = false;

            panel.Controls.Clear();
            panel.Controls.Add(settingsForm);
            settingsForm.Show();
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

        private void homeButton_Click(object sender, EventArgs e)
        {
            ChoiceForm choiceForm = new ChoiceForm(_logger, this);
            choiceForm.TopLevel = false;

            panel.Controls.Clear();
            panel.Controls.Add(choiceForm);
            choiceForm.Show();
        }

        private void panel_ControlAdded(object sender, ControlEventArgs e)
        {
            // Check if the added control is a ProfileCreator form
            if (e.Control is ProfileCreator profileCreatorForm)
            {
                // Adjust the size of the panelForm and the panel when ProfileCreator is present
                this.Size = new Size(980, 691); // Adjust as needed
                panel.Size = new Size(940, 540); // Adjust as needed
            }
            else
            {
                //Set default sizes
                this.Size = new Size(480, 691);
                panel.Size = new Size(440, 540);
            }
        }

        private void AutoSizeControls()
        {
            int FixedControlTop = 605;

            // Adjust the position of controls
            label2.Top = FixedControlTop;
            lastMessageTextBox.Top = FixedControlTop;
            versionLabel.Top = FixedControlTop;
        }
    }
}
