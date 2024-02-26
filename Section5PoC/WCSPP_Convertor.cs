using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public void ConvertListToWCSPPTextFile(List<Wire> wiresToConvert, List<Component> componentsToConvert)
        {
            serialisation.WriteToFile(ConvertWireToWCSPP(wiresToConvert), ConvertComponentToWCSPP(componentsToConvert));
        }

        public void ConvertListToWCSPPExcelFile(List<Wire> wiresToConvert, List<Component> componentsToConvert) 
        {
            wcsppExcelHandler.CreateExcelSheet(ConvertWireToWCSPP(wiresToConvert), ConvertComponentToWCSPP(componentsToConvert));
        }

        private List<WCSPP_Wire> ConvertWireToWCSPP(List<Wire> wiresToConvert)
        {
            List<WCSPP_Wire> convertedList = new List<WCSPP_Wire>();

            foreach (Wire wire in wiresToConvert)
            {
                WCSPP_Wire wCSPP_Wire = new WCSPP_Wire(wire.WireName, wire.CrossSectionalArea, wire.Color, wire.Material, wire.WireNote, wire.WireNote, wire.End1NodeName, wire.End1Cavity, "?", "?", "combination", "?", "?", "?", "?", "?", "?", "?", "?");
                wCSPP_Wire.Length = GetValueFromInputString(wire.WireNote, 0);
                wCSPP_Wire.Code_no = GetValueFromInputString(wire.WireNote, 1);

                //Stills needs to extract Term_1, Seal_1, Term_2, Seal_2, Connector_2, Port_2 info from connector itself

                convertedList.Add(wCSPP_Wire);
            }
            
            return convertedList;
        }

        private List<WCSPP_Component> ConvertComponentToWCSPP(List<Component> componentsToConvert)
        {
            List<WCSPP_Component> convertedList = new List<WCSPP_Component>();

            foreach (Component component in componentsToConvert)
            {
                WCSPP_Component wCSPP_Component = new WCSPP_Component
                {
                    Name = component.NodeName,
                    Part_no = component.PartNumber2,
                    Passive = GetPassivesForComponent(componentsToConvert, component.NodeName),
                    Instruction = "?",
                    Variant = component.CircuitOption,
                    Bundle = "?",
                    Description = "?",
                    Lokation = "?",


                    // Set other properties here as needed
                };

                // Add logic to extract and set Term_1, Seal_1, Term_2, Seal_2, Connector_2, Port_2 info from the component itself

                convertedList.Add(wCSPP_Component);
            }

            return convertedList;
        }

        //CircuitOption describes whether or not it's passive or not
        private string GetPassivesForComponent(List<Component> fullList, string componentName)
        {
            // Filter components based on ComponentName and CircuitOption
            var filteredComponents = fullList
                .Where(component => component.NodeName == componentName && component.ComponentTypeCode == "PASSIVES")
                .ToList();

            // Extract PartNumber2 and concatenate them with a space in between
            string result = string.Join(" ", filteredComponents.Select(component => component.PartNumber2));

            return result;
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
