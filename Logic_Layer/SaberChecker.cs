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
        public bool DSI_01(List<Converted_Wire> wiresToCheck)
        {
            bool testResult = true;

            foreach(Converted_Wire wire in wiresToCheck)
            {
                if(int.Parse(wire.Diameter) >= 35) //add wire.property.marking(first digit) = 1
                {
                    testResult = false; //it should just give a warning!
                    //generate testReport
                }
            }

            return testResult;
        }


        //DSI_02 Flagnote or instruction assigned to bundle component must have a connector variant
        public bool DSI_02(List<Converted_Component> componentsToCheck)
        {
            bool testResult = true;

            foreach (Converted_Component component in componentsToCheck)
            {
                if (component.Variant == "")
                {
                    //TODO: DSI-02 write warning message to log
                    testResult = false;
                }
            }

            return testResult;
        }

        //DSI_03 Flagnote or instruction must have passive on last row
        public bool DSI_03(List<DSI_Component> componentsToCheck)
        {
            bool testResult = true;

            foreach(DSI_Component component in componentsToCheck)
            {
                if(component.ComponentTypeCode == "PASSIVE")
                {
                    //passives must not be empty?
                }
            }

            return testResult;
        }


        //DSI_04 Connector must have terminals
        public bool DSI_04(List<Converted_Component> componentsToCheck)
        {
            bool testResult = true;

            foreach (Converted_Component component in componentsToCheck)
            {
                //TODO: DSI - 04 write warning message to log
            }

            return testResult;
        }

        //DSI_06 Section 7 of DSI may only contain covers
        public bool DSI_06(List<DSI_Component> componentsToCheck)
        {
            bool testResult = true;

            foreach(DSI_Component component in componentsToCheck)
            {
                //TODO: check this statement against actual data
                if(component.ComponentTypeCode == "PASSIVE" && component.ComponentTypeCode2 == "*sleeve")
                {
                    //Should give warning in testReport
                    testResult = false;
                }
            }

            return testResult;
        }
        //DSI_07 Insulation class code must be correct
        public bool DSI_07(List<DSI_Component> componentsToCheck)
        {
            bool testResult = true;

            foreach(DSI_Component component in componentsToCheck)
            {
                //TODO: check this against actual data and UAT_SOW2_DSI.xlsx
                if(component.PartNumber1 == "Class" && component.ComponentTypeCode2 == "sleeve_*")
                {
                    testResult = true;
                }
            }

            return testResult;
        }

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
        public bool DSI_19(List<DSI_Component> componentsToCheck)
        {
            bool testResult = false;

            foreach(DSI_Component component in componentsToCheck)
            {
                //Should probably check this for each individual component and generate a report which do and do not pass this test
                if((component.ComponentTypeCode == "PASSIVE") && (component.PartNumber1 == "LABEL"))
                {
                    testResult = true;
                }
            }

            return testResult;
        }


        //DSI_20 Variants used on bundle components must be defined for a bundle
        //TODO: psuedo code does not make a whole lot of sense c# wise, but checking if variants are defined is definetely doable.


        //DSI_21 issue drawing must be equal at least to one issue assembly in DSI file
        //TODO: psuedo code does not make a whole lot of sense c# wise, not sure what is expected here

        //DSI_22 assembly must have an issue
        //TODO: what is an assembly and where are the issues defined?

        //DSI_23 Issue assembly may not be higher than drawing issue

        //DSI_24 Bundle issue may not be lower than previous version

        //DSI_29 Branch length must be more than minimum

        //DSI_30 Bundle issue may not be deleted until a new drawing is released
    }
}
