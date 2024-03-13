using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class ExcelHandler
    {

        public void CreateProjectExcelSheet(List<Project_Wire> wires, List<Project_Component> components)
        {
            try
            {
                // Start the stopwatch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Close any existing instances of "ExtractedData.xlsx"
                foreach (var process in Process.GetProcessesByName("EXCEL"))
                {
                    if (process.MainWindowTitle.Contains("ExtractedData.xlsx"))
                    {
                        process.Kill();
                        process.WaitForExit(); // Wait for the process to exit before continuing
                    }
                }

                using (var package = new ExcelPackage())
                {
                    // Add a worksheet for Wires
                    var wireWorksheet = package.Workbook.Worksheets.Add("Project_Wires");

                    // Write column headers for wires
                    WriteHeaders(wireWorksheet, wires);

                    // Write wire data
                    WriteDataToSheet(wireWorksheet, wires);
                    //wireWorksheet.Cells[wireWorksheet.Dimension.Address].AutoFitColumns();
                    AddAutoFilterButtons(wireWorksheet);

                    // Add a worksheet for Components
                    var componentWorksheet = package.Workbook.Worksheets.Add("Project_Components");

                    // Write column headers for components
                    WriteHeaders(componentWorksheet, components);

                    // Write component data
                    WriteDataToSheet(componentWorksheet, components);
                    //componentWorksheet.Cells[componentWorksheet.Dimension.Address].AutoFitColumns();
                    AddAutoFilterButtons(componentWorksheet);

                    // Save the Excel package to a file
                    package.SaveAs(new FileInfo("ExtractedData.xlsx"));

                    Process.Start("ExtractedData.xlsx");
                }

                // Stop the stopwatch
                stopwatch.Stop();

                Console.WriteLine($"Project Excel file created successfully. Time elapsed: {stopwatch.Elapsed.TotalSeconds}s");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void CreateExcelSheet(List<WCSPP_Wire> extractedWires, List<WCSPP_Component> extractedComponents)
        {
            try
            {
                // Start the stopwatch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Close any existing instances of "ExtractedData.xlsx"
                foreach (var process in Process.GetProcessesByName("EXCEL"))
                {
                    if (process.MainWindowTitle.Contains("ExtractedData.xlsx"))
                    {
                        process.Kill();
                        process.WaitForExit(); // Wait for the process to exit before continuing
                    }
                }

                using (var package = new ExcelPackage())
                {
                    // Add a worksheet for Wires
                    var wireWorksheet = package.Workbook.Worksheets.Add("Wires");

                    // Write column headers for wires
                    WriteHeaders(wireWorksheet, extractedWires);

                    // Write wire data
                    WriteDataToSheet(wireWorksheet, extractedWires);
                    wireWorksheet.Cells[wireWorksheet.Dimension.Address].AutoFitColumns();
                    AddAutoFilterButtons(wireWorksheet);

                    // Add a worksheet for Components
                    var componentWorksheet = package.Workbook.Worksheets.Add("Components");

                    // Write column headers for components
                    WriteHeaders(componentWorksheet, extractedComponents);

                    // Write component data
                    WriteDataToSheet(componentWorksheet, extractedComponents);
                    componentWorksheet.Cells[componentWorksheet.Dimension.Address].AutoFitColumns();
                    AddAutoFilterButtons(componentWorksheet);

                    // Save the Excel package to a file
                    package.SaveAs(new FileInfo("ExtractedData.xlsx"));

                    Process.Start("ExtractedData.xlsx");
                }

                // Stop the stopwatch
                stopwatch.Stop();

                Console.WriteLine($"Excel file created successfully. Time elapsed: {stopwatch.Elapsed.TotalSeconds}s");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddAutoFilterButtons(ExcelWorksheet worksheet)
        {
            // Assuming headers are in the first row (row 1)
            worksheet.Cells["A1:" + worksheet.Dimension.End.Address].AutoFilter = true;
        }

        private static void WriteHeaders<T>(ExcelWorksheet worksheet, List<T> objects)
        {
            if (objects.Count == 0)
            {
                // Handle the case where the list is empty
                return;
            }

            PropertyInfo[] properties = objects[0].GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                string header = $"{properties[i].Name}";
                worksheet.Cells[1, i + 1].Value = header;
            }
            worksheet.View.FreezePanes(1, worksheet.Dimension.End.Column + 1);
        }


        private static int GetMaxLength<T>(List<T> objects, PropertyInfo property)
        {
            // Assuming the property is a string type, you may want to modify this logic
            // based on the actual data type of the property.
            int maxLength = property.Name.Length;

            foreach (var obj in objects)
            {
                object value = property.GetValue(obj);
                if (value != null)
                {
                    int length = value.ToString().Length;
                    if (length > maxLength)
                    {
                        maxLength = length;
                    }
                }
            }

            return maxLength;
        }

        private static void WriteDataToSheet<T>(ExcelWorksheet worksheet, List<T> objects)
        {
            if (objects.Count == 0)
            {
                // Handle the case where the list is empty
                return;
            }

            PropertyInfo[] properties = typeof(T).GetProperties();

            for (int row = 0; row < objects.Count; row++)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    var propertyValue = properties[col].GetValue(objects[row]);
                    worksheet.Cells[row + 2, col + 1].Value = propertyValue;
                }
            }
        }
    }
}
