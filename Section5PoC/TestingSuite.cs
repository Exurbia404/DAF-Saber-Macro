using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class TestingSuite
    {
        //Import folder/files

        //Convert them

        //Compare them to real files

        //Print output

        static string exePath = AppDomain.CurrentDomain.BaseDirectory;
        string originalFolder = Path.Combine(exePath, "data", "original");
        string generatedFolder = Path.Combine(exePath, "data", "output");
        string inputFolder = Path.Combine(exePath, "data", "input");

        private static Extractor extractor;
        private static WCSPP_Convertor wcsppConvertor;

        public TestingSuite()
        {
            extractor = new Extractor();
            wcsppConvertor = new WCSPP_Convertor();
        }

        public void StartComponentsTest()
        {
            ConvertFilesForTest();

            Dictionary<string, double> similarityPercentages = CompareFiles(originalFolder, generatedFolder);

            foreach (var entry in similarityPercentages)
            {
                Console.WriteLine($"Similarity Percentage for Set {entry.Key}: {entry.Value}%");
            }
        }

        private void ConvertFilesForTest()
        {
            string[] files = Directory.GetFiles(inputFolder, "*.txt");

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                List<Wire> extractedWires = extractor.ExtractWiresFromFile(filePath);
                List<Component> extractedComponents = extractor.ExtractComponentsFromFile(filePath);
                List<Bundle> extractedBundles = extractor.ExtractBundlesFromFile(filePath);

                wcsppConvertor.ConvertListToWCSPPTextFile(extractedWires, extractedComponents, extractedBundles, fileName);
            }
        }

        static Dictionary<string, double> CompareFiles(string originalFolder, string generatedFolder)
        {
            Dictionary<string, double> similarityPercentages = new Dictionary<string, double>();

            // Get all original files in the "original" folder
            string[] originalFiles = Directory.GetFiles(originalFolder, "*.txt");

            foreach (string originalFile in originalFiles)
            {
                // Extract the base name without extension (e.g., 1780456-09)
                string baseName = Path.GetFileNameWithoutExtension(originalFile);
                baseName = baseName.Replace("_Parts", "");

                // Construct the corresponding generated file names
                string generatedPartsFile = $"{baseName}_generated_parts.txt";

                // Check if the generated files exist
                string generatedPartsPath = Path.Combine(generatedFolder, generatedPartsFile);

                if (File.Exists(generatedPartsPath))
                {
                    // Compare the contents of the original file with the generated parts file
                    int matchingChars = CountMatchingCharacters(originalFile, generatedPartsPath);
                    int totalChars = CountTotalCharacters(originalFile);

                    if (totalChars > 0)
                    {
                        // Calculate the percentage of similarity for the set
                        double similarityPercentage = (double)matchingChars / totalChars * 100;
                        similarityPercentages.Add(baseName, similarityPercentage);
                    }
                    else
                    {
                        // If no characters to compare, consider them 100% similar
                        similarityPercentages.Add(baseName, 100);
                    }
                }
            }

            return similarityPercentages;
        }

        static int CountMatchingCharacters(string path1, string path2)
        {
            string content1 = File.ReadAllText(path1);
            string content2 = File.ReadAllText(path2);

            int matchingChars = content1.Zip(content2, (c1, c2) => c1 == c2 ? 1 : 0).Sum();

            return matchingChars;
        }

        static int CountTotalCharacters(string path)
        {
            string content = File.ReadAllText(path);
            return content.Length;
        }
    
    }

    //TODO: create a wire testing suite

    //TODO: create a document testing suite


}

