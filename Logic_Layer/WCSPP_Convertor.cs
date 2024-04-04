using Data_Interfaces;
using UI_Interfaces;

namespace Logic
{
    public class WCSPP_Convertor
    {
        //WCSPP contains:
        //Wire, Diameter, Color, Type, Code_no, Length, Connector_1, Port_1, Term_1, Seal_1, Wire_connection, Term_2, Seal_2, Connector_2, Port_2, Variant, Bundle, Loc_1, Loc_2

        //Can inject this with in an interface
        private iExcelExporter excelExporter;
        private iFileHandler fileHandler;
        //private FileHandler serialisation;

        private static List<DSI_Wire> wiresToConvert;
        private static List<DSI_Component> componentsToConvert;

        public WCSPP_Convertor(List<DSI_Wire> wires, List<DSI_Component> components, iExcelExporter _excelExporter, iFileHandler _fileHandler) 
        {
            excelExporter = _excelExporter;
            fileHandler = _fileHandler;

            wiresToConvert = wires;
            componentsToConvert = components;
        }

        //TODO: this can probably be one function that takes in an argument to swtich between text and excel file since conversion is the same
        public void ConvertListToWCSPPTextFile(List<DSI_Wire> wiresToConvert, List<DSI_Component> componentsToConvert, List<Bundle> extractedBundles, string fileName, string filePath)
        {
            List<Data_Interfaces.iConverted_Wire> wires = ConvertWireToWCSPP(wiresToConvert, extractedBundles).Cast<Data_Interfaces.iConverted_Wire>().ToList();
            List<Data_Interfaces.iConverted_Component> components = ConvertComponentToWCSPP(componentsToConvert, extractedBundles).Cast<Data_Interfaces.iConverted_Component>().ToList();
            List<Data_Interfaces.iBundle> bundles = extractedBundles.Cast<Data_Interfaces.iBundle>().ToList();

            fileHandler.WriteToFile(wires, components, bundles, fileName, filePath);
        }

        public void ConvertListToWCSPPExcelFile(List<DSI_Wire> wiresToConvert, List<DSI_Component> componentsToConvert, List<Bundle> extractedBundles) 
        {
            List<UI_Interfaces.iConverted_Wire> wires = ConvertWireToWCSPP(wiresToConvert, extractedBundles).Cast < UI_Interfaces.iConverted_Wire > ().ToList();
            List<UI_Interfaces.iConverted_Component> components = ConvertComponentToWCSPP(componentsToConvert, extractedBundles).Cast<UI_Interfaces.iConverted_Component>().ToList();
            
            excelExporter.CreateExcelSheet(wires, components);
        }

        private List<Converted_Wire> ConvertWireToWCSPP(List<DSI_Wire> wiresToConvert, List<Bundle> bundles)
        {
            List<Converted_Wire> convertedList = new List<Converted_Wire>();

            foreach (DSI_Wire wire in wiresToConvert)
            {
                Converted_Wire wCSPP_Wire = new Converted_Wire(wire.WireName, wire.CrossSectionalArea, wire.Color, wire.Material, wire.WireNote, wire.WireNote, wire.End1NodeName, wire.End1Cavity, "?", "?", "combination", "?", "?", wire.End2NodeName, wire.End2Cavity, "?", "?", "?", "?");
                
                wCSPP_Wire.Length = GetValueFromInputString(wire.WireNote, 0);
                wCSPP_Wire.Code_no = GetValueFromInputString(wire.WireNote, 1);
                
                wCSPP_Wire.Bundle = GetBundlesForVariant(bundles, wire.WireOption);
                
                wCSPP_Wire.Connector_2 = wire.End2NodeName;
                wCSPP_Wire.Variant = GetWireVariants(wire.WireOption);

                //Set Connector 1 info
                wCSPP_Wire.Term_1 = FindTerminalCode(wCSPP_Wire.Connector_1, wCSPP_Wire.Port_1, wCSPP_Wire.Variant);
                wCSPP_Wire.Seal_1 = FindSealCode(wCSPP_Wire.Connector_1, wCSPP_Wire.Port_1, wCSPP_Wire.Variant);


                //Set Connector 2 info
                wCSPP_Wire.Term_2 = FindTerminalCode(wCSPP_Wire.Connector_2, wCSPP_Wire.Port_2, wCSPP_Wire.Variant);
                wCSPP_Wire.Seal_2 = FindSealCode(wCSPP_Wire.Connector_2, wCSPP_Wire.Port_2, wCSPP_Wire.Variant);

                //Stills needs to extract Term_1, Seal_1, Term_2, Seal_2, Connector_2, Port_2 info from connector itself

                
                wCSPP_Wire.Wire_connection = GetWireConnection(wCSPP_Wire.Connector_1, wCSPP_Wire.Port_1, wCSPP_Wire.Connector_2, wCSPP_Wire.Port_2);

                convertedList.Add(wCSPP_Wire);
            }
            
            return convertedList;
        }

