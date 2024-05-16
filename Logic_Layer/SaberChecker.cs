using Logging;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logic;

public class SaberChecker
{
    private Logger _logger;

    private List<DSI_Component> components;
    private List<DSI_Wire> wires;

    public List<bool> TestResults { get; private set; }
    public Dictionary<DSI_Component, string> FailedComponents { get; private set; }
    public Dictionary<DSI_Wire, string> FailedWires { get; private set; }

    public SaberChecker(Logger logger, List<DSI_Component> dsiComponents, List<DSI_Wire> dsiWires)
    {
        _logger = logger;
        components = dsiComponents;
        wires = dsiWires;

        TestResults = new List<bool>();

        PerformChecks();
    }


    private void PerformChecks()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); // Start the stopwatch

        TestResults.Add(DSI_01(wires));
        TestResults.Add(DSI_02(components));
        TestResults.Add(DSI_03(components));
        TestResults.Add(DSI_04(components));
        TestResults.Add(DSI_06(components));
        TestResults.Add(DSI_07(components));
        TestResults.Add(DSI_08(components));
        TestResults.Add(DSI_12(wires));
        TestResults.Add(DSI_14(wires));
        TestResults.Add(DSI_15(wires));
        TestResults.Add(DSI_16(wires));
        TestResults.Add(DSI_19(components));

        stopwatch.Stop(); // Stop the stopwatch
        TimeSpan elapsed = stopwatch.Elapsed; // Get the elapsed time

        _logger.Log($"Performed checks in {elapsed.TotalMilliseconds} milliseconds");
    }

    //DSI_01 Battery PLUS cable must have a sleeve
    public bool DSI_01(List<DSI_Wire> wiresToCheck)
    {
        bool testResult = true;

        foreach (DSI_Wire wire in wiresToCheck)
        {
            double diameter;
            if (double.TryParse(wire.CrossSectionalArea, out diameter)) // Parsing as double
            {
                if (diameter < 35.0)
                {
                    testResult = false;
                    FailedWires.Add(wire, "DSI_01");
                }
            }
        }
        return testResult;
    }


    //DSI_02 Flagnote or instruction assigned to bundle component must have a connector variant
    public bool DSI_02(List<DSI_Component> componentsToCheck)
    {
        bool testResult = true;

        foreach (DSI_Component component in componentsToCheck)
        {
            if (component.CircuitOption == "")
            {
                //TODO: DSI-02 write warning message to log
                testResult = false;
                FailedComponents.Add(component, "DSI_02");
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
    public bool DSI_04(List<DSI_Component> componentsToCheck)
    {
        bool testResult = true;

        foreach (DSI_Component component in componentsToCheck)
        {
            
        }

        return testResult;
    }

    //DSI_06 Section 7 of DSI may only contain covers
    public bool DSI_06(List<DSI_Component> componentsToCheck)
    {
        bool testResult = true;

        if(componentsToCheck != null)
        {
            foreach (DSI_Component component in componentsToCheck)
            {
                if (component != null)
                {
                    //TODO: check this statement against actual data
                    if (component.ComponentTypeCode == "PASSIVE" && component.ComponentTypeCode2 == "*sleeve")
                    {
                        //Should give warning in testReport
                        testResult = false;
                        FailedComponents.Add(component, "DSI_06");
                    }
                }

            }
        }

        

        return testResult;
    }
    //DSI_07 Insulation class code must be correct
    public bool DSI_07(List<DSI_Component> componentsToCheck)
    {
        bool testResult = true;

        if(componentsToCheck != null)
        {
            foreach (DSI_Component component in componentsToCheck)
            {
                if (component != null)
                {
                    //TODO: check this against actual data and UAT_SOW2_DSI.xlsx
                    if (component.PartNumber1 == "Class" && component.ComponentTypeCode2 == "sleeve_*")
                    {
                        testResult = true;
                        FailedComponents.Add(component, "DSI_07");
                    }
                }
            }
        }

        return testResult;
    }

    //DSI_08 Bundle component must be assigned to a variant
    public bool DSI_08(List<DSI_Component> componentsToCheck)
    {
        bool testResult = true;

        foreach (DSI_Component component in componentsToCheck)
        {
            if(component.CircuitOption == "")
            {
                //TODO: DSI-08 write warning message to log
                testResult = false;
                FailedComponents.Add(component, "DSI_08");
            }
        }

        return testResult;
    }
    //DSI_12 Wire length must be 0 (loopback) or larger than 0 (all others)

    public bool DSI_12(List<DSI_Wire> wiresToCheck)
    {
        bool testResult = true;

        foreach(DSI_Wire wire in wiresToCheck)
        {
            //TODO: need to find actual wire Length
            //If length is smaller than 0 false
            if (int.Parse(wire.WireTag) < 0)
            {
                //TODO: DSI-12 write warning message to log

                testResult = false;
                FailedWires.Add(wire, "DSI_12");                
            }
        }

        return testResult;
    }

    //DSI_14 wire length must be more than minimum (?)

    public bool DSI_14(List<DSI_Wire> wiresToCheck)
    {
        bool testResult = true;

        foreach (DSI_Wire wire in wiresToCheck)
        {
            //If length is smaller than 100 false
            if (int.Parse(wire.WireTag) < 100)
            {
                //TODO: DSI-14 write warning message to log
                testResult = false;
                FailedWires.Add(wire, "DSI_14");
            }
        }

        return testResult;
    }

    //These checks are killing me!

    //DSI_15 Wire terminal must be present and have correct part number

    public bool DSI_15(List<DSI_Wire> wiresToCheck)
    {
        bool testResult = true;

        foreach (DSI_Wire wire in wiresToCheck)
        {
            //If length is smaller than 100 false
            //if (wire. == "" || wire.Term_2 == "")
            //{
                //TODO: DSI-15 write warning message to log
            //    return false;
            //}
        }

        return testResult;
    }

    //DSI_16 Connector must have valid part number

    public bool DSI_16(List<DSI_Wire> wiresToCheck)
    {
        bool testResult = true;

        foreach (DSI_Wire wire in wiresToCheck)
        {
            //if (wire.Code_no == "")
            //{
                //TODO: DSI-14 write warning message to log
              //  return false;
            //}
        }

        return testResult;
    }

    //DSI_19 Bundle must have identificiation label
    //TODO: see if DSI_19 is fixable
    public bool DSI_19(List<DSI_Component> componentsToCheck)
    {
        bool testResult = false;
        if (componentsToCheck != null)
        {
            foreach (DSI_Component component in componentsToCheck)
            {
                if (component != null)
                {
                    //Should probably check this for each individual component and generate a report which do and do not pass this test
                    if ((component.ComponentTypeCode == "PASSIVE") && (component.PartNumber1 == "LABEL") && component != null)
                    {
                        testResult = true;
                    }
                }
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
    //TODO: psuedo code does not make sense

    //DSI_24 Bundle issue may not be lower than previous version
    //TODO: cannot be done in this program

    //DSI_29 Branch length must be more than minimum
    //TODO: is already checked by checking minimum length?

    //DSI_30 Bundle issue may not be deleted until a new drawing is released
    //TODO: also does not make a whole lot of sense for this program
}
