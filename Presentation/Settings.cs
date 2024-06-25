using Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class Settings
    {
        public string Version = "Gold 2.2";

        public enum FileServers
        {
            DAF,
            Leyland,
            Other
        }
        public string ProductionFolder { get; private set; }
        public string ReldasFolder { get; private set; }
        public string DesignerFolder {  get; private set; }

        public string LocalFolder;
        public string LocalProductionFolder;
        public FolderPaths FolderPaths;
        private Logger _logger;

        public Settings(Logger logger)
        {
            //Set default file server to DAF
            FolderPaths = new FolderPaths(logger);
            LocalFolder = FolderPaths.Exurbia_Designer;
            LocalProductionFolder = FolderPaths.Exurbia_Production;
            _logger = logger;
            
            if(Environment.MachineName == "EXURBIA")
            {
                _logger.Log("Switching to local");
                SwitchFileServer(FileServers.Other);
            }
            else
            {
                SetDefaultFileServer();

            }
        }

        private void SetDefaultFileServer()
        {
            string defaultDrive = ConfigurationManager.AppSettings["DefaultDrive"];

            switch (defaultDrive)
            {
                case "DAF":
                    _logger.Log("Switching to DAF");
                    SwitchFileServer(FileServers.DAF);
                    break;
                case "LEY":
                    _logger.Log("Switching to Leyland");
                    SwitchFileServer(FileServers.Leyland);
                    break;
                case "Exurbia":
                    _logger.Log("Switching to local");
                    SwitchFileServer(FileServers.Other);
                    break;
                default:
                    _logger.Log($"Unknown default drive setting: {defaultDrive}. Defaulting to DAF.");
                    SwitchFileServer(FileServers.DAF); // Default to DAF if setting is unrecognized
                    break;
            }
        }

        public void SwitchFileServer(FileServers name)
        {
            switch (name)
            {
                case FileServers.DAF:
                    ProductionFolder = FolderPaths.DAF_Production;
                    ReldasFolder = FolderPaths.DAF_Reldas;
                    DesignerFolder = FolderPaths.DAF_Designer;
                    UpdateConfigFile("DAF");
                    break;
                case FileServers.Leyland:
                    ProductionFolder = FolderPaths.LEY_Production;
                    ReldasFolder = FolderPaths.LEY_Reldas;
                    DesignerFolder = FolderPaths.LEY_Designer;
                    UpdateConfigFile("LEY");
                    break;
                case FileServers.Other:
                    // Assign the folder paths for the "Other" file server here
                    ProductionFolder = FolderPaths.Exurbia_Production;
                    ReldasFolder = FolderPaths.Exurbia_Reldas;
                    DesignerFolder = FolderPaths.Exurbia_Designer;
                    //UpdateConfigFile("Exurbia");
                    break;
                default:
                    throw new ArgumentException("Invalid file server name");
            }
        }

        private void UpdateConfigFile(string serverName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Update the setting in appSettings section
            config.AppSettings.Settings["DefaultDrive"].Value = serverName;

            // Save the changes to the configuration file
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of the configuration file
            ConfigurationManager.RefreshSection("appSettings");

            _logger.Log($"Changed default file server to {serverName}");
        }
    }
}
