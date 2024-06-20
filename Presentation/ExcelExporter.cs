using Logging;
using System.IO;
using OfficeOpenXml;
using System.Diagnostics;
using System.Reflection;
using Logic;
using UI_Interfaces;
using Microsoft.Graph.Drives.Item.Bundles;
using OfficeOpenXml.Table;
using System.Data;


namespace Presentation
{
    public class ExcelExporter
    {
        private Logger _logger;
        private enum SensitivityLabel
        {
            General,
            Confidential,
            HighlyConfidential
        }
        private string directoryPath;
        public ExcelExporter(Logger logger)
        {
            _logger = logger;
            directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Saber Tool Plus", "TempData");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

        }

        public void CreateProjectExcelSheet(List<iProject_Wire> wires, List<IProject_Component> components, string fileName, List<Profile> profiles)
        {
            try
            {
                // Start the stopwatch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Close any existing instances of "ExtractedData.xlsx"
                foreach (var process in Process.GetProcessesByName("EXCEL"))
                {
                    if (process.MainWindowTitle.Contains($"{fileName}.xlsx"))
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
                    WriteHeaders(wireWorksheet, profiles[0]);

                    // Write wire data
                    WriteDataToSheet(wireWorksheet, wires, profiles[0]);
                    
                    AddAutoFilterButtons(wireWorksheet);

                    // Add a worksheet for Components
                    var componentWorksheet = package.Workbook.Worksheets.Add("Project_Components");

                    //Write column headers for components
                    WriteHeaders(componentWorksheet, profiles[1]);

                    // Write component data
                    WriteDataToSheet(componentWorksheet, components, profiles[1]);
                    AddAutoFilterButtons(componentWorksheet);

                    // Save the Excel package to a file
                    package.SaveAs(new FileInfo(Path.Combine(directoryPath, $"{fileName}.xlsx")));

                    // Get the full path to the Excel file
                    string filePath = Path.Combine(directoryPath, $"{fileName}.xlsx");

                    // Open the Excel file using its default application
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true; //required by .NET core 6 and higher
                    p.StartInfo.FileName = filePath;
                    p.Start();
                }

                // Stop the stopwatch
                stopwatch.Stop();

                _logger.Log($"Project Excel file opened successfully. Time elapsed: {stopwatch.Elapsed.TotalSeconds}s");
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public void CreateExcelSheet(List<iConverted_Wire> wires, List<iConverted_Component> components, string fileName, List<Profile> profiles, List<DSI_Tube> tubes, List<Bundle> bundles, List<bool> selectedSheets)
        {
            try
            {
                // Start the stopwatch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Close any existing instances of "ExtractedData.xlsx"
                foreach (var process in Process.GetProcessesByName("EXCEL"))
                {
                    if (process.MainWindowTitle.Contains($"{fileName}.xlsx"))
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
                    WriteHeaders(wireWorksheet, profiles[0]);

                    // Write wire data
                    WriteDataToSheet(wireWorksheet, wires, profiles[0]);
                    //wireWorksheet.Cells[wireWorksheet.Dimension.Address].AutoFitColumns();
                    AddAutoFilterButtons(wireWorksheet);

                    // Add a worksheet for Components
                    var componentWorksheet = package.Workbook.Worksheets.Add("Components");

                    // Write column headers for components
                    WriteHeaders(componentWorksheet, profiles[1]);

                    // Write component data
                    WriteDataToSheet(componentWorksheet, components, profiles[1]);
                    //componentWorksheet.Cells[componentWorksheet.Dimension.Address].AutoFitColumns();
                    AddAutoFilterButtons(componentWorksheet);

                    var tubeWorksheet = package.Workbook.Worksheets.Add("DSI_Tubing");

                    WriteHeaders_alt(tubeWorksheet, tubes);
                    WriteDataToSheet_alt(tubeWorksheet, tubes);
                    AddAutoFilterButtons(tubeWorksheet);

                    CreateExtraSheets(fileName, wires, components, bundles, selectedSheets, profiles);

                    // Save the Excel package to a file
                    package.SaveAs(new FileInfo(Path.Combine(directoryPath, $"{fileName}.xlsx")));

                    // Get the full path to the Excel file
                    string filePath = Path.Combine(directoryPath, $"{fileName}.xlsx");

                    // Open the Excel file using its default application
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true; //required by .NET core 6 and higher
                    p.StartInfo.FileName = filePath;
                    p.Start();
                }

                // Stop the stopwatch
                stopwatch.Stop();

                _logger.Log($"Excel file created successfully. Time elapsed: {stopwatch.Elapsed.TotalSeconds}s");
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        private void CreateExtraSheets(string fileName, List<iConverted_Wire> wires, List<iConverted_Component> components, List<Bundle> bundles, List<bool> selectedSheets, List<Profile> profiles)
        {
            //PE sheet
            if (selectedSheets[0])
            {
                _logger.Log("Creating PE sheet");
                CreateALL_PE_sheet(fileName, wires);
            }
            //RC sheet
            if (selectedSheets[1])
            {
                _logger.Log("Creating RC sheet");
                CreateRC_Sheets(fileName, wires, components, bundles);
            }
            //OC sheet
            if (selectedSheets[2])
            {
                _logger.Log("Creating OC sheet");
                CreateOC_Sheets(fileName, wires, components, bundles);
            }
            //Custom sheet
            if (selectedSheets[3])
            {
                _logger.Log("Creating Custom sheet");
                CreateCustomSheets(fileName, wires, bundles, profiles);
            }
        }

        public void CreateCustomSheets(string fileName, List<iConverted_Wire> wires, List<Bundle> bundles, List<Profile> profiles)
        {
            string fullPath = Path.Combine(directoryPath, fileName + "_Custom.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullPath)))
            {
                //Create the master sheet
                CreateMasterSheet(excelPackage, wires, bundles, profiles[2]);

                //Create the separate sheets as in the original tool
                foreach (Bundle bundle in bundles)
                {
                    //Create templist to give to CreateIndividualSheet
                    List<Bundle> tempList = new List<Bundle>
                    {
                        bundle
                    };

                    CreateIndividualSheet(excelPackage, wires, tempList, profiles[2]);
                }

                // Save the Excel package
                excelPackage.Save();
                // Open the Excel file using its default application
                Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
            }
        }

        private static void AddAutoFilterButtons(ExcelWorksheet worksheet)
        {
            // Assuming headers are in the first row (row 1)
            worksheet.Cells["A1:" + worksheet.Dimension.End.Address].AutoFilter = true;
        }

        private void WriteHeaders(ExcelWorksheet worksheet, Profile profile)
        {
            if (profile == null)
            {
                // Handle the case where the list is empty
                return;
            }

            PropertyInfo[] properties = typeof(Profile).GetProperties();

            // Get the parameters of the profile
            List<string> parameters = profile.Parameters;

            // Write the parameters as headers
            for (int i = 0; i < parameters.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = parameters[i];
            }

            // Freeze the top row (headers)
            worksheet.View.FreezePanes(2, 1);
        }

        private void WriteHeaders_alt<T>(ExcelWorksheet worksheet, List<T> objects)
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
            worksheet.View.FreezePanes(2, worksheet.Dimension.End.Column + 1);
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

        private void WriteDataToSheet<T>(ExcelWorksheet worksheet, List<T> objects, Profile profile)
        {
            if (objects.Count == 0 || profile == null || profile.Parameters == null || profile.Parameters.Count == 0)
            {
                // Handle the case where the list is empty or profile is null or parameters are not defined
                return;
            }

            PropertyInfo[] properties = typeof(T).GetProperties();

            // Create a mapping between parameter names and column indices
            Dictionary<string, int> columnMap = new Dictionary<string, int>();
            for (int i = 0; i < profile.Parameters.Count; i++)
            {
                string parameter = profile.Parameters[i];
                // Skip empty parameters
                if (string.IsNullOrEmpty(parameter))
                    continue;
                columnMap[parameter] = i + 1; // Add 1 to account for Excel's 1-based indexing
            }

            for (int row = 0; row < objects.Count; row++)
            {
                foreach (var kvp in columnMap)
                {
                    string propertyName = kvp.Key;
                    int columnIndex = kvp.Value;

                    // Find the property with the matching name
                    PropertyInfo property = properties.FirstOrDefault(p => p.Name == propertyName);
                    if (property != null)
                    {
                        // Get the value of the property for the current object
                        var propertyValue = property.GetValue(objects[row]);
                        // Write the value to the corresponding cell in the worksheet
                        worksheet.Cells[row + 2, columnIndex].Value = propertyValue;
                    }
                }
            }
        }

        private static void WriteDataToSheet_alt<T>(ExcelWorksheet worksheet, List<T> objects)
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

        private static object GetCustomPropertyValue(ExcelPackage package, string propertyName)
        {
            // Get the custom properties XML
            var customPropertiesXml = package.Workbook.Properties.CustomPropertiesXml;

            // Check if the custom properties XML is not null
            if (customPropertiesXml != null)
            {
                // Check if the property exists
                var propertyNode = customPropertiesXml.SelectSingleNode($"/Properties/AppProperties[@name='{propertyName}']");
                if (propertyNode != null)
                {
                    // Return the property value
                    return propertyNode.InnerText;
                }
            }

            // Return null if the property is not found
            return null;
        }

        public void CreateALL_PE_sheet(string fileName, List<iConverted_Wire> wires)
        {
            // Ensure unique file name
            string fullPath = Path.Combine(directoryPath, fileName + "_PE.xlsx");

            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullPath)))
            {
                List<iConverted_Wire> wiresToUse = new List<iConverted_Wire>(wires);

                // Prepare ALL_PE profile
                List<string> ALL_PE_Strings = new List<string>();
                string[] stringsToAdd = { "Connector_1", "Port_1", "Wire", "Wire_connection", "Diameter", "Color", "Type", "Code_no", "Length", "Term_1", "Seal_1", "Variant", "Bundle" };
                ALL_PE_Strings.AddRange(stringsToAdd);

                Profile ALL_PE_Profile = new Profile("ALL_PE", ALL_PE_Strings, Data_Interfaces.ProfileType.User);

                int originalCount = wiresToUse.Count;
                for (int i = 0; i < originalCount; i++)
                {
                    Converted_Wire newWire = new Converted_Wire(
                        wiresToUse[i].Wire,
                        wiresToUse[i].Diameter,
                        wiresToUse[i].Color,
                        wiresToUse[i].Type,
                        wiresToUse[i].Code_no, // Assuming part_no corresponds to the Code_no property
                        wiresToUse[i].Length,
                        wiresToUse[i].Connector_2,
                        wiresToUse[i].Port_2,
                        wiresToUse[i].Term_2,
                        wiresToUse[i].Seal_2,
                        wiresToUse[i].Wire_connection,
                        wiresToUse[i].Term_1,
                        wiresToUse[i].Seal_1,
                        wiresToUse[i].Connector_1,
                        wiresToUse[i].Port_1,
                        wiresToUse[i].Variant,
                        wiresToUse[i].Bundle,
                        wiresToUse[i].Loc_1,
                        wiresToUse[i].Loc_2,
                        wiresToUse[i].Temp_Class
                    );

                    string tempConnector = wiresToUse[i].Connector_1;
                    string tempTerm = wiresToUse[i].Term_1;
                    string tempSeal = wiresToUse[i].Seal_1;
                    string tempPort = wiresToUse[i].Port_1;

                    newWire.Connector_1 = wiresToUse[i].Connector_2;
                    newWire.Term_1 = wiresToUse[i].Term_2;
                    newWire.Seal_1 = wiresToUse[i].Seal_2;
                    newWire.Port_1 = wiresToUse[i].Port_2;

                    newWire.Connector_2 = tempConnector;
                    newWire.Term_2 = tempTerm;
                    newWire.Seal_2 = tempSeal;
                    newWire.Port_2 = tempPort;

                    wiresToUse.Add(newWire);
                }

                var wireWorksheet = excelPackage.Workbook.Worksheets["ALL_PE"];
                if (wireWorksheet == null)
                {
                    wireWorksheet = excelPackage.Workbook.Worksheets.Add("ALL_PE");
                }
                else
                {
                    wireWorksheet.Cells.Clear(); // Clear existing worksheet content
                }

                var sortedWires = wiresToUse.OrderBy(wire => wire.Connector_1)
                                   .ThenBy(wire => int.TryParse(wire.Port_1, out int port) ? port : int.MaxValue)
                                   .ToList();

                // Write column headers for wires
                WriteHeaders(wireWorksheet, ALL_PE_Profile);

                // Write wire data
                WriteDataToSheet(wireWorksheet, sortedWires, ALL_PE_Profile);

                AddAutoFilterButtons(wireWorksheet);

                // Save the Excel package
                excelPackage.Save();

                // Open the Excel file using its default application
                Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
            }
        }

        public void CreateRC_Sheets(string fileName, List<iConverted_Wire> wires, List<iConverted_Component> components, List<Bundle> selectedBundles)
        {
            string fullPath = Path.Combine(directoryPath, fileName + "_RC.xlsx");
            CoCoHandler cocoHandler = new CoCoHandler(_logger);


            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullPath)))
            {
                //Prepare RC profile
                List<string> RC_Profile_List = new List<string>();
                string[] stringsToAdd = { "Connector_1", "Port_1", "Wire", "Wire_connection", "Diameter", "Color", "Length", "Term_1", "Seal_1", "CC_T", "CC_S", "Temp_Class", "Type", "Code_no", "Variant", "Bundle" }; //TODO: add CC_t and CC_s
                RC_Profile_List.AddRange(stringsToAdd);

                Profile RC_Profile = new Profile("RC", RC_Profile_List, Data_Interfaces.ProfileType.User);

                List<iConverted_Wire> wiresToUse = new List<iConverted_Wire>(wires);

                int originalCount = wiresToUse.Count;
                for (int i = 0; i < originalCount; i++)
                {
                    Converted_Wire newWire = new Converted_Wire(
                        wiresToUse[i].Wire,
                        wiresToUse[i].Diameter,
                        wiresToUse[i].Color,
                        wiresToUse[i].Type,
                        wiresToUse[i].Code_no, // Assuming part_no corresponds to the Code_no property
                        wiresToUse[i].Length,
                        wiresToUse[i].Connector_2,
                        wiresToUse[i].Port_2,
                        wiresToUse[i].Term_2,
                        wiresToUse[i].Seal_2,
                        wiresToUse[i].Wire_connection,
                        wiresToUse[i].Term_1,
                        wiresToUse[i].Seal_1,
                        wiresToUse[i].Connector_1,
                        wiresToUse[i].Port_1,
                        wiresToUse[i].Variant,
                        wiresToUse[i].Bundle,
                        wiresToUse[i].Loc_1,
                        wiresToUse[i].Loc_2,
                        wiresToUse[i].Temp_Class
                    );

                    string tempConnector = wiresToUse[i].Connector_1;
                    string tempTerm = wiresToUse[i].Term_1;
                    string tempSeal = wiresToUse[i].Seal_1;
                    string tempPort = wiresToUse[i].Port_1;

                    newWire.Connector_1 = wiresToUse[i].Connector_2;
                    newWire.Term_1 = wiresToUse[i].Term_2;
                    newWire.Seal_1 = wiresToUse[i].Seal_2;
                    newWire.Port_1 = wiresToUse[i].Port_2;

                    newWire.Connector_2 = tempConnector;
                    newWire.Term_2 = tempTerm;
                    newWire.Seal_2 = tempSeal;
                    newWire.Port_2 = tempPort;
                    
                    wiresToUse.Add(newWire);
                }

                int term1;
                int term2;
                int seal1;
                int seal2;

                foreach (iConverted_Wire wire in  wiresToUse)
                {
                    // Attempt to parse Term_1
                    if (!string.IsNullOrEmpty(wire.Term_1) && int.TryParse(wire.Term_1, out term1))
                    {
                        wire.CC_T = cocoHandler.GetTerminalIdFromLocalDatabase(term1);
                    }
                    if (string.IsNullOrEmpty(wire.CC_T) && !string.IsNullOrEmpty(wire.Term_2) && int.TryParse(wire.Term_2, out term2))
                    {
                        wire.CC_T = cocoHandler.GetTerminalIdFromLocalDatabase(term2);
                    }

                    // Attempt to parse Seal_1
                    if (!string.IsNullOrEmpty(wire.Seal_1) && int.TryParse(wire.Seal_1, out seal1))
                    {
                        wire.CC_S = cocoHandler.GetScatIdFromLocalDatabase(seal1);
                    }
                    if (string.IsNullOrEmpty(wire.CC_S) && !string.IsNullOrEmpty(wire.Seal_2) && int.TryParse(wire.Seal_2, out seal2))
                    {
                        wire.CC_S = cocoHandler.GetTerminalIdFromLocalDatabase(seal2);
                    }
                }

                //Create the master sheet
                AddConnectorDataToSheet(CreateMasterSheet(excelPackage, wiresToUse, selectedBundles, RC_Profile), components);

                //Create the separate sheets as in the original tool
                foreach (Bundle bundle in selectedBundles)
                {
                    //Create templist to give to CreateIndividualSheet
                    List<Bundle> tempList = new List<Bundle>
                    {
                        bundle
                    };

                    AddConnectorDataToSheet(CreateIndividualSheet(excelPackage, wiresToUse, tempList, RC_Profile), components);
                }
                


                // Save the Excel package
                excelPackage.Save();

                // Open the Excel file using its default application
                Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
            }
        }

