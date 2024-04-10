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
        //public bool DSI_02(List<Converted_Component> componentsToCheck)
        //{
        //    bool testResult = true;
            
        //    foreach(Converted_Component component in componentsToCheck)
        //    {
        //        if(component.Variant == "")
        //        {
        //            //TODO: DSI-02 write warning message to log
        //            testResult = false;
        //        }
        //    }

        //    return testResult;
        //}

        //DSI_03 Flagnote or instruction must have passive on last row
        
        //DSI_04 Connector must have terminals
        //public bool DSI_04(List<Converted_Component> componentsToCheck)
        //{
        //    bool testResult = true;

        //    foreach(Converted_Component component in componentsToCheck)
        //    {
        //        if(component.CavityName == "")
        //        {
        //            TODO: DSI-04 write warning message to log
        //            testResult = false;
        //        }
        //    }

        //    return testResult;
        //}

        //DSI_06 Section 7 of DSI may only contain covers

        //DSI_07 Insulation class code must be correct


        //DSI_08 Bundle component must be assigned to a variant
        public bool DSI_08(List<Converted_Component> componentsToCheck)
        {
            bool testResult = true;

            foreach (Converted_Component component in componentsToCheck)
            {
                if(component.Variant == "")
                {
                    //TODO: DSI-08 write warning message to log
                    testResult = false;
                }
            }

            return testResult;
        }
        //DSI_12 Wire length must be 0 (loopback) or larger than 0 (all others)

        public bool DSI_12(List<Converted_Wire> wiresToCheck)
        {
            bool testResult = true;

            foreach(Converted_Wire wire in wiresToCheck)
            {
                //If length is smaller than 0 false
                if (int.Parse(wire.Length) < 0)
                {
                    //TODO: DSI-12 write warning message to log
                    return false;
                }
            }

            return testResult;
        }

        //DSI_14 wire length must be more than minimum (?)

        public bool DSI_14(List<Converted_Wire> wiresToCheck)
        {
            bool testResult = true;

            foreach (Converted_Wire wire in wiresToCheck)
            {
                //If length is smaller than 100 false
                if (int.Parse(wire.Length) < 100)
                {
                    //TODO: DSI-14 write warning message to log
                    return false;
                }
            }

            return testResult;
        }

        //DSI_15 Wire terminal must be present and have correct part number

        public bool DSI_15(List<Converted_Wire> wiresToCheck)
        {
            bool testResult = true;

            foreach (Converted_Wire wire in wiresToCheck)
            {
                //If length is smaller than 100 false
                if (wire.Term_1 == "" || wire.Term_2 == "")
                {
                    //TODO: DSI-15 write warning message to log
                    return false;
                }
            }

            return testResult;
        }

        //DSI_16 Connector must have valid part number

        public bool DSI_16(List<Converted_Wire> wiresToCheck)
        {
            bool testResult = true;

            foreach (Converted_Wire wire in wiresToCheck)
            {
                if (wire.Code_no == "")
                {
                    //TODO: DSI-14 write warning message to log
                    return false;
                }
            }

            return testResult;
        }

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
