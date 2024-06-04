using Logic;
using Data_Interfaces;
using Logging;
using Newtonsoft.Json;
using System.Drawing.Text;

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
            fileName = fileName.Replace("_DSI", "");

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
                    List<Profile> profiles = JsonConvert.DeserializeObject<List<Profile>>(json);
                    _logger.Log("Profiles loaded successfully.");

                    // Convert the list of Profile to a list of iProfile
                    List<iProfile> iProfiles = profiles.Cast<iProfile>().ToList();
                    return iProfiles;
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

        public bool SearchBeforeRelease(string bundleNumber, string issueNumber, string bsaFilePath)
        {
            string DSI_filePath = null;
            string DSI_Violations_filePath = null;
            string Parts_filePath = null;
            string WCSPP_filePath = null;
            string SVG_filePath = null;
            string TIF_filePath = null;

            // Search for the files in Boms folder
            string bomsFolderPath = Path.Combine(bsaFilePath, "Boms");
            if (Directory.Exists(bomsFolderPath))
            {
                // Recursively search through all subdirectories in Boms folder
                string[] bomDirectories = Directory.GetDirectories(bomsFolderPath, "*", SearchOption.AllDirectories);

                // Find the directory matching bundleNumber
                string bundleDirectory = bomDirectories.FirstOrDefault(dir => Path.GetFileName(dir) == bundleNumber);
                if (bundleDirectory != null)
                {
                    // Search for files matching the pattern "bundleNumber + issueNumber"
                    string searchName = bundleNumber + "-" + issueNumber;
                    string[] bundleFiles = Directory.GetFiles(bundleDirectory, $"{searchName}*", SearchOption.TopDirectoryOnly);

                    // Assign file paths if found
                    foreach (string file in bundleFiles)
                    {
                        if (file.EndsWith("_DSI.txt"))
                            DSI_filePath = file;
                        else if (file.EndsWith("_DSI.violations"))
                            DSI_Violations_filePath = file;
                        else if (file.EndsWith("_generated_parts.txt"))
                            Parts_filePath = file;
                        else if (file.EndsWith("_WCSPP.txt"))
                            WCSPP_filePath = file;
                    }
                }
            }

            // Search for the files in Drawings folder
            string drawingsFolderPath = Path.Combine(bsaFilePath, "Drawings");
            if (Directory.Exists(drawingsFolderPath))
            {
                // Search for SVG and TIF files matching both bundleNumber and issueNumber
                string[] svgFiles = Directory.GetFiles(drawingsFolderPath, $"*{bundleNumber}-{issueNumber}-*.svg", SearchOption.TopDirectoryOnly);
                if (svgFiles.Length > 0)
                    SVG_filePath = svgFiles[0];

                string[] tifFiles = Directory.GetFiles(drawingsFolderPath, $"*{bundleNumber}-{issueNumber}-*.tif", SearchOption.TopDirectoryOnly);
                if (tifFiles.Length > 0)
                    TIF_filePath = tifFiles[0];
            }

            // Now you have the file paths for each file type
            _logger.Log($"DSI_filePath: {DSI_filePath}");
            _logger.Log($"DSI_Violations_filePath: {DSI_Violations_filePath}");
            _logger.Log($"Parts_filePath: {Parts_filePath}");
            _logger.Log($"WCSPP_filePath: {WCSPP_filePath}");
            _logger.Log($"SVG_filePath: {SVG_filePath}");
            _logger.Log($"TIF_filePath: {TIF_filePath}");


            return (DSI_filePath != null && DSI_Violations_filePath != null && Parts_filePath != null && WCSPP_filePath != null && SVG_filePath != null && TIF_filePath != null);
        }

        public void ReleaseBundle(string bundleNumber, string issueNumber, string bsaFilePath, string destinationFilePath)
        {
            string DSI_filePath = null;
            string DSI_Violations_filePath = null;
            string Parts_filePath = null;
            string WCSPP_filePath = null;
            string SVG_filePath = null;
            string TIF_filePath = null;

            // Search for the files in Boms folder
            string bomsFolderPath = Path.Combine(bsaFilePath, "Boms");
            if (Directory.Exists(bomsFolderPath))
            {
                // Recursively search through all subdirectories in Boms folder
                string[] bomDirectories = Directory.GetDirectories(bomsFolderPath, "*", SearchOption.AllDirectories);

                // Find the directory matching bundleNumber
                string bundleDirectory = bomDirectories.FirstOrDefault(dir => Path.GetFileName(dir) == bundleNumber);
                if (bundleDirectory != null)
                {
                    // Search for files matching the pattern "bundleNumber + issueNumber"
                    string searchName = bundleNumber + "-" + issueNumber;
                    string[] bundleFiles = Directory.GetFiles(bundleDirectory, $"{searchName}*", SearchOption.TopDirectoryOnly);

                    // Assign file paths if found
                    foreach (string file in bundleFiles)
                    {
                        if (file.EndsWith("_DSI.txt"))
                            DSI_filePath = file;
                        else if (file.EndsWith("_DSI.violations"))
                            DSI_Violations_filePath = file;
                        else if (file.EndsWith("_generated_parts.txt"))
                            Parts_filePath = file;
                        else if (file.EndsWith("_generated_WCSPP.txt"))
                            WCSPP_filePath = file;
                    }
                }
            }

            // Search for the files in Drawings folder
            string drawingsFolderPath = Path.Combine(bsaFilePath, "Drawings");
            if (Directory.Exists(drawingsFolderPath))
            {
                // Search for SVG and TIF files matching both bundleNumber and issueNumber
                string[] svgFiles = Directory.GetFiles(drawingsFolderPath, $"*{bundleNumber}-{issueNumber}-*.svg", SearchOption.TopDirectoryOnly);
                if (svgFiles.Length > 0)
                    SVG_filePath = svgFiles[0];

                string[] tifFiles = Directory.GetFiles(drawingsFolderPath, $"*{bundleNumber}-{issueNumber}-*.tif", SearchOption.TopDirectoryOnly);
                if (tifFiles.Length > 0)
                    TIF_filePath = tifFiles[0];
            }

            
            // Create the destination directories if they don't exist
            string destinationBomsFolderPath = Path.Combine(destinationFilePath, "Boms", bundleNumber);
            string destinationDrawingsFolderPath = Path.Combine(destinationFilePath, "Drawings");

            if (!Directory.Exists(destinationBomsFolderPath))
                Directory.CreateDirectory(destinationBomsFolderPath);

            // Move the files to the destination folders
            if (DSI_filePath != null)
                File.Move(DSI_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(DSI_filePath)));

            if (DSI_Violations_filePath != null)
                File.Move(DSI_Violations_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(DSI_Violations_filePath)));

            if (Parts_filePath != null)
                File.Move(Parts_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(Parts_filePath)));

            if (WCSPP_filePath != null)
                File.Move(WCSPP_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(WCSPP_filePath)));

            if (SVG_filePath != null)
                File.Move(SVG_filePath, Path.Combine(destinationDrawingsFolderPath, Path.GetFileName(SVG_filePath)));

            if (TIF_filePath != null)
                File.Move(TIF_filePath, Path.Combine(destinationDrawingsFolderPath, Path.GetFileName(TIF_filePath)));
        }
    }
}
