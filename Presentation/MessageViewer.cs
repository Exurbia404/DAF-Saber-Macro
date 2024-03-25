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
        public MessageViewer(List<string> messages)
        {
            InitializeComponent();

            // Add each message as a separate item in the ListBox
            messagesListBox.Items.AddRange(messages.ToArray());
        }
    }
}
