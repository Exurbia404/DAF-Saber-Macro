using Logging;
using Logic;
using Newtonsoft.Json;
using System.Data;

namespace Data_Access
{
    public class RefSetHandler
    {
        private Logger _logger;
        private string filePath = @"N:\Saber Tool Plus\Data\refset.json";

        public RefSetHandler(Logger logger) 
        {
            _logger = logger;
        }

        public void SaveRefSets(List<DSI_Reference> references)
        {
            if(Environment.MachineName == "EXURBIA")
            {
                SaveToSettings(references);
            }
            else
            {
                SaveToDisk(references);
            }
        }

        public List<DSI_Reference> LoadRefSets()
        {
            if (Environment.MachineName == "EXURBIA")
            {
                return LoadFromSettings();
            }
            else
            {
                return LoadFromDisk();
            }
        }

        private void SaveToDisk(List<DSI_Reference> references)
        {
            _logger.Log("Saved" + references.Count + " to disk");
            string json = JsonConvert.SerializeObject(references, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private List<DSI_Reference> LoadFromDisk()
        {

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<DSI_Reference>>(json);
            }
            else
            {
                // Return an empty list if the file doesn't exist
                return new List<DSI_Reference>();
            }
        }

        private void SaveToSettings(List<DSI_Reference> references)
        {
            _logger.Log("Saved" + references.Count + " to settings");
            // Convert list of DSI_Reference objects to a list of strings
            List<string> referenceStrings = references.Select(reference => $"{reference.YearWeek}:{reference.BundleNumber}:{reference.ProjectName}:{reference.Description}").ToList();

            // Join the list of strings into a single string with a delimiter
            string serializedReferences = string.Join(";", referenceStrings);

            // Save the serialized references to application settings
            MySettings.Default.RefSets = serializedReferences;

            //Save the changes to the settings
            MySettings.Default.Save();
        }

        private List<DSI_Reference> LoadFromSettings()
        {
            List<DSI_Reference> references = new List<DSI_Reference>();

            // Start the stopwatch
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                // Retrieve the serialized references from application settings
                string serializedReferences = MySettings.Default.RefSets;

                if (!string.IsNullOrEmpty(serializedReferences))
                {
                    // Split the serialized references string by the delimiter
                    string[] referenceStrings = serializedReferences.Split(';');

                    foreach (string referenceString in referenceStrings)
                    {
                        // Split each reference string into its components
                        string[] parts = referenceString.Split(':');
                        if (parts.Length == 4) // Ensure all components are present
                        {
                            try
                            {
                                // Create a DSI_Reference object from the parts
                                DSI_Reference reference = new DSI_Reference
                                {
                                    YearWeek = parts[0],
                                    BundleNumber = parts[1],
                                    ProjectName = parts[2],
                                    Description = parts[3]
                                };
                                references.Add(reference);
                            }
                            catch (Exception ex)
                            {
                                // Handle any exceptions that occur during object creation
                                _logger.Log($"Error creating DSI_Reference object: {ex.Message}");
                            }
                        }
                        else
                        {
                            // Handle incorrect format error
                            _logger.Log("Invalid reference format: " + referenceString);
                        }
                    }
                }
            }
            finally
            {
                // Stop the stopwatch
                stopwatch.Stop();

                // Log the elapsed time in milliseconds
                _logger.Log($"LoadRefSetsFromSettings function executed in {stopwatch.ElapsedMilliseconds} ms");
            }
           
            return references;
        }
    }
}
