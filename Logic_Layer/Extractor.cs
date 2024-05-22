using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    public class Extractor
    {
        public List<Converted_Component> Components { get; private set; }
        public List<Project_Component> Project_Components { get; private set; }
        public List<Converted_Wire> Wires { get; private set; }
        public List<Project_Wire> Project_Wires { get; private set; }

        public List<Bundle> Bundles { get; private set; }

        private Logger _logger;
        private List<List<string[]>> sections;
        private string filePath;
        private List<DSI_Component> dsi_Components;
        
        public Extractor(Logger logger)
        {
            _logger = logger;
        }

        public void ExtractBundleFromFilePath(string filepath)
        {
            filePath = filepath;

            //1 get the sections
            sections = RetrieveSectionsFromFile();

            //4 Get the bundles
            Bundles = GetBundlesFromSection(sections[2]);

            dsi_Components = GetDSI_ComponentsFromSection(sections[5]);

            //2 Get the Wires
            Wires = GetWiresFromSection(sections[4], Bundles);

            //3 Get the Components
            Components = ConvertComponents(dsi_Components, Bundles);
        }

        private List<Converted_Component> ConvertComponents(List<DSI_Component> componentsToConvert, List<Bundle> bundles)
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
                        Variant = GetVariantForComponent(component),
                        Bundle = GetBundlesForVariant(bundles, component.CircuitOption),
                        Description = "",
                        Location = "",
                        EndText = GetEndTextForComponent(componentsToConvert, component.NodeName)

                        // Set other properties here as needed
                    };

                    if(wCSPP_Component.Variant == "" || wCSPP_Component.Bundle == "")
                    {
                        wCSPP_Component.Bundle = GetModuleNumbersForComponent(component.BlockNumber)[0];
                        wCSPP_Component.Variant = GetVariantForModularizedComponent(wCSPP_Component.Bundle);
                    }

                    convertedList.Add(wCSPP_Component);
                }
            }

            return convertedList.OrderBy(component => component.Name).ToList(); ;
        }

        private List<string> GetModuleNumbersForComponent(string blockNumber)
        {
            var blockNumbers = blockNumber.Split('/');
            List<string> matchingModuleNumbers = new List<string>();

            foreach (string[] line in sections[9])
            {
                if (blockNumbers.Contains(line[0]))
                {
                    matchingModuleNumbers.Add(line[2]);
                }
            }

            return matchingModuleNumbers;
        }

        public List<DSI_Tube> ExtractDSITubes(string filePath)
        {
            string sectionStart = "%Section 4";
            string sectionEnd = "%Section 5";
            bool isInSection4 = false;

            List<DSI_Tube> foundTubes = new List<DSI_Tube>();

            foreach (string line in File.ReadLines(filePath))
            {
                if (line.StartsWith(sectionEnd))
                {
                    // Exit the loop when reaching the end of Section 3
                    break;
                }

                if (isInSection4)
                {
                    DSI_Tube tube = ExtractDSITubeFromString(line, filePath);
                    // Process lines between Section 3 and Section 4
                    if (tube != null)
                    {
                        foundTubes.Add(tube);
                    }
                }

                if (line.StartsWith(sectionStart))
                {
                    // Start processing lines when entering Section 3
                    isInSection4 = true;
                }
            }
            return foundTubes;
        }

        private DSI_Tube ExtractDSITubeFromString(string line, string filePath)
        {
            string[] parts = line.Split(':');
            string startNode = parts[0];
            string endNode = parts[3];
            string length = parts[6];
            string insulations = GetInsulationForBranch(startNode, endNode, filePath);

            return new DSI_Tube(length, insulations, startNode, endNode);
        }

        private string GetInsulationForBranch(string startNode, string endNode, string filePath)
        {
            string sectionStart = "%Section 7";
            string sectionEnd = "%Section 8";
            bool isInSection7 = false;

            string foundInsulations = "";

            foreach (string line in File.ReadLines(filePath))
            {
                if (line.StartsWith(sectionEnd))
                {
                    // Exit the loop when reaching the end of Section 3
                    break;
                }

                if (isInSection7)
                {
                    // Split the line by '::' to get individual parts
                    string[] parts = line.Split(':');

                    if ((parts[0] == startNode) && (parts[2] == endNode))
                    {
                        if (foundInsulations != "")
                        {
                            foundInsulations += " and ";
                        }

                        //Insulation is found in the fifth location
                        foundInsulations += parts[5];
                    }
                }

                if (line.StartsWith(sectionStart))
                {
                    // Start processing lines when entering Section 3
                    isInSection7 = true;
                }
            }
            return foundInsulations;
        }


        private string GetVariantForModularizedComponent(string bundleNumber)
        {
            foreach (string[] line in sections[2])
            {
                if (line[0] == bundleNumber)
                {
                    return line[16];
                }
            }
            return "";
        }

        private string GetVariantForComponent(DSI_Component component)
        {
            if (component.CircuitOption != "")
            {
                return component.CircuitOption;
            }
            else
            {
                return "";
            }
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

        private List<DSI_Component> GetDSI_ComponentsFromSection(List<string[]> section6)
        {
            List<DSI_Component> foundDSI_Components = new List<DSI_Component>();

            foreach (string[] line in section6)
            {
                // Create a new Component object and set its properties based on the fields
                foundDSI_Components.Add(new DSI_Component
                {
                    NodeName = line[0],
                    CavityName = line[1],
                    WireName = line[2],
                    SequenceNumber = line[3],
                    ComponentTypeCode = line[4],
                    CircuitOption = line[5],
                    ServiceFunction = line[6],
                    Route = line[7],
                    PartNumber1 = line[8],
                    Quantity = line[9],
                    CrossSectionalArea = line[10],
                    PartNumber2 = line[11],
                    PartNumber3 = line[12],
                    SelectTerminal = line[13],
                    Seal = line[14],
                    Plugged = line[15],
                    BlockNumber = line[21],
                    TerminationMethod = line[17],
                    MaterialCode = line[18],
                    ComponentTypeCode2 = line[29],
                });
            }

            return foundDSI_Components;
        }

        private string GetLastValueBetweenColons1(string[] input)
        {
            return input[30];
        }

        private static string GetStringAtIndex(string[] fields, int index)
        {
            return index < fields.Length ? fields[index].Trim() : "";
        }

        private List<Bundle> GetBundlesFromSection(List<string[]> section3)
        {
            List<Bundle> foundbundles = new List<Bundle>();

            foreach (string[] line in section3)
            {
                foundbundles.Add(ExtractBundleFromString(line));
            }

            return foundbundles;
        }

        private Bundle ExtractBundleFromString(string[] fields)
        {
            {
                Bundle bundle = new Bundle();

                // Assign fields to properties
                if (fields.Length >= 1) bundle.VariantNumber = fields[0].Trim();
                if (fields.Length >= 2) bundle.Issue = fields[1].Trim();
                if (fields.Length >= 3) bundle.Date = fields[2].Trim();

                // Check if there are references separated by '|'
                if (fields.Length >= 4)
                {
                    string[] referencesArray = fields[16].Split('|');
                    bundle.References = referencesArray.Select(reference => reference.Trim()).ToArray();
                }

                return bundle;
            }
        }

        private List<Converted_Wire> GetWiresFromSection(List<string[]> section5, List<Bundle> bundles)
        {
            List<Converted_Wire> foundWires = new List<Converted_Wire>();

            foreach (string[] line in section5)
            {
                Converted_Wire newWire = new Converted_Wire
                {
                    Wire = line[0],
                    Diameter = line[4],
                    Color = line[3],
                    Type = line[5],
                    Code_no = GetValueFromInputString(line[19], 1),
                    Length = GetValueFromInputString(line[19], 0),
                    Connector_1 = line[8],
                    Port_1 = line[10],
                    Term_1 = "?",
                    Seal_1 = "?",
                    Wire_connection = "connection",
                    Term_2 = "?",
                    Seal_2 = "?",
                    Connector_2 = line[12],
                    Port_2 = line[14],
                    Variant = GetWireVariants(line[1]),
                    Bundle = GetBundlesForVariant(Bundles, line[1]),
                    Loc_1 = "?",
                    Loc_2 = "?",
                    Temp_Class = GetValueFromInputString(line[19], 2)
                };

                //Set Connector 1 info
                newWire.Term_1 = FindTerminalCode(newWire.Connector_1, newWire.Port_1, newWire.Variant);
                newWire.Seal_1 = FindSealCode(newWire.Connector_1, newWire.Port_1, newWire.Variant);

                //Set Connector 2 info
                newWire.Term_2 = FindTerminalCode(newWire.Connector_2, newWire.Port_2, newWire.Variant);
                newWire.Seal_2 = FindSealCode(newWire.Connector_2, newWire.Port_2, newWire.Variant);

                if (line[6] != "") //Check if modularized
                {
                    newWire.Term_1 = Modularized_FindTerminalCode(newWire.Connector_1, newWire.Port_1, newWire);
                    newWire.Term_2 = Modularized_FindTerminalCode(newWire.Connector_2, newWire.Port_2, newWire);
                    newWire.Seal_1 = Modularized_FindSealCode(newWire.Connector_1, newWire.Port_1);
                    newWire.Seal_2 = Modularized_FindSealCode(newWire.Connector_2, newWire.Port_2);
                }

                if (newWire.Variant == "" || newWire.Bundle == "")
                {
                    string blockNumber = line[6];
                    newWire.Bundle = GetModuleNumbersForComponent(blockNumber)[0];
                    newWire.Variant = GetVariantForModularizedComponent(newWire.Bundle);
                }

                newWire.Wire_connection = GetWireConnection(newWire.Connector_1, newWire.Port_1, newWire.Connector_2, newWire.Port_2);

                foundWires.Add(newWire);

            }

            return foundWires;
        }

        private string Modularized_FindTerminalCode(string connector, string port_1, Converted_Wire wire)
        {
            // Filter components based on Connector, Port_1, and ComponentTypeCode
            var filteredComponents = dsi_Components
                .Where(component =>
                    component.NodeName == connector &&
                    component.CavityName == port_1 &&
                    component.ComponentTypeCode == "TERM")
                .ToList();

            if (filteredComponents.Count == 0)
            {
                return "";
            }
            //Sometimes in modularized bundles there are multiple options
            else if (filteredComponents.Count > 1)
            {
                foreach (DSI_Component component in filteredComponents)
                {
                    //Get the list of blocknumbers for each component
                    List<string> blockNumbers = GetModuleNumbersForComponent(component.BlockNumber);
                    foreach (string blockNumber in blockNumbers)
                    {
                        _logger.Log(blockNumber);
                        //See if the wire contains the component variant
                        if (wire.Bundle.Contains(blockNumber))
                        {
                            _logger.Log("");
                            return component.PartNumber2;
                        }
                    }
                    
                }
                throw new InvalidOperationException("Multiple matching components found. Expected only one.");
            }

            // Return the code of the found component
            return filteredComponents[0].PartNumber2;
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

        private string FindTerminalCode(string connector, string port_1, string wireVariants)
        {
            // Filter components based on Connector, Port_1, and ComponentTypeCode
            var filteredComponents = dsi_Components
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
                foreach (DSI_Component component in filteredComponents)
                {
                    _logger.Log(component.ToString());
                }
                throw new InvalidOperationException("Multiple matching components found. Expected only one.");
            }

            // Return the code of the found component
            return filteredComponents[0].PartNumber2;
        }

        private string FindSealCode(string connector, string port_1, string wireVariants)
        {
            // Filter components based on Connector, Port_1, and ComponentTypeCode
            var filteredComponents = dsi_Components
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
                foreach (DSI_Component component in filteredComponents)
                {
                    _logger.Log(component.ToString());
                }
                throw new InvalidOperationException("Multiple matching components found. Expected only one.");
            }

            // Return the code of the found component
            return filteredComponents[0].PartNumber2;
        }

        private string Modularized_FindSealCode(string connector, string port_1)
        {
            // Filter components based on Connector, Port_1, and ComponentTypeCode
            var filteredComponents = dsi_Components
                .Where(component =>
                    component.NodeName == connector &&
                    component.CavityName == port_1 &&
                    component.ComponentTypeCode == "SEAL")
                .ToList();

            if (filteredComponents.Count == 0)
            {
                return "";
            }
            else if (filteredComponents.Count > 1)
            {
                foreach (DSI_Component component in filteredComponents)
                {
                    _logger.Log(component.ToString());
                }
                throw new InvalidOperationException("Multiple matching components found. Expected only one.");
            }

            // Return the code of the found component
            return filteredComponents[0].PartNumber2;
        }

        private bool VariantIsPresent(string componentVariants, string wireVariants)
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

        private string GetValueFromInputString(string input, int keyIndex)
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

        private string GetBundlesForVariant(List<Bundle> bundles, string variant)
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

        private bool CheckIfModularized(string[] line)
        {
            return (line[59] == "yes");
        }

        public void ExtractRefsetFromFolderPath(string folderPath)
        {

        }

        private List<List<string[]>> RetrieveSectionsFromFile()
        {
            return new List<List<string[]>>
            {
                GetSectionUsingMarkers("Section 1", "%Section 2"),
                GetSectionUsingMarkers("%Section 2", "%Section 3"),
                GetSectionUsingMarkers("%Section 3", "%Section 4"),
                GetSectionUsingMarkers("%Section 4", "%Section 5"),
                GetSectionUsingMarkers("%Section 5", "%Section 6"),
                GetSectionUsingMarkers("%Section 6", "%Section 7"),
                GetSectionUsingMarkers("%Section 7", "%Section 8"),
                GetSectionUsingMarkers("%Section 8", "%Section 9"),
                GetSectionUsingMarkers("%Section 9", "%Section 10"),
                GetSectionUsingMarkers("%Section 10", "%Section 11"),
                GetSectionUsingMarkers("%Section 11", "%Section 12"),
                GetSectionUsingMarkers("%Section 12", "%Section 13"),
                GetSectionUsingMarkers("%Section 13", "%Section 14"),
                GetSectionUsingMarkers("%Section 14", "%Section 15"),
                GetSectionUsingMarkers("%Section 15", "%Section 16"),
                GetSectionUsingMarkers("%Section 16", "%Section 17"),
                GetSectionUsingMarkers("%Section 18", "%End of File Marker"),
            };
        }

        private List<string[]> GetSectionUsingMarkers(string startMarker, string endMarker)
        {
            List<string[]> section = new List<string[]>();
            bool isInDesiredSection = false;

            foreach (string line in File.ReadLines(filePath))
            {
                if (line.StartsWith(endMarker))
                {
                    // Exit the loop when reaching the end of Section 3
                    break;
                }

                if (isInDesiredSection)
                {
                    // Split the line by '::' to get individual parts
                    string[] parts = line.Split(':');
                    section.Add(parts);
                }

                if (line.StartsWith(startMarker))
                {
                    // Start processing lines when entering Section 3
                    isInDesiredSection = true;
                }
            }
            return section;
        }

        public List<Project_Wire> Project_ExtractWiresFromWireFile(string filePath)
        {
            List<Project_Wire> foundWires = new List<Project_Wire>();

            // Read lines from the file and skip the first line
            foreach (string line in File.ReadLines(filePath).Skip(1))
            {
                Project_Wire wire = Project_ExtractWireFromString(line);
                // Process lines between Section 3 and Section 4
                if (wire != null)
                {
                    foundWires.Add(wire);
                }
            }
            return foundWires;
        }

        private Project_Wire Project_ExtractWireFromString(string inputString)
        {
            string[] fields = inputString.Split(',');

            // Create a new Project_Wire object and set its properties based on the fields
            Project_Wire wireObject = new Project_Wire
            {
                Wire = GetStringAtIndex(fields, 0),
                Diameter = GetStringAtIndex(fields, 1),
                Color = GetStringAtIndex(fields, 2),
                Type = GetStringAtIndex(fields, 3),
                Connector_1 = GetStringAtIndex(fields, 4),
                Port_1 = GetStringAtIndex(fields, 5),
                Term_1 = GetStringAtIndex(fields, 6),
                Seal_1 = GetStringAtIndex(fields, 7),
                Location_1 = GetStringAtIndex(fields, 8),
                Wire_connection = GetStringAtIndex(fields, 9),
                Term_2 = GetStringAtIndex(fields, 10),
                Seal_2 = GetStringAtIndex(fields, 11),
                Connector_2 = GetStringAtIndex(fields, 12),
                Port_2 = GetStringAtIndex(fields, 13),
                Location_2 = GetStringAtIndex(fields, 14),
                Harness = GetStringAtIndex(fields, 15),
                Variant = GetStringAtIndex(fields, 16),
                Bundle = GetStringAtIndex(fields, 17),
                CodeNumber_Wire = GetStringAtIndex(fields, 18),
                Tag = GetStringAtIndex(fields, 19),
            };

            return wireObject;

        }

        public List<Project_Component> Project_ExtractComponentFromComponentFile(string filePath)
        {
            List<Project_Component> foundComponents = new List<Project_Component>();

            // Read lines from the file and skip the first line
            foreach (string line in File.ReadLines(filePath).Skip(1))
            {
                Project_Component component = Project_ExtractComponentFromString(line);
                // Process lines between Section 3 and Section 4
                if (component != null)
                {
                    foundComponents.Add(component);
                }
            }
            return foundComponents;
        }

        private Project_Component Project_ExtractComponentFromString(string inputString)
        {
            string[] fields = inputString.Split(',');

            // Create a new Project_Component object and set its properties based on the fields
            Project_Component componentObject = new Project_Component
            {
                Type = GetStringAtIndex(fields, 0),
                Ref = GetStringAtIndex(fields, 1),
                Description = GetStringAtIndex(fields, 2),
                Location = GetStringAtIndex(fields, 3),
                Connector = GetStringAtIndex(fields, 4),
                SecLock = GetStringAtIndex(fields, 5),
                Harness = GetStringAtIndex(fields, 6),
                Variant = GetStringAtIndex(fields, 7),
                Bundle = GetStringAtIndex(fields, 8),
                Tag = GetStringAtIndex(fields, 9),
                System = GetStringAtIndex(fields, 10),
                Fuse_value = GetStringAtIndex(fields, 11),
                Color = GetStringAtIndex(fields, 12),
                N_pins = GetStringAtIndex(fields, 13),
            };

            return componentObject;
        }
    }
}
