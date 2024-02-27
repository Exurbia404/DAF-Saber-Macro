﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class WCSPP_Convertor
    {
        //WCSPP contains:
        //Wire, Diameter, Color, Type, Code_no, Length, Connector_1, Port_1, Term_1, Seal_1, Wire_connection, Term_2, Seal_2, Connector_2, Port_2, Variant, Bundle, Loc_1, Loc_2

        ExcelHandler wcsppExcelHandler;
        Serialisation serialisation;

        public WCSPP_Convertor() 
        {
            wcsppExcelHandler = new ExcelHandler();
            serialisation = new Serialisation();    
        }


        //TODO: this can probably be one function that takes in an argument to swtich between text and excel file since conversion is the same
        public void ConvertListToWCSPPTextFile(List<Wire> wiresToConvert, List<Component> componentsToConvert, List<Bundle> extractedBundles, string fileName)
        {
            serialisation.WriteToFile(ConvertWireToWCSPP(wiresToConvert, extractedBundles), ConvertComponentToWCSPP(componentsToConvert, extractedBundles), extractedBundles, fileName);
        }

        public void ConvertListToWCSPPExcelFile(List<Wire> wiresToConvert, List<Component> componentsToConvert, List<Bundle> extractedBundles) 
        {
            wcsppExcelHandler.CreateExcelSheet(ConvertWireToWCSPP(wiresToConvert, extractedBundles), ConvertComponentToWCSPP(componentsToConvert, extractedBundles));
        }

        private List<WCSPP_Wire> ConvertWireToWCSPP(List<Wire> wiresToConvert, List<Bundle> bundles)
        {
            List<WCSPP_Wire> convertedList = new List<WCSPP_Wire>();

            foreach (Wire wire in wiresToConvert)
            {
                WCSPP_Wire wCSPP_Wire = new WCSPP_Wire(wire.WireName, wire.CrossSectionalArea, wire.Color, wire.Material, wire.WireNote, wire.WireNote, wire.End1NodeName, wire.End1Cavity, "?", "?", "combination", "?", "?", "?", "?", "?", "?", "?", "?");
                wCSPP_Wire.Length = GetValueFromInputString(wire.WireNote, 0);
                wCSPP_Wire.Code_no = GetValueFromInputString(wire.WireNote, 1);
                wCSPP_Wire.Bundle = GetBundlesForVariant(bundles, wire.CircuitOption);

                //Stills needs to extract Term_1, Seal_1, Term_2, Seal_2, Connector_2, Port_2 info from connector itself

                convertedList.Add(wCSPP_Wire);
            }
            
            return convertedList;
        }

        private List<WCSPP_Component> ConvertComponentToWCSPP(List<Component> componentsToConvert, List<Bundle> bundles)
        {
            List<WCSPP_Component> convertedList = new List<WCSPP_Component>();

            foreach (Component component in componentsToConvert)
            {
                if (component.ComponentTypeCode == "CONNECTOR")
                {
                    // Add logic to extract and set Term_1, Seal_1, Term_2, Seal_2, Connector_2, Port_2 info from the component itself
                    WCSPP_Component wCSPP_Component = new WCSPP_Component
                    {
                        Name = component.NodeName,
                        Part_no = component.PartNumber2,
                        Passive = GetPassivesForComponent(componentsToConvert, component.NodeName, GetEndTextForComponent(componentsToConvert, component.NodeName)),
                        Instruction = GetInstructionForComponent(componentsToConvert, component.NodeName),
                        Variant = component.CircuitOption,
                        Bundle = GetBundlesForVariant(bundles, component.CircuitOption),
                        Description = "",
                        Lokation = "",
                        EndText = GetEndTextForComponent(componentsToConvert, component.NodeName)
    
                        // Set other properties here as needed
                    };

                convertedList.Add(wCSPP_Component);
                }
            }

            return convertedList.OrderBy(component => component.Name).ToList(); ;
        }

        static string GetBundlesForVariant(List<Bundle> bundles, string variant)
        {
            // Split the variant into individual variants
            string[] variantArray = variant.Split('/');

            // Filter bundles where any of the variants is present in the references
            IEnumerable<string> matchingVariants = bundles
                .Where(bundle => variantArray.Any(v => bundle.References.Contains(v)))
                .Select(bundle => bundle.VariantNumber);

            // Concatenate matching variants into a single string
            string result = string.Join(" ", matchingVariants);

            return result;
        }

        private string GetPassivesForComponent(List<Component> fullList, string componentName, string endText)
        {
            // Filter components based on ComponentName and ServiceFunction, and PartNumber2 contains numbers
            var filteredComponents = fullList
                .Where(component =>
                    component.NodeName == componentName &&
                    component.ComponentTypeCode == "PASSIVES" &&
                    StartsWithNumber(component.PartNumber2) &&
                    (component.PartNumber2.Length >= 7) &&
                    component.ComponentTypeCode2 == endText)
                .ToList();

            // Extract PartNumber2 and concatenate them with a space in between
            string result = string.Join(" ", filteredComponents.Select(component => component.PartNumber2));

            return result;
        }

        private string GetEndTextForComponent(List<Component> fullList, string componentName)
        {
            // Filter components based on ComponentName and ServiceFunction
            var filteredComponents = fullList
                .Where(component => component.NodeName == componentName &&
                                    component.ComponentTypeCode == "CONNECTOR")
                .ToList();

            // Extract PartNumber2 and concatenate them with a space in between
            string result = string.Join(" ", filteredComponents.Select(component => component.ComponentTypeCode2));

            return result;
        }

        private string GetInstructionForComponent(List<Component> fullList, string componentName)
        {
            //Include the component if:
            //  -NodeName matches a specified name.
            //  -ComponentTypeCode is "PASSIVES".
            //  - If PartNumber2 doesn't start with a number OR starts with a number:
            //    - Include if ComponentTypeCode2 is not "additional instructions".
            //    - Include if PartNumber2 is not equal to NodeName.
            //  - PartNumber2 contains letters.

            var filteredComponents = fullList
            .Where(component =>
                component.NodeName == componentName &&
                component.ComponentTypeCode == "PASSIVES" &&
                (
                    (!StartsWithNumber(component.PartNumber2)) ||
                    (StartsWithNumber(component.PartNumber2) && (IsAlphanumericEqual(component.PartNumber2, component.NodeName) || IsStringPresent(component.PartNumber2, component.NodeName))
                )) &&
                HasLetters(component.PartNumber2) &&
                component.ComponentTypeCode2 != "additional instructions")
            .ToList();

            // Extract PartNumber2 and concatenate them with a space in between
            string result = string.Join(" ", filteredComponents.Select(component => component.PartNumber2));

            return result;
        }

        private bool HasNumbers(string input)
        {
            return input.Any(char.IsDigit);
        }

        private bool HasLetters(string input)
        {
            return input.Any(char.IsLetter);
        }

        bool IsStringPresent(string str1, string str2)
        {
            // Implement this function to check if str1 is present in str2
            return str2.IndexOf(str1, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        bool IsAlphanumericEqual(string str1, string str2)
        {
            // Implement this function to check if two alphanumeric strings are equal
            return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
        }

        private bool StartsWithNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                // Handle empty or null strings
                return false;
            }

            char firstChar = input[0];
            return char.IsDigit(firstChar);
        }

        static string GetValueFromInputString(string input, int keyIndex)
        {
            string[] keyValuePairs = input.Split(',');

            int currentIndex = 0;

            foreach (string pair in keyValuePairs)
            {
                string[] keyValue = pair.Split('=');

                if (keyValue.Length == 2)
                {
                    if (currentIndex == keyIndex)
                    {
                        return keyValue[1].Trim();
                    }

                    currentIndex++;
                }
            }

            // If the specified index is out of range, you might want to handle this case accordingly
            return "Index out of range";
        }

    }
}
