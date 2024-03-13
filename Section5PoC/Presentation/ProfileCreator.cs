using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Section5PoC.Presentation
{
    public partial class ProfileCreator : Form
    {
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

        public ProfileCreator()
        {
            InitializeComponent();
        }

        private void ProfileCreator_Load(object sender, EventArgs e)
        {

        }
    }
}
