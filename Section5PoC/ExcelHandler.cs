using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class ExcelHandler
    {
        public void CreateExcelSheet(List<Wire> extractedWires)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    // Add a worksheet to the Excel package
                    var worksheet = package.Workbook.Worksheets.Add("Wires");

                    // Write column headers
                    WriteHeaders(worksheet);

                    // Write wire data
                    WriteData(worksheet, extractedWires);
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    AddAutoFilterButtons(worksheet);


                    // Save the Excel package to a file
                    package.SaveAs(new FileInfo("Wires.xlsx"));

                    Process.Start("Wires.xlsx");
                }

                Console.WriteLine("Excel file created successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void AddAutoFilterButtons(ExcelWorksheet worksheet)
        {
            // Assuming headers are in the first row (row 1)
            worksheet.Cells["A1:" + worksheet.Dimension.End.Address].AutoFilter = true;
        }

        static void WriteHeaders(ExcelWorksheet worksheet)
        {
            // Assuming your wire class has properties like WireName, WireOption, etc.
            string[] headers = {
                "WireName (30)", "WireOption (200)", "WireType (30)", "Color (30)",
                "CrossSectionalArea", "Material (30)", "UserModule (30)", "MulticoreName (30)",
                "End1NodeName (10)", "End1Route (10)", "End1Cavity (10)", "End1MaterialCode (4)",
                "End2NodeName (10)", "End2Route (10)", "End2Cavity (10)", "End2MaterialCode (4)",
                "IncludeOnBOM", "IncludeOnChart", "WireTag (30)", "WireNote (100)",
                "WireLengthChangeType", "WireLengthChangeValue", "AssemblyItemNumber", "MulticoreOption (30)"
            };


            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }
        }

        static void WriteData(ExcelWorksheet worksheet, List<Wire> wires)
        {
            for (int row = 0; row < wires.Count; row++)
            {
                var wire = wires[row];

                // Assuming your wire class has properties like WireName, WireOption, etc.
                worksheet.Cells[row + 2, 1].Value = wire.WireName;
                worksheet.Cells[row + 2, 2].Value = wire.WireOption;
                worksheet.Cells[row + 2, 3].Value = wire.WireType;
                worksheet.Cells[row + 2, 4].Value = wire.Color;
                worksheet.Cells[row + 2, 5].Value = wire.CrossSectionalArea;
                worksheet.Cells[row + 2, 6].Value = wire.Material;
                worksheet.Cells[row + 2, 7].Value = wire.UserModule;
                worksheet.Cells[row + 2, 8].Value = wire.MulticoreName;
                worksheet.Cells[row + 2, 9].Value = wire.End1NodeName;
                worksheet.Cells[row + 2, 10].Value = wire.End1Route;
                worksheet.Cells[row + 2, 11].Value = wire.End1Cavity;
                worksheet.Cells[row + 2, 12].Value = wire.End1MaterialCode;
                worksheet.Cells[row + 2, 13].Value = wire.End2NodeName;
                worksheet.Cells[row + 2, 14].Value = wire.End2Route;
                worksheet.Cells[row + 2, 15].Value = wire.End2Cavity;
                worksheet.Cells[row + 2, 16].Value = wire.End2MaterialCode;
                worksheet.Cells[row + 2, 17].Value = wire.IncludeOnBOM;
                worksheet.Cells[row + 2, 18].Value = wire.IncludeOnChart;
                worksheet.Cells[row + 2, 19].Value = wire.WireTag;
                worksheet.Cells[row + 2, 20].Value = wire.WireNote;
                worksheet.Cells[row + 2, 21].Value = wire.WireLengthChangeType;
                worksheet.Cells[row + 2, 22].Value = wire.WireLengthChangeValue;
                worksheet.Cells[row + 2, 23].Value = wire.AssemblyItemNumber;
                worksheet.Cells[row + 2, 24].Value = wire.MulticoreOption;

                // ... add other properties ...
            }
        }
    }
}
