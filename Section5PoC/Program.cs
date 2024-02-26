using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace Section5PoC
{
    internal class Program
    {
        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static string extractedBundles;
        
        
        private static Extractor extractor;
        private static WCSPP_Convertor wcsppConvertor;

        public static void Main()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            extractor = new Extractor();
            wcsppConvertor = new WCSPP_Convertor();

            string textFilePath = SelectTxtFilePath();

            extractedWires = extractor.ExtractWiresFromFile(textFilePath);
            extractedComponents = extractor.ExtractComponentsFromFile(textFilePath);
            extractedBundles = extractor.ExtractBundlesFromFile(textFilePath);

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Open in Excel");
            Console.WriteLine("2. Write to Text File");
            Console.Write("Enter the number of your choice: ");


            //TODO: maybe rewrite this seems dirty
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        wcsppConvertor.ConvertListToWCSPPExcelFile(extractedWires, extractedComponents, extractedBundles);
                        Console.WriteLine("Opening in Excel...");
                        break;

                    case 2:
                        wcsppConvertor.ConvertListToWCSPPTextFile(extractedWires, extractedComponents, extractedBundles);
                        Console.WriteLine("Writing to Text File...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.ReadLine();
        }

        

        static string SelectTxtFilePath()
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
