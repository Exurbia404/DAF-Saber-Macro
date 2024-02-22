using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Section5PoC
{
    internal class Program
    {
        private static Extractor extractor;
        private static List<Wire> extractedWires;
        private static ExcelHandler excelHandler;
        private static WCSPP_Convertor wcsppConvertor;

        public static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            extractor = new Extractor();
            extractedWires = extractor.ExtractSection5FromTextFile(SelectTxtFilePath());
            excelHandler = new ExcelHandler();
            wcsppConvertor = new WCSPP_Convertor();

            // Your existing code to extract wires and create WCSPPConvertor instance
            // ...

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Open in Excel");
            Console.WriteLine("2. Write to Text File");
            Console.Write("Enter the number of your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        wcsppConvertor.ConvertListToWCSPPExcelFile(extractedWires);
                        Console.WriteLine("Opening in Excel...");
                        break;

                    case 2:
                        wcsppConvertor.ConvertListToWCSPPTextFile(extractedWires);
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
