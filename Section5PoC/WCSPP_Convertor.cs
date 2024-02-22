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

        public WCSPP_Convertor() 
        {
            wcsppExcelHandler = new ExcelHandler();
        }

        public void ConvertListToWCSPPTextFile(List<Wire> listToConvert)
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;



            // Get the current date and time in a format suitable for including in a file name
            string dateTimeString = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Combine with the desired subdirectories and file name
            string filePath = Path.Combine(exeDirectory, "data", "output", $"output_{dateTimeString}.txt");

            List<WCSPP_Wire> convertedList = ConvertWireToWCSPP(listToConvert);

            // Create or overwrite the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header line
                writer.WriteLine("Wire,Diam,Color,Type,Code_no,Length,Connector_1,Port_1,Term_1,Seal_1,Wire_connection,Term_2,Seal_2,Connector_2,Port_2,Variant,Bundle,Loc_1,Loc_2,");

                // Write each WCSPP_Wire object to the file
                foreach (var wcsppWire in convertedList)
                {
                    // Format the line with object properties
                    string line = $"{wcsppWire.Wire},{wcsppWire.Diameter},{wcsppWire.Color},{wcsppWire.Type},{wcsppWire.Code_no},{wcsppWire.Length}," +
                                  $"{wcsppWire.Connector_1},{wcsppWire.Port_1},{wcsppWire.Term_1},{wcsppWire.Seal_1},{wcsppWire.Wire_connection}," +
                                  $"{wcsppWire.Term_2},{wcsppWire.Seal_2},{wcsppWire.Connector_2},{wcsppWire.Port_2},{wcsppWire.Variant},{wcsppWire.Bundle}," +
                                  $"{wcsppWire.Loc_1},{wcsppWire.Loc_2},";

                    // Write the formatted line to the file
                    writer.WriteLine(line);
                }
            }
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
                    Passive = "?",
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
