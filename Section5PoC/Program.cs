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

        public static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

            extractor = new Extractor();
            extractedWires = extractor.ExtractSection5FromTextFile(SelectTxtFilePath());
            excelHandler = new ExcelHandler();

            excelHandler.CreateExcelSheet(extractedWires);

            Console.ReadLine();
        }

        

        static string SelectTxtFilePath()
        {
            // Get the directory where the executable is located
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] txtFiles = Directory.GetFiles(exePath, "*.txt");

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