        private ExcelWorksheet CreateIndividualSheet(ExcelPackage excelPackage, List<iConverted_Wire> wires, List<Bundle> bundles, Profile sheetProfile)
        {
            List<iConverted_Wire> wiresToUse = wires;
            string bundleNumber = bundles[0].VariantNumber;
            string sheetName = sheetProfile.Name + "_" + bundleNumber;

            // Check if the worksheet already exists
            var wireWorksheet = excelPackage.Workbook.Worksheets[sheetName];
            if (wireWorksheet != null)
            {
                // Clear existing worksheet content
                wireWorksheet.Cells.Clear();
            }
            else
            {
                // Add new worksheet if it doesn't exist
                wireWorksheet = excelPackage.Workbook.Worksheets.Add(sheetName);
            }

            // Filter the wiresToUse collection to include only those wires 
            // where the Variant matches the given bundleNumber.
            // Then, order the filtered wires by Connector_1 and Port_1 (parsed as integers if possible).
            var sortedWires = wiresToUse
                .Where(wire => wire.Bundle.Split(' ').Any(bundleNumber => bundles.Any(bundle => bundle.VariantNumber == bundleNumber)))
                .OrderBy(wire => wire.Connector_1)
                .ThenBy(wire => int.TryParse(wire.Port_1, out int port) ? port : int.MaxValue)
                .ToList();

            // Write column headers for wires
            WriteHeaders(wireWorksheet, sheetProfile);

            // Write wire data
            WriteDataToSheet(wireWorksheet, sortedWires, sheetProfile);

            AddAutoFilterButtons(wireWorksheet);

            return wireWorksheet;
        }

