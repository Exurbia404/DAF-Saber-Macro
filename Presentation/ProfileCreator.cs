using System.Data;
using Data_Access;
using Logging;
using Logic;
using Newtonsoft.Json;
using Point = System.Drawing.Point;
using Data_Interfaces;
using System.Security.Cryptography;

namespace Presentation
{
    public partial class ProfileCreator : Form
    {
        private List<ComboBox> comboBoxes;
        private List<Label> comboBox_Labels;

        //Distance between buttons
        private int horizontalOffset = 80;
        private int headerCounter;

        //Used by buttons, labels and comboBoxes
        private int initalXOffset = 260;
        private ProfileController profileController;
        private Profile loadedProfile;
        private PanelForm panelForm;

        public ProfileCreator(Logger logger)
        {
            InitializeComponent();

            profileController = new ProfileController(new FileHandler(logger));
            headerCounter = 3;
            comboBoxes = new List<ComboBox>();
            comboBox_Labels = new List<Label>();

            AddUserProfilesToListBox();

            SetDefaultComboBox();
            GenerateComboBoxes(headerCounter);
        }

        private void AddUserProfilesToListBox()
        {
            foreach (Profile profile in profileController.userProfiles)
            {
                profilesListBox.Items.Add(profile.Name);
            }
        }

        private void SetDefaultComboBox()
        {
            foreach (Profile profile in profileController.defaultProfiles)
            {
                profileTypeComboBox.Items.Add(profile.Name);
            }
        }

        private void GenerateComboBoxes(int headerCount)
        {
            List<string> currentSelection = GetComboBoxesValues();
            while(currentSelection.Count > headerCount)
            {
                currentSelection.RemoveAt(currentSelection.Count);
            }

            // Remove all existing ComboBoxes
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.Dispose();
            }
            comboBoxes.Clear();

            int verticalOffset = 80;
            int rows = 0; // Counter for rows
            int columns = 8; // Number of ComboBoxes in each row

            for (int i = 0; i < headerCount; i++)
            {
                // Calculate row and column indices
                int row = i / columns;
                int column = i % columns;

                // Calculate x and y coordinates based on row and column
                int x = initalXOffset + (horizontalOffset * column);
                int y = 70 + (row * verticalOffset);

                // Check if the ComboBox would be placed outside the form
                if (x + horizontalOffset > this.ClientSize.Width)
                {
                    // Reset x-coordinate to initalXOffset
                    x = initalXOffset;

                    // Increment row counter
                    rows++;

                    // Adjust y-coordinate based on the new row
                    y = 60 + (rows * verticalOffset);
                }

                // Create a new ComboBox
                ComboBox comboBox = new ComboBox
                {
                    // Set ComboBox location
                    Location = new Point(x, y),

                    // Set ComboBox size
                    Size = new Size(horizontalOffset, 25),

                    // Set ComboBox DropDownStyle to DropDownList
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                // Add ComboBox to the list
                comboBoxes.Add(comboBox);

                // Add ComboBox to the appropriate container in your UI (e.g., a panel or form)
                this.Controls.Add(comboBox);
                comboBox.Show();
            }

            // Generate labels and add options to ComboBoxes
            GenerateLabels(headerCount);
            AddOptionsToComboBoxes(profileController.defaultProfiles[0]);
            LoadInProfile(currentSelection);
        }

        private void LoadInProfile(List<string> profile) 
        { 
            if (profile != null)
            {
                for (int i = 0; i < profile.Count; i++)
                {
                    comboBoxes[i].SelectedItem = profile[i];
                }
            }
        }

