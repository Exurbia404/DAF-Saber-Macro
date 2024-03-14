using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Section5PoC.Presentation
{
    public partial class ProfileCreator : Form
    {
        private enum Wire_WCSPP_Profile_Options
        {
            Wire,
            Diameter,
            Color,
            Type,
            Code_no,
            Length,
            Connector_1,
            Port_1,
            Term_1,
            Seal_1,
            Wire_connection,
            Term_2,
            Seal_2,
            Connector_2,
            Port_2,
            Variant,
            Bundle,
            Loc_1,
            Loc_2,
        }
        private enum Wire_Profile_Options
        {
            WireName,
            WireOption,
            WireType,
            Color,
            CrossSectionalArea,
            Material,
            UserModule,
            MulticoreName,
            End1NodeName,
            End1Route,
            End1Cavity,
            End1MaterialCode,
            End2NodeName,
            End2Route,
            End2Cavity,
            End2MaterialCode,
            IncludeOnBOM,
            IncludeOnChart,
            WireTag,
            WireNote,
            WireLengthChangeType,
            WireLengthChangeValue,
            AssemblyItemNumber,
            MulticoreOption,
        }
        private enum Wire_Project_Profile_Options
        {
            Wire,
            Diameter,
            Color,
            Type,
            Connector_1,
            Port_1,
            Term_1,
            Seal_1,
            Lokation_1,
            Wire_connection,
            Term_2,
            Seal_2,
            Connector_2,
            Port_2,
            Lokation_2,
            Harness,
            Variant,
            Bundle,
            Kodenr_wire,
            Tag,
        }

        private enum Component_WCSPP_Profile_Options
        {
            Name,
            Part_no,
            Passive,
            Instruction,
            Variant,
            Bundle,
            Description,
            Lokation,
            EndText,
        }
        private enum Component_Profile_Options
        {
            NodeName,
            CavityName,
            WireName,
            SequenceNumber,
            ComponentTypeCode,
            CircuitOption,
            ServiceFunction,
            Route,
            PartNumber1,
            Quantity,
            CrossSectionalArea,
            PartNumber2,
            PartNumber3,
            SelectTerminal,
            Seal,
            Plugged,
            BlockNumber,
            TerminationMethod,
            MaterialCode,
            ComponentTypeCode2,
        }
        private enum Component_Project_Profile_Options
        {

            Type,
            Ref,
            Description,
            Location,
            Connector,
            SecLock,
            Harness,
            Variant,
            Bundle,
            Tag,
            System,
            Fuse_value,
            Color,
            N_pins,
        }

        private ProfileController profileController;

        private Dictionary<string, List<string>> profiles;
        private List<ComboBox> comboBoxes;
        private List<Label> comboBox_Labels;

        //Distance between buttons
        private int horizontalOffset = 40;
        private int headerCounter = 3;

        public ProfileCreator()
        {
            profileController = new ProfileController();
            profiles = new Dictionary<string, List<string>>();
            comboBoxes = new List<ComboBox>();
            comboBox_Labels = new List<Label>();


            LoadDefaultProfiles();

            InitializeComponent();

            GenerateComboBoxes(headerCounter);
        }

        private void LoadDefaultProfiles()
        {
            //Programatically load in each enum (if the enum changes you don't have to worry about this)
            var wireWCSPPProfile = Enum.GetValues(typeof(Wire_WCSPP_Profile_Options))
                                        .Cast<Wire_WCSPP_Profile_Options>()
                                        .Select(option => option.ToString())
                                        .ToList();

            var wireProfile = Enum.GetValues(typeof(Wire_Profile_Options))
                                  .Cast<Wire_Profile_Options>()
                                  .Select(option => option.ToString())
                                  .ToList();

            var wireProjectProfile = Enum.GetValues(typeof(Wire_Project_Profile_Options))
                                        .Cast<Wire_Project_Profile_Options>()
                                        .Select(option => option.ToString())
                                        .ToList();

            var componentWCSPPProfile = Enum.GetValues(typeof(Component_WCSPP_Profile_Options))
                                            .Cast<Component_WCSPP_Profile_Options>()
                                            .Select(option => option.ToString())
                                            .ToList();

            var componentProfile = Enum.GetValues(typeof(Component_Profile_Options))
                                      .Cast<Component_Profile_Options>()
                                      .Select(option => option.ToString())
                                      .ToList();

            var componentProjectProfile = Enum.GetValues(typeof(Component_Project_Profile_Options))
                                              .Cast<Component_Project_Profile_Options>()
                                              .Select(option => option.ToString())
                                              .ToList();

            profiles.Add("Wire_WCSPP_Profile", wireWCSPPProfile);
            profiles.Add("Wire_Profile", wireProfile);
            profiles.Add("Wire_Project_Profile", wireProjectProfile);
            profiles.Add("Component_WCSPP_Profile", componentWCSPPProfile);
            profiles.Add("Component_Profile", componentProfile);
            profiles.Add("Component_Project_Profile", componentProjectProfile);
        }

        private void ProfileCreator_Load(object sender, EventArgs e)
        {

        }

        private void GenerateComboBoxes(int headerCount)
        {
            RemoveOldHeadersAndButtons();

            int x = 175; // Initial x-coordinate
            int y = 60;  // Initial y-coordinate

            // Iterate through each header
            for (int i = 0; i < headerCount; i++)
            {
                // Create a new ComboBox
                ComboBox comboBox = new ComboBox();

                // Set ComboBox location
                comboBox.Location = new Point(x, y);
                comboBox.Size = new Size(40, 25);

                // Add ComboBox to the list
                comboBoxes.Add(comboBox);

                // Increment x-coordinate for the next ComboBox
                x += horizontalOffset; // horizontalOffset is assumed to be set elsewhere

                // Add ComboBox to the appropriate container in your UI (e.g., a panel or form)
                this.Controls.Add(comboBox);
                comboBox.Show();
            }

            GenerateLabels(headerCount);
            MoveHeaderButtons(headerCount);
        }

        private void GenerateLabels(int headerCount)
        {
            int labelX = 175; // Initial x-coordinate
            int labelY = 40; // Initial y-coordinate for Labels

            // Iterate through each header
            for (int i = 0; i < headerCount; i++)
            {
                // Create a new Label
                Label label = new Label();

                // Set Label text to alphabet (A, B, C, ...)
                label.Text = ((char)('X' + i)).ToString();
                label.Size = new Size(25, 25);

                // Set Label location
                label.Location = new Point(labelX, labelY);

                // Add Label to the appropriate container in your UI (e.g., a panel or form)
                comboBox_Labels.Add(label);
                this.Controls.Add(label);
                label.Show();

                labelX += horizontalOffset;
            }
        }

        private void MoveHeaderButtons(int headerCount)
        {

            int x = 175 + ((headerCount * horizontalOffset) + 10); // Initial x-coordinate for header buttons (added 10 for extra offset)
            int y = 45; // y-coordinate for header buttons

            // Set the location for the Add Header Button
            removeHeaderButton.Location = new Point(x, y);

            // Increment x-coordinate for the Delete Header Button
            x += removeHeaderButton.Width + 10; // Assuming a gap of 10 pixels between buttons

            // Set the location for the Delete Header Button
            addHeaderButton.Location = new Point(x, y);
        }

        
        private void RemoveOldHeadersAndButtons()
        {
            //Get rid of all existing comboBoxes and labels in the list so duplicates cannot exist
            foreach(ComboBox comboBox in comboBoxes)
            {
                comboBox.Dispose();
            }

            foreach(Label label in comboBox_Labels)
            {
                label.Dispose();
            }
        }

        private void GetComboBoxesValues()
        {

        }

        private void saveProfileButton_Click(object sender, EventArgs e)
        {

        }

        private void removeHeaderButton_Click(object sender, EventArgs e)
        {
            //headerCounter cannot go into the negative
            if(headerCounter > 0)
            {
                headerCounter--;
                GenerateComboBoxes(headerCounter);
            }
            
        }

        private void addHeaderButton_Click(object sender, EventArgs e)
        {
            //Here you can set the maximum amount of headers
            if (headerCounter < 26)
            {
                headerCounter++;
                GenerateComboBoxes(headerCounter);
            }
        }
    }
}