        private string GetWireConnection(string connector_1, string port_1, string connector_2, string port_2)
        {
            // Combine the info into the specified format
            string result = $"{connector_1}{(string.IsNullOrEmpty(port_1) ? "" : $":{port_1}")} to {connector_2}{(string.IsNullOrEmpty(port_2) ? "" : $":{port_2}")}";

            return result;
        }

        private string GetWireVariants(string variantString)
        {
            // Replace '/' with a space

            return variantString.Replace('/', ' '); ;
        }

        private static string FindTerminalCode(string connector, string port_1, string wireVariants)
        {
            // Filter components based on Connector, Port_1, and ComponentTypeCode
            var filteredComponents = componentsToConvert
                .Where(component =>
                    component.NodeName == connector &&
                    component.CavityName == port_1 &&
                    component.ComponentTypeCode == "TERM" &&
                    VariantIsPresent(component.CircuitOption, wireVariants))
                .ToList();

            if (filteredComponents.Count == 0)
            {
                return "";
            }
            else if (filteredComponents.Count > 1)
            {
                throw new InvalidOperationException("Multiple matching components found. Expected only one.");
            }

            // Return the code of the found component
            return filteredComponents[0].PartNumber2;
        }

        private static string FindSealCode(string connector, string port_1, string wireVariants)
        {
            // Filter components based on Connector, Port_1, and ComponentTypeCode
            var filteredComponents = componentsToConvert
                .Where(component =>
                    component.NodeName == connector &&
                    component.CavityName == port_1 &&
                    component.ComponentTypeCode == "SEAL" &&
                    VariantIsPresent(component.CircuitOption, wireVariants))
                .ToList();

            if (filteredComponents.Count == 0)
            {
                return "";
            }
            else if (filteredComponents.Count > 1)
            {
                throw new InvalidOperationException("Multiple matching components found. Expected only one.");
            }

            // Return the code of the found component
            return filteredComponents[0].PartNumber2;
        }

        private static bool VariantIsPresent(string componentVariants, string wireVariants)
        {
            // Split wireVariants into an array
            string[] wireVariantArray = wireVariants.Split(' ');

            // Split componentVariants into an array using '/'
            string[] componentVariantArray = componentVariants.Split('/');

            // Check if any wire variant is present in component variants
            foreach (string wireVariant in wireVariantArray)
            {
                if (componentVariantArray.Contains(wireVariant))
                {
                    return true;
                }
            }

            return false;
        }

        private List<Converted_Component> ConvertComponentToWCSPP(List<DSI_Component> componentsToConvert, List<Bundle> bundles)
        {
            List<Converted_Component> convertedList = new List<Converted_Component>();

            foreach (DSI_Component component in componentsToConvert)
            {
                if (component.ComponentTypeCode == "CONNECTOR")
                {
                    // Add logic to extract and set Term_1, Seal_1, Term_2, Seal_2, Connector_2, Port_2 info from the component itself
                    Converted_Component wCSPP_Component = new Converted_Component
                    {
                        Name = component.NodeName,
                        Part_no = component.PartNumber2,
                        Passive = GetPassivesForComponent(componentsToConvert, component.NodeName, GetEndTextForComponent(componentsToConvert, component.NodeName)),
                        Instruction = GetInstructionForComponent(componentsToConvert, component.NodeName),
                        Variant = component.CircuitOption,
                        Bundle = GetBundlesForVariant(bundles, component.CircuitOption),
                        Description = "",
                        Location = "",
                        EndText = GetEndTextForComponent(componentsToConvert, component.NodeName)
    
                        // Set other properties here as needed
                    };

                convertedList.Add(wCSPP_Component);
                }
            }

            return convertedList.OrderBy(component => component.Name).ToList(); ;
        }

        private static string GetBundlesForVariant(List<Bundle> bundles, string variant)
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

        private string GetPassivesForComponent(List<DSI_Component> fullList, string componentName, string endText)
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

        private string GetEndTextForComponent(List<DSI_Component> fullList, string componentName)
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

        private string GetInstructionForComponent(List<DSI_Component> fullList, string componentName)
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

        private bool IsStringPresent(string str1, string str2)
        {
            // Implement this function to check if str1 is present in str2
            return str2.IndexOf(str1, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool IsAlphanumericEqual(string str1, string str2)
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

        private static string GetValueFromInputString(string input, int keyIndex)
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
