using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class SaberChecker
    {
        //DSI_01 Battery PLUS cable must have a sleeve

        //DSI_02 Flagnote or instruction assigned to bundle component must have a connector variant
        public bool DSI_02(List<DSI_Component> listToCheck)
        {
            bool testResult = true;
            
            foreach(DSI_Component component in listToCheck)
            {
                if(component.PartNumber2== "")
                {
                    testResult = false;
                }
            }

            return testResult;
        }

        //DSI_03 Flagnote or instruction must have passive on last row
        
        //DSI_04 Connector must have terminals
        public bool DSI_04(List<DSI_Component> listToCheck)
        {
            bool testResult = true;

            foreach(DSI_Component component in listToCheck)
            {
                if(component.CavityName == "")
                {
                    testResult = false;
                }
            }

            return testResult;
        }

        //DSI_06 Section 7 of DSI may only contain covers

        //DSI_07 Insulation class code must be correct

        //DSI_08 Bundle component must be assigned to a variant
        public bool DSI_08(List<DSI_Component> listToCheck)
        {
            bool testResult = true;

            foreach (DSI_Component component in listToCheck)
            {
                if(component.CircuitOption == "")
                {
                    testResult = false;
                }
            }

            return testResult;
        }
        //DSI_12 Wire length must be 0 (loopback) or larger than 0 (all others)

        //DSI_14 wire length must be more than minimum (?)

        //DSI_15 Wire terminal must be present and have correct part number

        //DSI_16 Connector must have valid part number

        //DSI_19 Bundle must have identificiation label

        //DSI_20 Variants used on bundle components must be defined for a bundle

        //DSI_21 issue drawing must be equal at least to one issue assembly in DSI file

        //DSI_22 assembly must have an issue

        //DSI_23 Issue assembly may not be higher than drawing issue

        //DSI_24 Bundle issue may not be lower than previous version

        //DSI_29 Branch length must be more than minimum

        //DSI_30 Bundle issue may not be deleted until a new drawing is released
    }
}
