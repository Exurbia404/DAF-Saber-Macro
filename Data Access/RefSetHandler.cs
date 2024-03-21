using Logging;
using Logic;
using System.Data;

namespace Data_Access
{
    public class RefSetHandler
    {
        Logger _logger;
        
        public RefSetHandler(Logger logger) 
        {
            _logger = logger;
        }

        public void SaveRefSets(List<DSI_Reference> references)
        {
            // Convert list of DSI_Reference objects to a list of strings
            List<string> referenceStrings = references.Select(reference => $"{reference.YearWeek}:{reference.BundleNumber}:{reference.ProjectName}:{reference.Description}").ToList();

            // Join the list of strings into a single string with a delimiter
            string serializedReferences = string.Join(";", referenceStrings);

            // Save the serialized references to application settings
            MySettings.Default.RefSets = serializedReferences;

            //Save the changes to the settings
            MySettings.Default.Save();
        }

        public List<DSI_Reference> LoadRefSets()
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
                _logger.Log($"LoadRefSets function executed in {stopwatch.ElapsedMilliseconds} ms");
            }

            return references;
        }
    }
}
