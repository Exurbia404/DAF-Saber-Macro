using Logic;
using Data_Interfaces;
using Logging;
using Newtonsoft.Json;
using System.Drawing.Text;
using System.Diagnostics;

namespace Data_Access
{
    
    public class FileHandler : iFileHandler
    {

        private Logger _logger;
        private string profilesFolder;

        private string DSI_filePath = null;
        private string DSI_Violations_filePath = null;
        private string Parts_filePath = null;
        private string WCSPP_filePath = null;
        private string SVG_filePath = null;
        private string TIF_filePath = null;

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
            string newFileName = $"{fileName}_WCSPP.txt";
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
                                  $"{wcsppWire.Term_2},{wcsppWire.Seal_2},{wcsppWire.Connector_2},{wcsppWire.Port_2},{wcsppWire.Variant},{wcsppWire.Bundle},,,";

                    // Write the formatted line to the file
                    writer.WriteLine(line);
                }

                _logger.Log($"{fileName}_WCSPP.txt generated");
            }

            //Generate the new filePath
            newFileName = $"{fileName}_parts.txt";
            newFilePath = Path.Combine(filePath, newFileName);

            // Create or overwrite the file
            using (StreamWriter writer = new StreamWriter(newFilePath))
            {
                // Determine if any component has a non-empty BundleModularID
                bool hasBundleModularID = components.Any(wcsppComponent => !string.IsNullOrEmpty(wcsppComponent.BundleModularID));

                // Write the appropriate header line based on the condition
                if (hasBundleModularID)
                {
                    writer.WriteLine("Name,Part_no,,Passive,Instruction,Variant,Bundle,Description,,BundleModularID,,,,,,,,,,,,,,,,,,,");
                }
                else
                {
                    writer.WriteLine("Name,Part_no,,Passive,Instruction,Variant,Bundle,Description,,,,,,,,,,,,,,,,,,,,,");
                }

                // Write each WCSPP_Wire object to the file
                foreach (var wcsppComponent in components)
                {
                    // Format the line with object properties
                    string line = $"{wcsppComponent.Name},{wcsppComponent.Part_no},,{wcsppComponent.Passive},{wcsppComponent.Instruction},{wcsppComponent.Variant},{wcsppComponent.Bundle}," +
                                $"{wcsppComponent.Description},{wcsppComponent.Location},{wcsppComponent.BundleModularID},,,,,,,,,,,,,,,,,,,{wcsppComponent.EndText}";
                    // Write the formatted line to the file
                    writer.WriteLine(line);
                }
                _logger.Log($"{fileName}_parts.txt generated");
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
            // Start the stopwatch for the entire process
            Stopwatch totalStopwatch = Stopwatch.StartNew();

            // Variables to hold time taken for each section
            double timeToFindBundleDirectory = 0;
            double timeToFindBomsFilePaths = 0;
            double timeToFindDrawingsFilePaths = 0;

            string searchName = bundleNumber + "-" + issueNumber;

            // Search for the files in Boms folder
            string bomsFolderPath = Path.Combine(bsaFilePath, "Boms");
            string bundleDirectory = null;

            if (Directory.Exists(bomsFolderPath))
            {
                // Start the stopwatch for finding the bundle directory
                Stopwatch bundleDirectoryStopwatch = Stopwatch.StartNew();

                // Search for the directory matching the bundleNumber in the Boms folder (non-recursive)
                string[] directories = Directory.GetDirectories(bomsFolderPath, bundleNumber, SearchOption.TopDirectoryOnly);
                bundleDirectory = directories.FirstOrDefault();

                // Stop the stopwatch for finding the bundle directory and log the time taken
                bundleDirectoryStopwatch.Stop();
                timeToFindBundleDirectory = bundleDirectoryStopwatch.Elapsed.TotalSeconds;

                if (bundleDirectory != null)
                {
                    // Start the stopwatch for finding the Boms file paths
                    Stopwatch bomsFilePathsStopwatch = Stopwatch.StartNew();

                    // Search for files matching the pattern "bundleNumber + issueNumber"
                    var bundleFiles = Directory.EnumerateFiles(bundleDirectory, $"{searchName}*", SearchOption.TopDirectoryOnly)
                                                .AsParallel();

                    // Assign file paths if found
                    foreach (string file in bundleFiles)
                    {
                        if (file.EndsWith("_DSI.txt"))
                            DSI_filePath = file;
                        else if (file.EndsWith("_DSI.violations"))
                            DSI_Violations_filePath = file;
                        else if (file.EndsWith("_parts.txt"))
                            Parts_filePath = file;
                        else if (file.EndsWith("_WCSPP.txt"))
                            WCSPP_filePath = file;

                        // Break early if all files are found
                        if (DSI_filePath != null && DSI_Violations_filePath != null && Parts_filePath != null && WCSPP_filePath != null)
                            break;
                    }

                    // Stop the stopwatch for finding the Boms file paths and log the time taken
                    bomsFilePathsStopwatch.Stop();
                    timeToFindBomsFilePaths = bomsFilePathsStopwatch.Elapsed.TotalSeconds;
                }
            }

            // Search for the files in Drawings folder
            string drawingsFolderPath = Path.Combine(bsaFilePath, "Drawings");
            if (Directory.Exists(drawingsFolderPath))
            {
                // Start the stopwatch for finding the Drawings file paths
                Stopwatch drawingsFilePathsStopwatch = Stopwatch.StartNew();

                // Use parallel tasks to search for SVG and TIF files concurrently
                var svgTask = Task.Run(() =>
                {
                    var svgFiles = Directory.EnumerateFiles(drawingsFolderPath, $"*{bundleNumber}-{issueNumber}-*.svg", SearchOption.TopDirectoryOnly);
                    SVG_filePath = svgFiles.FirstOrDefault();
                });

                var tifTask = Task.Run(() =>
                {
                    var tifFiles = Directory.EnumerateFiles(drawingsFolderPath, $"*{bundleNumber}-{issueNumber}-*.tif", SearchOption.TopDirectoryOnly);
                    TIF_filePath = tifFiles.FirstOrDefault();
                });

                // Wait for both tasks to complete
                Task.WaitAll(svgTask, tifTask);

                // Stop the stopwatch for finding the Drawings file paths and log the time taken
                drawingsFilePathsStopwatch.Stop();
                timeToFindDrawingsFilePaths = drawingsFilePathsStopwatch.Elapsed.TotalSeconds;
            }

            // Stop the total stopwatch
            totalStopwatch.Stop();
            double totalExecutionTime = totalStopwatch.Elapsed.TotalSeconds;

            // Log the times for each section and the total time
            _logger.Log($"Time to find bundle directory: {timeToFindBundleDirectory} seconds");
            _logger.Log($"Time to find Boms file paths: {timeToFindBomsFilePaths} seconds");
            _logger.Log($"Time to find Drawings file paths: {timeToFindDrawingsFilePaths} seconds");
            _logger.Log($"Total execution time: {totalExecutionTime} seconds");

            // Log the file paths
            _logger.Log($"DSI_filePath: {DSI_filePath}");
            _logger.Log($"DSI_Violations_filePath: {DSI_Violations_filePath}");
            _logger.Log($"Parts_filePath: {Parts_filePath}");
            _logger.Log($"WCSPP_filePath: {WCSPP_filePath}");
            _logger.Log($"SVG_filePath: {SVG_filePath}");
            _logger.Log($"TIF_filePath: {TIF_filePath}");

            // Return true if all required file paths are found
            return (DSI_filePath != null && DSI_Violations_filePath != null && Parts_filePath != null && WCSPP_filePath != null && SVG_filePath != null && TIF_filePath != null);
        }

        private string FindBundleDirectory(string rootPath, string bundleNumber)
        {
            foreach (string dir in Directory.EnumerateDirectories(rootPath))
            {
                if (Path.GetFileName(dir) == bundleNumber)
                {
                    return dir;
                }
                string foundDir = FindBundleDirectory(dir, bundleNumber);
                if (foundDir != null)
                {
                    return foundDir;
                }
            }
            return null;
        }

        public void ReleaseBundle(string bundleNumber, string issueNumber, string bsaFilePath, string destinationFilePath)
        {
            // Check if any of the file paths are empty
            if (DSI_filePath == null || DSI_Violations_filePath == null || Parts_filePath == null || WCSPP_filePath == null || SVG_filePath == null || TIF_filePath == null)
            {
                return;
            }

            // Create the destination directories if they don't exist
            string destinationBomsFolderPath = Path.Combine(destinationFilePath, "Boms", bundleNumber);
            string destinationDrawingsFolderPath = Path.Combine(destinationFilePath, "Drawings");

            if (!Directory.Exists(destinationBomsFolderPath))
                Directory.CreateDirectory(destinationBomsFolderPath);

            if (!Directory.Exists(destinationDrawingsFolderPath))
                Directory.CreateDirectory(destinationDrawingsFolderPath);

            // Copy the files to the destination folders
            if (DSI_filePath != null)
                File.Copy(DSI_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(DSI_filePath)), true);

            if (DSI_Violations_filePath != null)
                File.Copy(DSI_Violations_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(DSI_Violations_filePath)), true);

            if (Parts_filePath != null)
                File.Copy(Parts_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(Parts_filePath)), true);

            if (WCSPP_filePath != null)
                File.Copy(WCSPP_filePath, Path.Combine(destinationBomsFolderPath, Path.GetFileName(WCSPP_filePath)), true);

            if (SVG_filePath != null)
                File.Copy(SVG_filePath, Path.Combine(destinationDrawingsFolderPath, Path.GetFileName(SVG_filePath)), true);

            if (TIF_filePath != null)
                File.Copy(TIF_filePath, Path.Combine(destinationDrawingsFolderPath, Path.GetFileName(TIF_filePath)), true);
        }
    }
}