        private void GenerateLabels(int headerCount)
        {
            int labelX = initalXOffset; // Initial x-coordinate
            int labelY = 40; // Initial y-coordinate for Labels
            int verticalOffset = 80;
            int oldCount = comboBox_Labels.Count;

            // If there are less labels required
            while (headerCount < oldCount)
            {
                int index = oldCount - 1;
                comboBox_Labels[index].Dispose();
                comboBox_Labels.RemoveAt(index);
                oldCount--;
            }

            int rows = 0; // Counter for rows
            int columns = 8; // Number of labels in each row

            // Iterate through each header
            for (int i = comboBox_Labels.Count; i < headerCount; i++)
            {
                // Calculate row and column indices
                int row = i / columns;
                int column = i % columns;

                // Calculate x and y coordinates based on row and column
                int x = initalXOffset + (horizontalOffset * column);
                int y = labelY + (row * verticalOffset);

                // Check if the label would be placed outside the form
                if (x + horizontalOffset > this.ClientSize.Width)
                {
                    // Reset x-coordinate to initalXOffset
                    x = initalXOffset;

                    // Increment row counter
                    rows++;

                    // Adjust y-coordinate based on the new row
                    y = labelY + (rows * verticalOffset);
                }

                // Create a new Label
                Label label = new Label();

                // Set Label text to alphabet (A, B, C, ...)
                label.Text = ((char)('A' + i)).ToString();
                label.Size = new Size(25, 25);

                // Set Label location
                label.Location = new Point(x, y);

                // Add Label to the appropriate container in your UI (e.g., a panel or form)
                comboBox_Labels.Add(label);
                this.Controls.Add(label);

                label.Show();

                // Update labelX for the next label
                labelX = x + horizontalOffset;
            }
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

        private void AddOptionsToComboBoxes(Profile profile)
        {
            if (profile != null)
            {
                List<string> parameters = profile.Parameters;

                foreach (ComboBox comboBox in comboBoxes)
                {
                    comboBox.Items.Clear();

                    // Add items from the profile parameters to the ComboBox
                    foreach (string parameter in parameters)
                    {
                        comboBox.Items.Add(parameter);
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
                else if (comboBox.SelectedItem == null)
                {
                    int currentIndex = comboBoxes.IndexOf(comboBox);
                    if((currentIndex + 1) < comboBoxes.Count)
                    {
                        if (comboBoxes[currentIndex + 1] != null)
                        {
                            count++;
                        }
                    }
                    
                }
            }
            return count;
        }

        private List<string> GetComboBoxesValues()
        {
            //Update headerCounter as other methods also rely on this
            //headerCounter = AmountOfSelectedComboBoxes();

            RemoveOldHeadersAndButtons(headerCounter);

            List<string> retrievedValues = new List<string>();

            foreach (ComboBox comboBox in comboBoxes)
            {
                // Check if an item is selected in the ComboBox
                if (comboBox.SelectedItem != null)
                {
                    retrievedValues.Add(comboBox.SelectedItem.ToString());
                }
                if (comboBox.SelectedItem == null)
                {
                    retrievedValues.Add("");
                }
            }

            return retrievedValues;
        }



        private void removeHeaderButton_Click(object sender, EventArgs e)
        {
            //headerCounter cannot go into the negative
            if (headerCounter > 0)
            {
                headerCounter--;
                if (loadedProfile != null)
                {
                    //loadedProfile.Parameters.RemoveAt(headerCounter);
                }
                GenerateComboBoxes(headerCounter);

            }

        }

        private void addHeaderButton_Click(object sender, EventArgs e)
        {
            headerCounter = comboBoxes.Count;
            //Here you can set the maximum amount of headers
            if (headerCounter < 26)
            {
                headerCounter++;
                GenerateComboBoxes(headerCounter);
            }
        }

        private void profileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Profile> defaultProfiles = profileController.defaultProfiles;
            List<string> selectedProfileParameters = new List<string>();

            // Get the selected profile
            Profile selectedProfile = defaultProfiles.FirstOrDefault(p => p.Name == profileTypeComboBox.SelectedItem.ToString());

            // Check if the selected profile exists
            if (selectedProfile != null)
            {
                // Call AddOptionsToComboBoxes with the selected profile parameters
                AddOptionsToComboBoxes(selectedProfile);
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
                // Check if the profile name already exists in the defaultProfiles list
                Profile existingProfile = profileController.defaultProfiles.FirstOrDefault(profile => profile.Name == profileName);
                existingProfile = profileController.userProfiles.FirstOrDefault(profile => profile.Name == profileName);

                if (existingProfile != null)
                {
                    // Overwrite the existing profile
                    existingProfile.Parameters = GetComboBoxesValues();
                }
                else
                {
                    // Create a new profile with the given name and parameters
                    Profile newProfile = new Profile(profileName, GetComboBoxesValues(), ProfileType.User);

                    // Add the new profile to the defaultProfiles list
                    profileController.defaultProfiles.Add(newProfile);
                    profilesListBox.Items.Add(profileName);
                }

                // Optionally, you can clear the text in the profileNameTextBox
                UnselectAllComboBoxes();
                profileNameTextBox.Clear();

                // Optionally, you can save the profiles to a file or database
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
            // Get the selected profile name from the profileNameTextBox
            string selectedProfileName = profileNameTextBox.Text.Trim();

            // Check if a profile is selected
            if (!string.IsNullOrEmpty(selectedProfileName))
            {
                // Confirm with the user before deleting the profile
                DialogResult result = MessageBox.Show($"Are you sure you want to delete the profile '{selectedProfileName}'?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Find the selected profile
                    Profile selectedProfile = profileController.userProfiles.FirstOrDefault(profile => profile.Name == selectedProfileName);

                    if (selectedProfile != null)
                    {
                        // Remove the selected profile from the defaultProfiles list
                        profileController.userProfiles.Remove(selectedProfile);
                        profilesListBox.Items.Remove(selectedProfileName);

                        //clear the selection in the profileTypeComboBox
                        profileTypeComboBox.SelectedIndex = -1;
                        profileController.SaveProfiles();
                    }
                }
            }
            else
            {
                //display an error message if no profile is selected
                MessageBox.Show("Please select a profile to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (profilesListBox.SelectedItem != null)
            {
                LoadProfileToComboBoxes(profilesListBox.SelectedItem.ToString());
            }
        }

        private void LoadProfileToComboBoxes(string loadedProfileName)
        {
            // Set the profile name in profileNameTextBox
            profileNameTextBox.Text = loadedProfileName;

            // Check if the loaded profile exists in defaultProfiles
            loadedProfile = profileController.allProfiles.FirstOrDefault(profile => profile.Name == loadedProfileName);

            headerCounter = loadedProfile.Parameters.Count;

            if (loadedProfile != null)
            {
                // Get the parameters of the loaded profile
                List<string> loadedProfileParameters = loadedProfile.Parameters;

                GenerateComboBoxes(loadedProfileParameters.Count);

                // Ensure that the loadedProfileParameters count matches the number of comboBoxes
                if (loadedProfileParameters.Count == comboBoxes.Count)
                {
                    // Iterate through each ComboBox in comboBoxes
                    for (int i = 0; i < comboBoxes.Count; i++)
                    {
                        // Set the selected item in the ComboBox
                        comboBoxes[i].SelectedItem = loadedProfileParameters[i];
                    }
                }
                else
                {
                    MessageBox.Show("The number of values in the loaded profile does not match the number of ComboBoxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                headerCounter = comboBoxes.Count;
            }
        }

        private void newProfileButton_Click(object sender, EventArgs e)
        {
            loadedProfile = new Profile("empty", new List<string>(), ProfileType.User);
            headerCounter = 0;
            GenerateComboBoxes(headerCounter);
        }
    }
}