        private ExcelWorksheet CreateMasterSheet(ExcelPackage excelPackage, List<iConverted_Wire> wires, List<Bundle> bundles, Profile sheetProfile)
        {
            List<iConverted_Wire> wiresToUse = wires;
            string bundleNumber = bundles[0].VariantNumber;
            string sheetName = sheetProfile.Name + "_ALL";

            // Check if the worksheet already exists
            var wireWorksheet = excelPackage.Workbook.Worksheets[sheetName];
            if (wireWorksheet != null)
            {
                // Clear existing worksheet content
                wireWorksheet.Cells.Clear();
            }
            else
            {
                // Add new worksheet if it doesn't exist
                wireWorksheet = excelPackage.Workbook.Worksheets.Add(sheetName);
            }

            // Filter the wiresToUse collection to include only those wires 
            // where the Variant matches the given bundleNumber.
            // Then, order the filtered wires by Connector_1 and Port_1 (parsed as integers if possible).
            var sortedWires = wiresToUse
                .Where(wire => wire.Bundle.Split(' ').Any(bundleNumber => bundles.Any(bundle => bundle.VariantNumber == bundleNumber)))
                .OrderBy(wire => wire.Connector_1)
                .ThenBy(wire => int.TryParse(wire.Port_1, out int port) ? port : int.MaxValue)
                .ToList();

            // Write column headers for wires
            WriteHeaders(wireWorksheet, sheetProfile);

            // Write wire data
            WriteDataToSheet(wireWorksheet, sortedWires, sheetProfile);

            AddAutoFilterButtons(wireWorksheet);

            return wireWorksheet;
        }

