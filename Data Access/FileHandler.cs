using Logic;
using Data_Interfaces;
using Logging;
using Newtonsoft.Json;

namespace Data_Access
{
    
    public class FileHandler : iFileHandler
    {

        private Logger _logger;
        private string profilesFolder;
        
        public FileHandler(Logger logger)
        {
            _logger = logger;
            profilesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Saber Tool Plus", "UserData");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(profilesFolder))
            {
                Directory.CreateDirectory(profilesFolder);
            }
        }

        public void WriteToFile(List<iConverted_Wire> wires, List<iConverted_Component> components, List<iBundle> extractedBundles, string fileName, string filePath)
        {
            filePath = filePath.Replace("_DSI", "");

            //Construct the new filePath
            string newFileName = $"{fileName}_generated_WCSPP.txt";
            string newFilePath = Path.Combine(filePath, newFileName);

            _logger.Log("Writing to: " + newFilePath);

            // Create or overwrite the file
            using (StreamWriter writer = new StreamWriter(newFilePath))
            {
                // Write the header line
                writer.WriteLine("Wire,Diam,Color,Type,Code_no,Length,Connector_1,Port_1,Term_1,Seal_1,Wire_connection,Term_2,Seal_2,Connector_2,Port_2,Variant,Bundle,Loc_1,Loc_2,");

                // Write each WCSPP_Wire object to the file
                foreach (var wcsppWire in wires)
                {
                    // Format the line with object properties
                    string line = $"{wcsppWire.Wire},{wcsppWire.Diameter},{wcsppWire.Color},{wcsppWire.Type},{wcsppWire.Code_no},{wcsppWire.Length}," +
                                  $"{wcsppWire.Connector_1},{wcsppWire.Port_1},{wcsppWire.Term_1},{wcsppWire.Seal_1},{wcsppWire.Wire_connection}," +
                                  $"{wcsppWire.Term_2},{wcsppWire.Seal_2},{wcsppWire.Connector_2},{wcsppWire.Port_2},{wcsppWire.Variant},{wcsppWire.Bundle}," +
                                  $"{wcsppWire.Loc_1},{wcsppWire.Loc_2},";

                    // Write the formatted line to the file
                    writer.WriteLine(line);
                }

                _logger.Log($"{fileName}_generated_WCSPP.txt generated");
            }

            //Generate the new filePath
            newFileName = $"{fileName}_generated_parts.txt";
            newFilePath = Path.Combine(filePath, newFileName);

            // Create or overwrite the file
            using (StreamWriter writer = new StreamWriter(newFilePath))
            {
                // Write the header line
                writer.WriteLine("Name,Part_no,,Passive,Instruction,Variant,Bundle,Description,Lokation,,,,,,,,,,,,,,,,,,,,");

                // Write each WCSPP_Wire object to the file
                foreach (var wcsppComponent in components)
                {
                    // Format the line with object properties
                    string line = $"{wcsppComponent.Name},{wcsppComponent.Part_no},,{wcsppComponent.Passive},{wcsppComponent.Instruction},{wcsppComponent.Variant},{wcsppComponent.Bundle}," +
                                $"{wcsppComponent.Description},{wcsppComponent.Location},,,,,,,,,,,,,,,,,,,,{wcsppComponent.EndText}";
                    // Write the formatted line to the file
                    writer.WriteLine(line);
                }
                _logger.Log($"{fileName}_generated_parts.txt generated");
            }
        }

        public void LoadFile()
        {

        }

        public void LoadFilesFromFolder()
        {

        }

        public void SaveProfiles(List<iProfile> profiles)
        {
            try
            {
                string json = JsonConvert.SerializeObject(profiles, Formatting.Indented);
                string filePath = Path.Combine(profilesFolder, "profiles.json");
                File.WriteAllText(filePath, json);
                _logger.Log("Profiles saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error saving profiles: {ex.Message}");
            }
        }

        public List<iProfile> LoadProfiles()
        {
            try
            {
                string filePath = Path.Combine(profilesFolder, "profiles.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    List<iProfile> profiles = JsonConvert.DeserializeObject<List<iProfile>>(json);
                    _logger.Log("Profiles loaded successfully.");
                    return profiles;
                }
                else
                {
                    _logger.Log("No profiles found.");
                    return new List<iProfile>();
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Error loading profiles: {ex.Message}");
                return new List<iProfile>();
            }
        }
    }
}
