using Data_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class ProfileController
    {
        //Default profiles, user can create their own based on these
        private List<string> Wire_WCSPP_Profile_Options_List = new List<string>()
        {
            "Wire",
            "Diameter",
            "Color",
            "Type",
            "Code_no",
            "Length",
            "Connector_1",
            "Port_1",
            "Term_1",
            "Seal_1",
            "Wire_connection",
            "Term_2",
            "Seal_2",
            "Connector_2",
            "Port_2",
            "Variant",
            "Bundle",
            "Loc_1",
            "Loc_2"
        };

        private List<string> Wire_Profile_Options_List = new List<string>()
        {
            "WireName",
            "WireOption",
            "WireType",
            "Color",
            "CrossSectionalArea",
            "Material",
            "UserModule",
            "MulticoreName",
            "End1NodeName",
            "End1Route",
            "End1Cavity",
            "End1MaterialCode",
            "End2NodeName",
            "End2Route",
            "End2Cavity",
            "End2MaterialCode",
            "IncludeOnBOM",
            "IncludeOnChart",
            "WireTag",
            "WireNote",
            "WireLengthChangeType",
            "WireLengthChangeValue",
            "AssemblyItemNumber",
            "MulticoreOption"
        };

        private List<string> Wire_Project_Profile_Options_List = new List<string>()
        {
            "Wire",
            "Diameter",
            "Color",
            "Type",
            "Connector_1",
            "Port_1",
            "Term_1",
            "Seal_1",
            "Lokation_1",
            "Wire_connection",
            "Term_2",
            "Seal_2",
            "Connector_2",
            "Port_2",
            "Lokation_2",
            "Harness",
            "Variant",
            "Bundle",
            "Kodenr_wire",
            "Tag"
        };

        private List<string> Component_WCSPP_Profile_Options_List = new List<string>()
        {
            "Name",
            "Part_no",
            "Passive",
            "Instruction",
            "Variant",
            "Bundle",
            "Description",
            "Lokation",
            "EndText"
        };

        private List<string> Component_Profile_Options_List = new List<string>()
        {
            "NodeName",
            "CavityName",
            "WireName",
            "SequenceNumber",
            "ComponentTypeCode",
            "CircuitOption",
            "ServiceFunction",
            "Route",
            "PartNumber1",
            "Quantity",
            "CrossSectionalArea",
            "PartNumber2",
            "PartNumber3",
            "SelectTerminal",
            "Seal",
            "Plugged",
            "BlockNumber",
            "TerminationMethod",
            "MaterialCode",
            "ComponentTypeCode2"
        };

        private List<string> Component_Project_Profile_Options_List = new List<string>()
        {
            "Type",
            "Ref",
            "Description",
            "Location",
            "Connector",
            "SecLock",
            "Harness",
            "Variant",
            "Bundle",
            "Tag",
            "System",
            "Fuse_value",
            "Color",
            "N_pins"
        };

        //TODO: not yet implemented
        //private ProfileController profileController;

        public List<Profile> defaultProfiles;
        public List<Profile> userProfiles;

        public List<Profile> allProfiles
        {
            get { return defaultProfiles.Concat(userProfiles).ToList(); }
        }

        private iFileHandler fileHandler;
        public ProfileController(iFileHandler filehandler)
        {
            defaultProfiles = new List<Profile>();
            fileHandler = filehandler;

            userProfiles = LoadProfiles();

            InitializeDefaultProfiles();
        }

        private void InitializeDefaultProfiles()
        { 
            defaultProfiles.Add(new Profile("Wire WCSPP Profile", Wire_WCSPP_Profile_Options_List, ProfileType.Wire));
            defaultProfiles.Add(new Profile("Wire Profile", Wire_Profile_Options_List, ProfileType.Wire));
            defaultProfiles.Add(new Profile("Wire Project Profile", Wire_Project_Profile_Options_List, ProfileType.Project_Wire));
            defaultProfiles.Add(new Profile("Component WCSPP Profile", Component_WCSPP_Profile_Options_List, ProfileType.Component));
            defaultProfiles.Add(new Profile("Component Profile", Component_Profile_Options_List, ProfileType.Component));
            defaultProfiles.Add(new Profile("Component Project Profile", Component_Project_Profile_Options_List, ProfileType.Project_Component));
        }

        public void SaveProfiles()
        {
            fileHandler.SaveProfiles(allProfiles.Cast<iProfile>().ToList());
        }

        public List<Profile> LoadProfiles()
        {
            List<Profile> foundProfiles = new List<Profile>();

            foreach (iProfile loadedProfile in fileHandler.LoadProfiles()) 
            {
                if(loadedProfile.Type == ProfileType.User)
                {
                    foundProfiles.Add(new Profile(loadedProfile.Name, loadedProfile.Parameters, loadedProfile.Type));
                }
            }
            return foundProfiles;
        }
    }
}