        public void CreateOC_Sheets(string fileName, List<iConverted_Wire> wires, List<iConverted_Component> components, List<Bundle> selectedBundles)
        {
            string fullPath = Path.Combine(directoryPath, fileName + "_OC.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullPath)))
            {
                //Prepare OC profile
                List<string> OC_Profile_List = new List<string>();
                string[] stringsToAdd = { "Connector_1", "Port_1", "Wire", "Diameter", "Color", "Type", "Code_no", "Term_1", "Seal_1",
                "", "Length table", "", "", "Weight (Kg)", "Length", "Wire_connection", "Variant", "Bundle"}; //TODO: implement/find Weight KG
                OC_Profile_List.AddRange(stringsToAdd);

                Profile OC_Profile = new Profile("OC", OC_Profile_List, Data_Interfaces.ProfileType.User);

                List<iConverted_Wire> wiresToUse = new List<iConverted_Wire>(wires);

                int originalCount = wiresToUse.Count;
                for (int i = 0; i < originalCount; i++)
                {
                    Converted_Wire newWire = new Converted_Wire(
                        wiresToUse[i].Wire,
                        wiresToUse[i].Diameter,
                        wiresToUse[i].Color,
                        wiresToUse[i].Type,
                        wiresToUse[i].Code_no, // Assuming part_no corresponds to the Code_no property
                        wiresToUse[i].Length,
                        wiresToUse[i].Connector_2,
                        wiresToUse[i].Port_2,
                        wiresToUse[i].Term_2,
                        wiresToUse[i].Seal_2,
                        wiresToUse[i].Wire_connection,
                        wiresToUse[i].Term_1,
                        wiresToUse[i].Seal_1,
                        wiresToUse[i].Connector_1,
                        wiresToUse[i].Port_1,
                        wiresToUse[i].Variant,
                        wiresToUse[i].Bundle,
                        wiresToUse[i].Loc_1,
                        wiresToUse[i].Loc_2,
                        wiresToUse[i].Temp_Class
                    );

                    string tempConnector = wiresToUse[i].Connector_1;
                    string tempTerm = wiresToUse[i].Term_1;
                    string tempSeal = wiresToUse[i].Seal_1;
                    string tempPort = wiresToUse[i].Port_1;

                    newWire.Connector_1 = wiresToUse[i].Connector_2;
                    newWire.Term_1 = wiresToUse[i].Term_2;
                    newWire.Seal_1 = wiresToUse[i].Seal_2;
                    newWire.Port_1 = wiresToUse[i].Port_2;

                    newWire.Connector_2 = tempConnector;
                    newWire.Term_2 = tempTerm;
                    newWire.Seal_2 = tempSeal;
                    newWire.Port_2 = tempPort;

                    wiresToUse.Add(newWire);
                }

                //Create the master sheet
                AddConnectorDataToSheet(CreateMasterSheet(excelPackage, wires, selectedBundles, OC_Profile), components);

                //Create the separate sheets as in the original tool
                foreach (Bundle bundle in selectedBundles)
                {
                    //Create templist to give to CreateIndividualSheet
                    List<Bundle> tempList = new List<Bundle>
                    {
                        bundle
                    };

                    AddConnectorDataToSheet(CreateIndividualSheet(excelPackage, wires, tempList, OC_Profile), components);
                }

                // Save the Excel package
                excelPackage.Save();

                // Open the Excel file using its default application
                Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
            }
        }

        private void AddConnectorDataToSheet(ExcelWorksheet worksheet, List<iConverted_Component> components)
        {
            int rowCount = worksheet.Dimension.Rows;
            string nextConnector = worksheet.Cells[1, 1].Text;

            for (int i = 1; i <= rowCount; i++)
            {
                string currentConnector = worksheet.Cells[i, 1].Text;

                // Check if the current connector is the next occurrence (different from nextConnector)
                if (currentConnector != nextConnector)
                {
                    var matchingComponent = components.Find(comp => comp.Name == currentConnector);
                    if (matchingComponent != null)
                    {
                        // Insert a new row after the current row
                        worksheet.InsertRow(i + 1, 1);

                        // Add connector information in the new row, in the cells right next to the current connector
                        worksheet.Cells[i + 1, 1].Value = currentConnector;
                        worksheet.Cells[i + 1, 2].Value = matchingComponent.Part_no;
                        worksheet.Cells[i + 1, 3].Value = matchingComponent.EndText;

                        // Insert an empty row after the connector data row
                        worksheet.InsertRow(i + 2, 1);

                        rowCount += 2; // Increment the row count since two new rows are added
                        i += 2; // Skip the newly added rows
                    }
                }

                nextConnector = worksheet.Cells[i + 1, 1].Text; // Update nextConnector to currentConnector
            }
        }




        public void CreateExcelReport(Dictionary<string, string> CombinedFailures, string fileName)
        {
            string filePath = Path.Combine(directoryPath, $"{fileName}_testresults.xlsx");

            // Delete the file if it exists
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Test Results");

                // Adding headers
                worksheet.Cells[1, 1].Value = "Failed Object";
                worksheet.Cells[1, 2].Value = "Failed Tests";

                // Adding data
                int row = 2;
                foreach (var failure in CombinedFailures)
                {
                    worksheet.Cells[row, 1].Value = failure.Key.ToString();
                    worksheet.Cells[row, 2].Value = failure.Value;
                    row++;
                }

                // Adding table format for sorting and filtering
                var dataRange = worksheet.Cells[1, 1, row - 1, 2];
                var table = worksheet.Tables.Add(dataRange, "FailuresTable");
                table.TableStyle = TableStyles.Medium9;
                //table.ShowFilter = true;

                // AutoFit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Save the file
                var fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);

                // Automatically open the file
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
        }
    }
}
