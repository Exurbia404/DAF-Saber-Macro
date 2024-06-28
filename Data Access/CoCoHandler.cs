using System;
using System.Collections.Generic;
using System.IO;
using Logging;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

public class CoCoHandler
{
    private string TerminalJSONFilePath = @"W:\PD-Saber\5_PD_HD\Planning\Saber Tool Plus\Data\TerminalDatabase.json";
    private string ScatJSONFilePath = @"W:\PD-Saber\5_PD_HD\Planning\Saber Tool Plus\Data\ScatDatabase.json";

    private string TerminalExcelFilePath = @"W:\PD-Saber\5_PD_HD\Planning\Saber Tool Plus\Data\Terminal_Coco.xlsx";
    private string ScatExcelFilePath = @"W:\PD-Saber\5_PD_HD\Planning\Saber Tool Plus\Data\Scat_Coco.xlsx";

    private string ExurbiaTerminal = @"C:\Users\tomvh\Documents\School\S5 - Internship\Data\TerminalDatabase.json";
    private string ExurbiaScat = @"C:\Users\tomvh\Documents\School\S5 - Internship\Data\ScatDatabase.json";

    private string ExurbiaTerminalExcel = @"C:\Users\tomvh\Documents\School\S5 - Internship\Data\Terminal_Coco.xlsx";
    private string ExurbiaScatExcel = @"C:\Users\tomvh\Documents\School\S5 - Internship\Data\Scat_Coco.xlsx";

    private Logger _logger;
    private List<Connector> _terminalData;
    private List<Connector> _scatData;

    public CoCoHandler(Logger logger)
    {
        _logger = logger;

        if(Environment.MachineName == "EXURBIA")
        {
            TerminalJSONFilePath = ExurbiaTerminal;
            ScatJSONFilePath = ExurbiaScat; 
        }

        _terminalData = LoadJsonData(TerminalJSONFilePath);
        _scatData = LoadJsonData(ScatJSONFilePath);
    }

    public CoCoHandler(string terminalJSONFilePath, string scatJSONFilePath)
    {
        _terminalData = LoadJsonData(terminalJSONFilePath);
        _scatData = LoadJsonData(scatJSONFilePath);
    }

    private List<Connector> LoadJsonData(string filePath)
    {
        try
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Connector>>(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading JSON file at {filePath}: {ex.Message}");
            return new List<Connector>();
        }
    }

    public string GetTerminalIdFromLocalDatabase(int connectorNumber)
    {
        var connector = _terminalData.Find(c => c.Partnumber == connectorNumber);
        return connector != null ? connector.ID.ToString() : null;
    }

    public string GetScatIdFromLocalDatabase(int connectorNumber)
    {
        var connector = _scatData.Find(c => c.Partnumber == connectorNumber);
        return connector != null ? connector.ID.ToString() : null;
    }

    public void UpdateCoCoUsingExcel()
    {
        string terminalPath = TerminalExcelFilePath;
        string scatPath = ScatExcelFilePath;

        if (Environment.MachineName == "EXURBIA")
        {
            terminalPath = ExurbiaTerminalExcel;
            scatPath = ExurbiaScatExcel;
        }
        List<ExcelRecord> TerminalRecords = ReadExcelDatabase(terminalPath);

        UpdateJsonDatabase(_terminalData, TerminalRecords);
        SaveJsonDatabase(TerminalJSONFilePath, _terminalData);

        List<ExcelRecord> ScatRecords = ReadExcelDatabase(scatPath);

        UpdateJsonDatabase(_scatData, ScatRecords);
        SaveJsonDatabase(ScatJSONFilePath, _scatData);
    }

    private List<ExcelRecord> ReadExcelDatabase(string filePath)
    {
        _logger.Log("Trying to read Excel CoCo database");
        List<ExcelRecord> excelRecords = new List<ExcelRecord>();

        using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
        {
            WorkbookPart workbookPart = doc.WorkbookPart;
            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault();
            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();

            foreach (Row row in sheetData.Elements<Row>().Skip(1)) // Skip header row
            {
                ExcelRecord record = new ExcelRecord
                {
                    ContactID = int.Parse(GetCellValue(row.Elements<Cell>().ElementAt(0), workbookPart)),
                    ContactDAFnr = int.Parse(GetCellValue(row.Elements<Cell>().ElementAt(1), workbookPart))
                };
                excelRecords.Add(record);
            }
        }

        return excelRecords;
    }

    private string GetCellValue(Cell cell, WorkbookPart workbookPart)
    {
        if (cell.CellValue == null) return null;

        string value = cell.CellValue.InnerText;

        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable.ChildElements[int.Parse(value)].InnerText;
        }
        return value;
    }

    private void UpdateJsonDatabase(List<Connector> jsonDatabase, List<ExcelRecord> excelDatabase)
    {
        _logger.Log("Updating JSON database");
        foreach (var excelRecord in excelDatabase)
        {
            var jsonRecord = jsonDatabase.Find(record => record.ID == excelRecord.ContactID);
            if (jsonRecord != null)
            {
                // Update existing record
                jsonRecord.Partnumber = excelRecord.ContactDAFnr;
            }
            else
            {
                // Add new record
                jsonDatabase.Add(new Connector
                {
                    ID = excelRecord.ContactID,
                    Partnumber = excelRecord.ContactDAFnr
                });
            }
        }
    }

    private void SaveJsonDatabase(string filePath, List<Connector> jsonDatabase)
    {
        _logger.Log("Saving JSON database");
        string updatedJson = JsonConvert.SerializeObject(jsonDatabase, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson);
    }
}

public class Connector
{
    public int Partnumber { get; set; }
    public int ID { get; set; }
}

public class ExcelRecord
{
    public int ContactID { get; set; }
    public int ContactDAFnr { get; set; }
}

