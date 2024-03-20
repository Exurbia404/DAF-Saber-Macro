using System.Diagnostics;
using System;
using OfficeOpenXml;
using Data_Access;

namespace Logic
{
    internal class Program
    {
        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static List<Bundle> extractedBundles;
        
        
        private static Extractor extractor;
        private static WCSPP_Convertor wcsppConvertor;
        private static TestingSuite testingSuite;
        private static ExcelImporter excelImporter;

        public static void Main()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            MainForm myForm = new MainForm();
            myForm.Show();  // Show the form
            Application.Run(myForm);

            Console.ReadLine();
        }

        public void ConsoleProgram()
        {

            string textFilePath = SelectTxtFilePath();
            string fileName = Path.GetFileNameWithoutExtension(textFilePath).Replace("_DSI", "");

            extractor = new Extractor();

            extractedWires = extractor.ExtractWiresFromFile(textFilePath);
            extractedComponents = extractor.ExtractComponentsFromFile(textFilePath);
            extractedBundles = extractor.ExtractBundlesFromFile(textFilePath);

            wcsppConvertor = new WCSPP_Convertor(extractedWires, extractedComponents);
            testingSuite = new TestingSuite();

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Open in Excel");
            Console.WriteLine("2. Write to Text File");
            Console.WriteLine("3. Test Conversion"); // New option
            Console.Write("Enter the number of your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        wcsppConvertor.ConvertListToWCSPPExcelFile(extractedWires, extractedComponents, extractedBundles);
                        Console.WriteLine("Opening in Excel...");
                        break;

                    case 2:
                        Stopwatch stopwatch = new Stopwatch();

                        // Start the stopwatch
                        stopwatch.Start();
                        wcsppConvertor.ConvertListToWCSPPTextFile(extractedWires, extractedComponents, extractedBundles, fileName);
                        stopwatch.Stop();
                        Console.WriteLine("Writing to Text File...");
                        // Get the elapsed time
                        TimeSpan elapsedTime = stopwatch.Elapsed;

                        // Print the elapsed time in milliseconds
                        Console.WriteLine($"Elapsed Time: {elapsedTime.TotalMilliseconds} milliseconds");
                        break;

                    case 3:
                        Console.WriteLine("Testing Conversion...");
                        testingSuite.StartComponentsTest();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.ReadLine();
            Main();
        }
        

        private static string SelectTxtFilePath()
        {
            // Get the directory where the executable is located
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string inputFolderPath = Path.Combine(exePath, "data", "input");

            string[] txtFiles = Directory.GetFiles(inputFolderPath, "*.txt");

            if (txtFiles.Length > 0)
            {
                Console.WriteLine("Text files in the same folder as the executable:");

                for (int i = 0; i < txtFiles.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(txtFiles[i])}");
                }

                Console.Write("Enter the number of the text file you want to choose: ");

                if (int.TryParse(Console.ReadLine(), out int selectedFileIndex) && selectedFileIndex > 0 && selectedFileIndex <= txtFiles.Length)
                {
                    // Return the path of the selected text file
                    return txtFiles[selectedFileIndex - 1];
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return null; // Return null to indicate failure
                }
            }
            else
            {
                Console.WriteLine("No .txt files found in the same folder as the executable.");
                return null; // Return null to indicate failure
            }

        }
    }
}
