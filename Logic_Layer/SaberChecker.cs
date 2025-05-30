﻿using Logging;
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
    public Dictionary<string, string> CombinedFailures { get; private set; }

    private List<(DSI_Component, string)> FailedComponents;
    private List<(DSI_Wire, string)> FailedWires;
    


    public SaberChecker(Logger logger, List<DSI_Component> dsiComponents, List<DSI_Wire> dsiWires)
    {
        _logger = logger;
        components = dsiComponents;
        wires = dsiWires;

        FailedWires = new List<(DSI_Wire, string)>();
        FailedComponents = new List<(DSI_Component, string)>();
        CombinedFailures = new Dictionary<string, string>();


        TestResults = new List<bool>();

        PerformChecks();
        CombineFailures();
    }

    private void CombineFailures()
    {
        foreach (var (component, failure) in FailedComponents)
        {
            string nodeName = component.NodeName;
            if (CombinedFailures.ContainsKey(nodeName))
            {
                CombinedFailures[nodeName] += " - " + failure;
            }
            else
            {
                CombinedFailures[nodeName] = failure;
            }
        }

        foreach (var (wire, failure) in FailedWires)
        {
            string wireName = wire.WireName;
            if (CombinedFailures.ContainsKey(wireName))
            {
                CombinedFailures[wireName] += " - " + failure;
            }
            else
            {
                CombinedFailures[wireName] = failure;
            }
        }
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
        //TestResults.Add(DSI_15(wires));
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
                if (diameter > 35.0)
                {
                    testResult = false;
                    FailedWires.Add((wire, "DSI_01"));
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
                FailedComponents.Add((component, "DSI_02"));
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
                        FailedComponents.Add((component, "DSI_06"));
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
                        FailedComponents.Add((component, "DSI_07"));
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
                FailedComponents.Add((component, "DSI_08"));
            }
        }
        return testResult;
    }

    //DSI_12 Wire length must be 0 (loopback) or larger than 0 (all others)
    public bool DSI_12(List<DSI_Wire> wiresToCheck)
    {
        bool testResult = true;

        foreach (DSI_Wire wire in wiresToCheck)
        {
            // Find actual wire length (assuming it's stored in a property named WireLength)
            int wireLength;
            if (!int.TryParse(wire.WireTag, out wireLength))
            {
                continue; // Skip to the next wire
            }

            // Check if wire length is smaller than 0
            if (wireLength < 0)
            {
                // Set testResult to false
                testResult = false;

                // Add the wire to FailedWires
                FailedWires.Add((wire, "DSI_12"));
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
            // Find actual wire length (assuming it's stored in a property named WireLength)
            int wireLength;
            if (!int.TryParse(wire.WireTag, out wireLength))
            {
                // Log an error if WireLengthChangeValue cannot be parsed as an integer
                Console.WriteLine($"Error: Invalid wire length value for wire {wire.WireName}");
                continue; // Skip to the next wire
            }

            // Check if wire length is smaller than 100
            if (wireLength < 100)
            {
                // Log a warning message
                Console.WriteLine($"Warning: Wire length is less than 0 for wire {wire.WireName}");

                // Set testResult to false
                testResult = false;

                // Add the wire to FailedWires
                FailedWires.Add((wire, "DSI_12"));
            }
        }

        return testResult;
    }

    //DSI_15 Wire terminal must be present and have correct part number

    public bool DSI_15(List<DSI_Component> componentsToCheck)
    {
        bool testResult = true;

        foreach (DSI_Component wire in componentsToCheck)
        {
            //Look for TERMS
            //Look if partnumber is 7 long and filled in
        }

        return testResult;
    }

    //DSI_16 Connector must have valid part number

    public bool DSI_16(List<DSI_Wire> wiresToCheck)
    {
        bool testResult = true;

        foreach (DSI_Wire wire in wiresToCheck)
        {
            FailedWires.Add((wire, "DSI_16"));

            //and be 7 long 
            //if (wire.Code_no == "")
            //{
            //TODO: DSI-14 write warning message to log
            //  return false;
            //}
        }

        return testResult;
    }

    //DSI_19 Bundle must have identificiation label
    public bool DSI_19(List<DSI_Component> componentsToCheck)
    {
        bool testResult = false;
        if (componentsToCheck != null)
        {
            foreach (DSI_Component component in componentsToCheck)
            {
                if (component != null)
                {
                    //1 should have "LABEL" in partnumber1 may be Label5 or label4
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
    //Get the variant for a component, see if it is present in any of the composite details line (section 3)

    //DSI_21 issue drawing must be equal at least to one issue assembly in DSI file
    //section 1 bundle issue number must be equal to the highest issue number present in section 3 composites
    //Combine with DSI_22 and DSI_23

    //Take 1 issue lower and check whether or not section 3 aren't lower than their previous versions
    //Production folder first then proto

    //DSI_24 Bundle issue may not be lower than previous version

    //DSI_29 Branch length must be more than minimum
    //Section 4 branch lengths must be longer than 0

    //DSI_30 Bundle issue may not be deleted until a new drawing is released
    //Composite details in section #3 all need to be present in DSI20
}
