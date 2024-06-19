using System;
using System.Collections.Generic;
using System.IO;
using Logging;
using Newtonsoft.Json;

public class CoCoHandler
{
    private string TerminalJSONFilePath = @"W:\PD-Saber\5_PD_HD\Planning\Saber Tool Plus\Data\TerminalDatabase.json";
    private string ScatJSONFilePath = @"W:\PD-Saber\5_PD_HD\Planning\Saber Tool Plus\Data\ScatDatabase.json";

    private string ExurbiaTerminal = @"C:\Users\tomvh\Documents\School\S5 - Internship\Data\TerminalDatabase.json";
    private string ExurbiaScat = @"C:\Users\tomvh\Documents\School\S5 - Internship\Data\ScatDatabase.json";

    private Logger _logger;
    private List<Connector> _terminalData;
    private List<Connector> _scatData;
    //test
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
}

public class Connector
{
    public int Partnumber { get; set; }
    public int ID { get; set; }
}

