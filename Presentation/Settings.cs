using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class Settings
    {
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
        private FolderPaths folderPaths;
        private Logger _logger;

        public Settings(Logger logger)
        {
            //Set default file server to DAF
            folderPaths = new FolderPaths(logger);
            LocalFolder = folderPaths.ExurbiaLocal;
            SwitchFileServer(FileServers.DAF);
        }

        public void SwitchFileServer(FileServers name)
        {
            switch (name)
            {
                case FileServers.DAF:
                    ProductionFolder = folderPaths.DAF_Production;
                    ReldasFolder = folderPaths.DAF_Reldas;
                    DesignerFolder = folderPaths.DAF_Designer;
                    break;
                case FileServers.Leyland:
                    ProductionFolder = folderPaths.LEY_Production;
                    ReldasFolder = folderPaths.LEY_Reldas;
                    DesignerFolder = folderPaths.LEY_Designer;
                    break;
                case FileServers.Other:
                    // Assign the folder paths for the "Other" file server here
                    // For now, let's assign them the same as DAF
                    ProductionFolder = folderPaths.DAF_Production;
                    ReldasFolder = folderPaths.DAF_Reldas;
                    DesignerFolder = folderPaths.DAF_Designer;
                    break;
                default:
                    throw new ArgumentException("Invalid file server name");
            }
        }
    }
}
