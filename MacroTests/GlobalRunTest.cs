using Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace MacroTests
{
    public class GlobalRunTest
    {
        private Extractor extractor;
        private Logger _logger;
        private List<string> folderPaths;

        [Fact]
        public void TestIfAllBomsRun()
        {
            // DeleteAllGeneratedTxtFiles(); // Uncomment if needed

            _logger = new Logger();
            extractor = new Extractor(_logger);

            string DesignerBuildOfMaterialsFolder = new FolderPaths(_logger).ExurbiaLocal;
            folderPaths = GetImmediateSubfolders(DesignerBuildOfMaterialsFolder);

            foreach (string path in folderPaths)
            {
                OpenLatestBundleFile(path);
            }

            List<string> failingSubdirectories = AreGeneratedTxtFilesPresent();

            if (failingSubdirectories.Any())
            {
                Console.WriteLine("The following subdirectories are missing 'generated' .txt files:");
                foreach (var subdirectory in failingSubdirectories)
                {
                    Console.WriteLine(subdirectory);
                }
            }

            Assert.True(!failingSubdirectories.Any(), "Some subdirectories are missing 'generated' .txt files.");
        }

        private List<string> AreGeneratedTxtFilesPresent()
        {
            string directoryPath = @"C:\Users\tomvh\Documents\School\S5 - Internship\boms";

            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException("Directory does not exist: " + directoryPath);
            }

            // Get all subdirectories
            string[] subdirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);

            // List to hold subdirectories that are missing 'generated' .txt files
            List<string> failingSubdirectories = new List<string>();

            // Check each subdirectory
            foreach (string subdirectory in subdirectories)
            {
                // Get all .txt files in the current subdirectory
                string[] files = Directory.GetFiles(subdirectory, "*.txt", SearchOption.TopDirectoryOnly);

                // Check if any file contains "generated" in its name
                bool hasGeneratedFile = files.Any(file => Path.GetFileName(file).Contains("generated"));
                bool dsiPresent = files.Any(file => Path.GetFileName(file).Contains("DSI"));


                // If no "generated" file is found in the current subdirectory, add to failing list
                if (!hasGeneratedFile && !dsiPresent)
                {
                    Console.WriteLine(subdirectory.ToString());
                    failingSubdirectories.Add(subdirectory);
                }
            }

            // Return the list of failing subdirectories
            return failingSubdirectories;
        }

        private List<string> GetImmediateSubfolders(string folderPath)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> folderPaths = new List<string>();

            try
            {
                // Get all immediate subfolders synchronously
                string[] subfolders = Directory.GetDirectories(folderPath);

                // Process subfolders
                foreach (string subfolder in subfolders)
                {
                    try
                    {
                        // Check if the folder has any files

                        // Extract folder name and path
                        string folderName = Path.GetFileName(subfolder);

                        // Check if the folder name is 7 or 8 numbers long
                        if (IsNumeric(folderName) && (folderName.Length == 7 || folderName.Length == 8))
                        {
                            // Add to list
                            folderPaths.Add(subfolder);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Log($"Error processing folder '{subfolder}': {ex.Message}");
                    }
                }

                stopwatch.Stop();
                _logger.Log("Folders retrieved in: " + stopwatch.Elapsed.TotalMilliseconds + "ms");
                return folderPaths;
            }
            catch (Exception ex)
            {
                _logger.Log($"Error: {ex.Message}");
                return null;
            }
        }

        private static bool IsNumeric(string str)
        {
            return int.TryParse(str, out _);
        }

        private string GetFilePath(string inputName)
        {
            try
            {
                inputName = Path.GetFileName(inputName);

                foreach (string folderPath in folderPaths)
                {
                    string folderName = Path.GetFileName(folderPath);

                    // Case-insensitive comparison
                    if (string.Equals(folderName, inputName, StringComparison.OrdinalIgnoreCase))
                    {
                        return folderPath;
                    }
                }

                // No matching folder path found
                return null;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                return null;
            }
        }

        private void OpenLatestBundleFile(string bundleName)
        {
            try
            {
                // Get the corresponding folder path
                string selectedFolderPath = GetFilePath(bundleName);

                // Search for the latest .txt file containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");
                if (txtFiles.Length > 0)
                {
                    // Sort files by creation time and get the latest one
                    string latestTxtFile = txtFiles.OrderByDescending(f => new FileInfo(f).CreationTime).First();

                    GenerateTxtFiles(latestTxtFile);
                }

            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
            }

        }

        private void GenerateTxtFiles(string textFilePath)
        {
            

            extractor.ExtractBundleFromFilePath(textFilePath);

            List<Bundle> extractedBundles = extractor.Bundles;
            List<DSI_Tube> extractedTubes = extractor.ExtractDSITubes(textFilePath);
            FileHandler fileHandler = new FileHandler(_logger);

            List<Converted_Component> convertedComponents = extractor.Components;
            List<Converted_Wire> convertedWires = extractor.Wires;


            string bundleNumber = GetFileName(textFilePath);
            string filePath = GetFolderPath(textFilePath);

            fileHandler.WriteToFile(convertedWires.Cast<Data_Interfaces.iConverted_Wire>().ToList(), convertedComponents.Cast<Data_Interfaces.iConverted_Component>().ToList(), extractedBundles.Cast<Data_Interfaces.iBundle>().ToList(), bundleNumber, filePath);
            
        }

        private string GetFileName(string filePath)
        {
            try
            {
                // Get the file name without the extension
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

                // Remove the substring "_DSI" from the file name
                string cleanedFileName = fileNameWithoutExtension.Replace("_DSI", "");

                return fileNameWithoutExtension;
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as invalid file paths
                return null;
            }
        }

        private string GetFolderPath(string filePath)
        {
            try
            {
                // Get the directory name containing the file
                return Path.GetDirectoryName(filePath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as invalid file paths
                return null;
            }
        }

        public static void DeleteAllGeneratedTxtFiles()
        {
            string directoryPath = @"C:\Users\tomvh\Documents\School\S5 - Internship\boms";

            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                // Get all .txt files in the directory and its subdirectories
                string[] files = Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories);

                // Loop through the files and delete those containing "generated" in their names
                foreach (string file in files)
                {
                    if (file.Contains("generated"))
                    {
                        try
                        {
                            File.Delete(file);
                            Console.WriteLine($"Deleted: {file}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist: " + directoryPath);
            }
        }
    }
}