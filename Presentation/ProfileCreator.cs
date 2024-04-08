using System.Data;
using Logging;
using Logic_Layer;
using Newtonsoft.Json;
using Point = System.Drawing.Point;

namespace Presentation
{
    public partial class ProfileCreator : Form
    {
        private List<ComboBox> comboBoxes;
        private List<Label> comboBox_Labels;

        //Distance between buttons
        private int horizontalOffset = 120;
        private int headerCounter = 3;

        //Used by buttons, labels and comboBoxes
        private int initalXOffset = 210;
        private ProfileController profileController;
        public ProfileCreator(Logger logger)
        {
            InitializeComponent();

            profileController = new ProfileController();

            comboBoxes = new List<ComboBox>();
            comboBox_Labels = new List<Label>();

            SetDefaultComboBox();
            GenerateComboBoxes(headerCounter);
        }

        private void SetDefaultComboBox()
        { 
            foreach (string profileName in profileController.defaultProfiles.Keys)
            {
                profileTypeComboBox.Items.Add(profileName);
            }
        }

        private void GenerateComboBoxes(int headerCount)
        {
            RemoveOldHeadersAndButtons(headerCount);

            int x = initalXOffset; // Initial x-coordinate
            int y = 60;  // Initial y-coordinate
            
            int initialOffset = horizontalOffset * comboBoxes.Count;

            // Iterate through each header
            for (int i = comboBoxes.Count(); i < headerCount; i++)
            {

                // Create a new ComboBox
                ComboBox comboBox = new ComboBox();

                // Set ComboBox location
                comboBox.Location = new Point((initalXOffset + (horizontalOffset * i)), y);
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
            AddOptionsToComboBoxes(profileController.defaultProfiles);
            MoveHeaderButtons(headerCount);
        }


        private void GenerateLabels(int headerCount)
        {
            int labelX = initalXOffset; // Initial x-coordinate
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

            int x = initalXOffset + ((headerCount * horizontalOffset) + 10); // Initial x-coordinate for header buttons (added 10 for extra offset)
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
            if (profileController.defaultProfiles.Count > 0)
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
            if (profileController.defaultProfiles.ContainsKey(selectedProfileName))
            {
                // Get the dictionary associated with the selected profile name
                selectedProfileDictionary = new Dictionary<string, List<string>>();
                selectedProfileDictionary.Add(selectedProfileName, profileController.defaultProfiles[selectedProfileName]);

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
                if (profileController.defaultProfiles.ContainsKey(profileName))
                {
                    // Overwrite the existing profile
                    profileController.defaultProfiles[profileName] = GetComboBoxesValues();
                }
                else
                {
                    // Create a new dictionary for the profile with an empty list
                    List<string> newProfile = GetComboBoxesValues();

                    // Add the new profile to the userProfiles dictionary
                    profileController.defaultProfiles.Add(profileName, newProfile);
                    profilesListBox.Items.Add(profileName);
                }

                // Optionally, you can clear the text in the profileNameTextBox

                UnselectAllComboBoxes();
                profileNameTextBox.Clear();

                profileController.SaveProfiles();
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
                    profileController.defaultProfiles.Remove(selectedProfileName);
                    profilesListBox.Items.Remove(selectedProfileName);

                    // Optionally, you can clear the selection in the profileTypeComboBox
                    profileTypeComboBox.SelectedIndex = -1;
                    profileController.SaveProfiles();

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
            if (profileController.defaultProfiles.ContainsKey(loadedProfileName))
            {
                // Retrieve the dictionary corresponding to the loaded profile name
                List<string> loadedProfile = profileController.defaultProfiles[loadedProfileName];

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
