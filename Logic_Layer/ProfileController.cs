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
        private enum Wire_WCSPP_Profile_Options
        {
            Wire,
            Diameter,
            Color,
            Type,
            Code_no,
            Length,
            Connector_1,
            Port_1,
            Term_1,
            Seal_1,
            Wire_connection,
            Term_2,
            Seal_2,
            Connector_2,
            Port_2,
            Variant,
            Bundle,
            Loc_1,
            Loc_2,
        }
        private enum Wire_Profile_Options
        {
            WireName,
            WireOption,
            WireType,
            Color,
            CrossSectionalArea,
            Material,
            UserModule,
            MulticoreName,
            End1NodeName,
            End1Route,
            End1Cavity,
            End1MaterialCode,
            End2NodeName,
            End2Route,
            End2Cavity,
            End2MaterialCode,
            IncludeOnBOM,
            IncludeOnChart,
            WireTag,
            WireNote,
            WireLengthChangeType,
            WireLengthChangeValue,
            AssemblyItemNumber,
            MulticoreOption,
        }
        private enum Wire_Project_Profile_Options
        {
            Wire,
            Diameter,
            Color,
            Type,
            Connector_1,
            Port_1,
            Term_1,
            Seal_1,
            Lokation_1,
            Wire_connection,
            Term_2,
            Seal_2,
            Connector_2,
            Port_2,
            Lokation_2,
            Harness,
            Variant,
            Bundle,
            Kodenr_wire,
            Tag,
        }

        private enum Component_WCSPP_Profile_Options
        {
            Name,
            Part_no,
            Passive,
            Instruction,
            Variant,
            Bundle,
            Description,
            Lokation,
            EndText,
        }
        private enum Component_Profile_Options
        {
            NodeName,
            CavityName,
            WireName,
            SequenceNumber,
            ComponentTypeCode,
            CircuitOption,
            ServiceFunction,
            Route,
            PartNumber1,
            Quantity,
            CrossSectionalArea,
            PartNumber2,
            PartNumber3,
            SelectTerminal,
            Seal,
            Plugged,
            BlockNumber,
            TerminationMethod,
            MaterialCode,
            ComponentTypeCode2,
        }
        private enum Component_Project_Profile_Options
        {

            Type,
            Ref,
            Description,
            Location,
            Connector,
            SecLock,
            Harness,
            Variant,
            Bundle,
            Tag,
            System,
            Fuse_value,
            Color,
            N_pins,
        }

        //TODO: not yet implemented
        //private ProfileController profileController;

        public Dictionary<string, List<string>> defaultProfiles;
        public Dictionary<string, List<string>> userProfiles;

        public ProfileController()
        {
            defaultProfiles = new Dictionary<string, List<string>>();
            userProfiles = LoadProfiles();

            InitializeDefaultProfiles();
        }

        private void InitializeDefaultProfiles()
        {
            //Programatically load in each enum (if the enum changes you don't have to worry about this)
            var wireWCSPPProfile = Enum.GetValues(typeof(Wire_WCSPP_Profile_Options))
                                        .Cast<Wire_WCSPP_Profile_Options>()
                                        .Select(option => option.ToString())
                                        .ToList();

            var wireProfile = Enum.GetValues(typeof(Wire_Profile_Options))
                                  .Cast<Wire_Profile_Options>()
                                  .Select(option => option.ToString())
                                  .ToList();

            var wireProjectProfile = Enum.GetValues(typeof(Wire_Project_Profile_Options))
                                        .Cast<Wire_Project_Profile_Options>()
                                        .Select(option => option.ToString())
                                        .ToList();

            var componentWCSPPProfile = Enum.GetValues(typeof(Component_WCSPP_Profile_Options))
                                            .Cast<Component_WCSPP_Profile_Options>()
                                            .Select(option => option.ToString())
                                            .ToList();

            var componentProfile = Enum.GetValues(typeof(Component_Profile_Options))
                                      .Cast<Component_Profile_Options>()
                                      .Select(option => option.ToString())
                                      .ToList();

            var componentProjectProfile = Enum.GetValues(typeof(Component_Project_Profile_Options))
                                              .Cast<Component_Project_Profile_Options>()
                                              .Select(option => option.ToString())
                                              .ToList();

            defaultProfiles.Add("Wire_WCSPP_Profile", wireWCSPPProfile);
            defaultProfiles.Add("Wire_Profile", wireProfile);
            defaultProfiles.Add("Wire_Project_Profile", wireProjectProfile);
            defaultProfiles.Add("Component_WCSPP_Profile", componentWCSPPProfile);
            defaultProfiles.Add("Component_Profile", componentProfile);
            defaultProfiles.Add("Component_Project_Profile", componentProjectProfile);
        }

        public void SaveProfiles()
        {

            //TODO: this needs to go through Data_Access
        
        }

        public Dictionary<string, List<string>> LoadProfiles()
        {
            //TODO: fix this one as well
            
            return userProfiles;
        }
    }
}
