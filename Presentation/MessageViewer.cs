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
    public partial class MessageViewer : Form
    {
        private Logger _logger;
        public MessageViewer(Logger logger)
        {
            InitializeComponent();
            _logger = logger;
            _logger.LogEvent += Logger_LogEvent;

            // Add each message as a separate item in the ListBox
            messagesListBox.Items.AddRange(_logger.messages.ToArray());
            
        }

        // Method to handle the LogEvent and update the ListBox with new messages
        private void Logger_LogEvent(object sender, string message)
        {
            // Invoke on the UI thread to update the ListBox
            if (messagesListBox.InvokeRequired)
            {
                messagesListBox.BeginInvoke(new Action(() =>
                {
                    messagesListBox.Items.Add(message);
                }));
            }
            else
            {
                messagesListBox.Items.Add(message);
            }
        }

        private void MessageViewer_Resize(object sender, EventArgs e)
        {
            // Calculate the new size for messagesListBox
            int padding = 5;
            int newWidth = this.ClientSize.Width - (2 * padding);
            int newHeight = this.ClientSize.Height - (2 * padding);

            // Apply the new size to messagesListBox
            messagesListBox.Size = new Size(newWidth, newHeight);

            // Adjust the position of messagesListBox to consider the padding
            messagesListBox.Location = new Point(padding, padding);
        }
    }
}
