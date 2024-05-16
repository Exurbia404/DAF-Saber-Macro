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

        public string LocalFolder = FolderPaths.ExurbiaLocal;

        public Settings()
        {
            //Set default file server to DAF
            SwitchFileServer(FileServers.DAF);
        }

        public void SwitchFileServer(FileServers name)
        {
            switch (name)
            {
                case FileServers.DAF:
                    ProductionFolder = FolderPaths.DAF_Production;
                    ReldasFolder = FolderPaths.DAF_Reldas;
                    DesignerFolder = FolderPaths.DAF_Designer;
                    break;
                case FileServers.Leyland:
                    ProductionFolder = FolderPaths.LEY_Production;
                    ReldasFolder = FolderPaths.LEY_Reldas;
                    DesignerFolder = FolderPaths.LEY_Designer;
                    break;
                case FileServers.Other:
                    // Assign the folder paths for the "Other" file server here
                    // For now, let's assign them the same as DAF
                    ProductionFolder = FolderPaths.DAF_Production;
                    ReldasFolder = FolderPaths.DAF_Reldas;
                    DesignerFolder = FolderPaths.DAF_Designer;
                    break;
                default:
                    throw new ArgumentException("Invalid file server name");
            }
        }
    }
}
