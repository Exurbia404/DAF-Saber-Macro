using System.Diagnostics;
using OfficeOpenXml;
using Logic;
using Logging;

namespace Data_Access
{
    public class ExcelImporter
    {
        private string dsiDataSetLocation = @"U:\Data\SaberWiP\3_Key_Users\www\Macro\Office10\Importeren_SABER_DATA.xlsm";
        private string LocalDataSetLocation = @"C:\Users\tomvh\Documents\School\S5 - Internship\Original tool\Importeren_SABER_DATA.xlsm";
        public List<DSI_Reference> DSIReferences { get; private set; }
        private Logger _logger;
        public ExcelImporter(Logger logger) 
        {
            _logger = logger;
            DSIReferences = GetDataSetFromExcelSheet(dsiDataSetLocation);
        }

        private List<DSI_Reference> GetDataSetFromExcelSheet(string filePath)
        {
            string computerName = Environment.MachineName;

            if(computerName == "EXURBIA")
            {
                filePath = LocalDataSetLocation;
            }

            string searchTerm = "jrwk";
            string sheetName = "DATASET";

            List<DSI_Reference> foundReferences = new List<DSI_Reference>();

            // Start the stopwatch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                // Get the worksheet by name
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                if (worksheet != null)
                {
                    // Search for "jrwk" in the second row
                    int startColumn = 1; // Assuming the search starts from the first column
                    int endColumn = worksheet.Dimension.End.Column;
                    int rowToSearch = 2; // Search in the second row

                    List<int> columnIndices = new List<int>();

                    // Find all occurrences of "jrwk"
                    for (int col = startColumn; col <= endColumn; col++)
                    {
                        if (worksheet.Cells[rowToSearch, col].Text.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                        {
                            columnIndices.Add(col);
                        }
                    }

                    // Process each occurrence
                    foreach (int columnIndex in columnIndices)
                    {
                        int currentRow = rowToSearch + 1; // Start from the row beneath "jrwk"

                        //Get the project name, from right next to the "jrwk" cell
                        string projectName = worksheet.Cells[rowToSearch, columnIndex + 1].Text;
                        
                        // Process rows until an empty cell is encountered
                        while (!string.IsNullOrEmpty(worksheet.Cells[currentRow, columnIndex].Text))
                        {
                            // Create DSI_Reference objects with different offsets
                            DSI_Reference reference = new DSI_Reference
                            {
                                YearWeek = worksheet.Cells[currentRow, columnIndex].Text,
                                BundleNumber = worksheet.Cells[currentRow, columnIndex + 1].Text,
                                ProjectName = projectName,
                                Description = worksheet.Cells[currentRow, columnIndex + 3].Text
                                // ^ Adjusted to get the value from the cell to the right of "jrwk"
                            };

                            foundReferences.Add(reference);

                            currentRow++;
                        }
                    }

                    if (foundReferences.Count == 0)
                    {
                        _logger.Log($"No occurrences of the search term '{searchTerm}' found in the second row.");
                    }

                    // Stop the stopwatch
                    stopwatch.Stop();

                    _logger.Log($"References imported in: {stopwatch.Elapsed.TotalSeconds}s");

                    return foundReferences;
                }
                else
                {
                    _logger.Log($"Sheet '{sheetName}' not found.");
                    return null;
                }
            }

        }

    }
}
