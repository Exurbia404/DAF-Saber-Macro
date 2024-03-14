using DocumentFormat.OpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart.ChartEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Section5PoC.Presentation
{
    public partial class ProfileCreator : Form
    {
        //Default profiles, user can create their own based on these
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

        private Dictionary<string, List<string>> defaultProfiles;
        private Dictionary<string, List<string>> userProfiles;
        private List<ComboBox> comboBoxes;
        private List<Label> comboBox_Labels;

        //Distance between buttons
        private int horizontalOffset = 120;
        private int headerCounter = 3;

        public ProfileCreator()
        {
            InitializeComponent();

            profileController = new ProfileController();
            defaultProfiles = new Dictionary<string, List<string>>();
            userProfiles = new Dictionary<string, List<string>>();
            comboBoxes = new List<ComboBox>();
            comboBox_Labels = new List<Label>();


            LoadDefaultProfiles();


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

            defaultProfiles.Add("Wire_WCSPP_Profile", wireWCSPPProfile);
            defaultProfiles.Add("Wire_Profile", wireProfile);
            defaultProfiles.Add("Wire_Project_Profile", wireProjectProfile);
            defaultProfiles.Add("Component_WCSPP_Profile", componentWCSPPProfile);
            defaultProfiles.Add("Component_Profile", componentProfile);
            defaultProfiles.Add("Component_Project_Profile", componentProjectProfile);

            foreach (string profileName in defaultProfiles.Keys)
            {
                profileTypeComboBox.Items.Add(profileName);
            }
        }

        private void ProfileCreator_Load(object sender, EventArgs e)
        {

        }

        private void GenerateComboBoxes(int headerCount)
        {
            RemoveOldHeadersAndButtons(headerCount);

            int x = 175; // Initial x-coordinate
            int y = 60;  // Initial y-coordinate
            
            int initialOffset = horizontalOffset * comboBoxes.Count;

            // Iterate through each header
            for (int i = comboBoxes.Count(); i < headerCount; i++)
            {

                // Create a new ComboBox
                ComboBox comboBox = new ComboBox();

                // Set ComboBox location
                comboBox.Location = new Point((x + initialOffset), y);
                comboBox.Size = new Size(horizontalOffset, 25);

                // Set ComboBox DropDownStyle to DropDownList
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                // Add ComboBox to the list
                comboBoxes.Add(comboBox);

                // Increment x-coordinate for the next ComboBox
                x += horizontalOffset; // horizontalOffset is assumed to be set elsewhere

                // Add ComboBox to the appropriate container in your UI (e.g., a panel or form)
                this.Controls.Add(comboBox);
                comboBox.Show();
            }

            GenerateLabels(headerCount);
            AddOptionsToComboBoxes(defaultProfiles);
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
                label.Text = ((char)('A' + i)).ToString();
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


        private void RemoveOldHeadersAndButtons(int headerCount)
        {
            // Remove ComboBoxes and Labels that are outside the range of headerCount
            for (int i = comboBoxes.Count - 1; i >= headerCount; i--)
            {
                comboBoxes[i].Dispose();
                comboBoxes.RemoveAt(i);
            }

            for (int j = comboBox_Labels.Count - 1; j >= headerCount; j--)
            {
                comboBox_Labels[j].Dispose();
                comboBox_Labels.RemoveAt(j);
            }
        }

        private void AddOptionsToComboBoxes(Dictionary<string, List<string>> profile)
        {
            if (defaultProfiles.Count > 0)
            {
                // Get the first KeyValuePair from the dictionary
                KeyValuePair<string, List<string>> keyValueProfile = profile.First();
                foreach(ComboBox comboBox in comboBoxes)
                {
                    comboBox.Items.Clear();
                    // Add items from the first list to the ComboBox
                    foreach (string item in keyValueProfile.Value)
                    {
                        comboBox.Items.Add(item);
                    }
                }
            }
        }

        private int AmountOfSelectedComboBoxes()
        {
            int count = 0;
            foreach (ComboBox comboBox in comboBoxes)
            {
                // Check if an item is selected in the ComboBox
                if (comboBox.SelectedItem != null)
                {
                    count++;
                }
            }
            return count;
        }

        private List<string> GetComboBoxesValues()
        {
            //Update headerCounter as other methods also rely on this
            headerCounter = AmountOfSelectedComboBoxes();
            
            RemoveOldHeadersAndButtons(headerCounter);
            MoveHeaderButtons(headerCounter);


            List<string> retrievedValues = new List<string>();

            foreach (ComboBox comboBox in comboBoxes)
            {
                // Check if an item is selected in the ComboBox
                if (comboBox.SelectedItem != null)
                {
                    retrievedValues.Add(comboBox.SelectedItem.ToString());
                }
                else
                {
                    // If nothing is selected, return null
                    return null;
                }
            }

            return retrievedValues;
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

        private void profileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> selectedProfileDictionary;
            string selectedProfileName = profileTypeComboBox.SelectedItem.ToString();

            // Check if the selected profile name exists in the dictionary
            if (defaultProfiles.ContainsKey(selectedProfileName))
            {
                // Get the dictionary associated with the selected profile name
                selectedProfileDictionary = new Dictionary<string, List<string>>();
                selectedProfileDictionary.Add(selectedProfileName, defaultProfiles[selectedProfileName]);

                // Call AddOptionsToComboBoxes with the selected dictionary
                AddOptionsToComboBoxes(selectedProfileDictionary);
            }
        }

        private void UnselectAllComboBoxes()
        {
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.SelectedIndex = -1; // Set the selected index to -1 to clear the selection
            }
        }

        private void saveProfileButton_Click(object sender, EventArgs e)
        {
            // Get the name from profileNameTextBox
            string profileName = profileNameTextBox.Text.Trim();

            // Check if the profile name is not empty
            if (!string.IsNullOrWhiteSpace(profileName))
            {
                // Check if the profile name already exists in the userProfiles dictionary
                if (userProfiles.ContainsKey(profileName))
                {
                    // Overwrite the existing profile
                    userProfiles[profileName] = GetComboBoxesValues();
                }
                else
                {
                    // Create a new dictionary for the profile with an empty list
                    List<string> newProfile = GetComboBoxesValues();

                    // Add the new profile to the userProfiles dictionary
                    userProfiles.Add(profileName, newProfile);
                    profilesListBox.Items.Add(profileName);
                }

                // Optionally, you can clear the text in the profileNameTextBox

                UnselectAllComboBoxes();
                profileNameTextBox.Clear();
            }
            else
            {
                // Optionally, show an error message if the profile name is empty
                MessageBox.Show("Please enter a profile name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteProfileButton_Click(object sender, EventArgs e)
        {
            // Get the selected profile name from the profileTypeComboBox
            string selectedProfileName = profileNameTextBox.Text.Trim();

            // Check if a profile is selected
            if (!string.IsNullOrEmpty(selectedProfileName))
            {
                // Confirm with the user before deleting the profile
                DialogResult result = MessageBox.Show($"Are you sure you want to delete the profile '{selectedProfileName}'?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Remove the selected profile from the userProfiles dictionary
                    userProfiles.Remove(selectedProfileName);
                    profilesListBox.Items.Remove(selectedProfileName);

                    // Optionally, you can clear the selection in the profileTypeComboBox
                    profileTypeComboBox.SelectedIndex = -1;
                }
            }
            else
            {
                // Optionally, display an error message if no profile is selected
                MessageBox.Show("Please select a profile to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(profilesListBox.SelectedItem != null)
            {
                LoadProfileToComboBoxes(profilesListBox.SelectedItem.ToString());
            }
        }

        private void LoadProfileToComboBoxes(string loadedProfileName)
        {
            
            // Set the profile name in profileNameTextBox
            profileNameTextBox.Text = loadedProfileName;

            // Check if the loaded profile exists in userProfiles
            if (userProfiles.ContainsKey(loadedProfileName))
            {
                // Retrieve the dictionary corresponding to the loaded profile name
                List<string> loadedProfile = userProfiles[loadedProfileName];

                GenerateComboBoxes(loadedProfile.Count);

                // Ensure that the loadedProfile count matches the number of comboBoxes
                if (loadedProfile.Count == comboBoxes.Count)
                {
                    // Iterate through each ComboBox in comboBoxes
                    for (int i = 0; i < comboBoxes.Count; i++)
                    {
                        // Set the selected item in the ComboBox
                        comboBoxes[i].SelectedItem = loadedProfile[i]; // Assign the value from loadedProfile directly
                    }
                }
                else
                {
                    MessageBox.Show("The number of values in the loaded profile does not match the number of ComboBoxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
