using Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Management;
public class FolderPaths
{
    //DAF file server
    public string DAF_Production = @"\SaberRelease\Production";
    public string DAF_Reldas = @"\SaberRelease\Designer\Boms";
    public string DAF_Designer = @"\SaberWiP\2_Users\designs\BSA\Boms";

    //Leyland file server
    public string LEY_Production = @"\SaberRelease\Production";
    public string LEY_Reldas = @"\SaberRelease\Designer\Boms";
    public string LEY_Designer = @"\SaberWiP\2_Users\designs\BSA\Boms";

    //I believe there is a third file server

    //My local drive!
    public string ExurbiaLocal = @"C:\Users\tomvh\Documents\School\S5 - Internship\boms";

    public bool HasFoundDAF { get; private set; }
    public bool HasFoundLEY { get; private set; }

    private Logger _logger;
    public FolderPaths(Logger logger)
    {
        _logger = logger;



        string dafNetworkPath = @"\\app-p-SABER.eu.paccar.com\Saber\DATA";
        string dafDriveLetter = GetDriveLetter(dafNetworkPath);

        string leylandNetworkPath = @"\\eu.paccar.com\LEYLEY\Saber";
        string leylandDriveLetter = GetDriveLetter(leylandNetworkPath);

        HasFoundDAF = !dafDriveLetter.IsNullOrEmpty();
        HasFoundLEY = !leylandNetworkPath.IsNullOrEmpty();

        if (!dafDriveLetter.IsNullOrEmpty())
        {
            DAF_Designer = ConcatenateStrings(dafDriveLetter, DAF_Designer);
            DAF_Production = ConcatenateStrings(dafDriveLetter, DAF_Production);
            DAF_Reldas = ConcatenateStrings(dafDriveLetter, DAF_Reldas);
        }
        else if (!leylandDriveLetter.IsNullOrEmpty())
        {
            LEY_Designer = ConcatenateStrings(leylandDriveLetter, LEY_Designer);
            LEY_Production = ConcatenateStrings(leylandDriveLetter, LEY_Production);
            LEY_Reldas = ConcatenateStrings(leylandDriveLetter, LEY_Reldas);
        }
    }

    private void ListNetworkDrives()
    {
        string query = "SELECT Name, VolumeName, ProviderName FROM Win32_LogicalDisk WHERE DriveType=4";
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

        foreach (ManagementObject mo in searcher.Get())
        {
            string driveLetter = mo["Name"]?.ToString();
            string volumeName = mo["VolumeName"]?.ToString() ?? "No Volume Name";
            string networkPath = mo["ProviderName"]?.ToString();

           _logger.Log($"Volume Name: {volumeName}, Network Path: {networkPath}, Drive Letter: {driveLetter}");
        }
    }

    private string GetDriveLetter(string networkPath)
    {
        string query = "SELECT Name, ProviderName FROM Win32_LogicalDisk WHERE DriveType=4";
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

        foreach (ManagementObject mo in searcher.Get())
        {
            string name = mo["Name"]?.ToString();
            string providerName = mo["ProviderName"]?.ToString();

            if (providerName != null && providerName.Equals(networkPath, StringComparison.OrdinalIgnoreCase))
            {
                return name;
            }
        }

        return null;
    }

    private string ConcatenateStrings(string str1, string str2)
    {
        return str1 + str2;
    }
}