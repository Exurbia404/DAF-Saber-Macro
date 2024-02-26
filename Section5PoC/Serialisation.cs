using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class Serialisation
    {
        //TODO: this won't allow for 1780456-9 only 1780456, this has to be amended
        public void WriteToFile(List<WCSPP_Wire> wires, List<WCSPP_Component> components, string extractedBundles)
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string firstSet = GetFirstSetOfNumbers(extractedBundles);

            // Construct the new file name
            string newFileName = $"{firstSet}_generated_parts.txt";

            // Combine with the desired subdirectories and file name
            string filePath = Path.Combine(exeDirectory, "data", "output", newFileName);

            // Create or overwrite the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header line
                writer.WriteLine("Wire,Diam,Color,Type,Code_no,Length,Connector_1,Port_1,Term_1,Seal_1,Wire_connection,Term_2,Seal_2,Connector_2,Port_2,Variant,Bundle,Loc_1,Loc_2,");

                // Write each WCSPP_Wire object to the file
                foreach (var wcsppWire in wires)
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

            newFileName = $"{firstSet}_generated_WCSPP.txt";


            filePath = Path.Combine(exeDirectory, "data", "output", newFileName);

            // Create or overwrite the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header line
                writer.WriteLine("Name,Part_no,,Passive,Instruction,Variant,Bundle,Description,Lokation,,,,,,,,,,,,,,,,,,,,");

                // Write each WCSPP_Wire object to the file
                foreach (var wcsppComponent in components)
                {
                    // Format the line with object properties
                    string line = $"{wcsppComponent.Name},{wcsppComponent.Part_no},,{wcsppComponent.Passive},{wcsppComponent.Instruction},{wcsppComponent.Variant},{wcsppComponent.Bundle}," +
                                $"{wcsppComponent.Description},{wcsppComponent.Lokation},,,,,,,,,,,,,,,,,,,,{wcsppComponent.EndText} ";
                    // Write the formatted line to the file
                    writer.WriteLine(line);
                }
            }
        }

        private string GetFirstSetOfNumbers(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                // Handle empty or null strings
                return string.Empty;
            }

            // Split the string by spaces
            string[] sets = input.Split(' ');

            // Find the first set that contains only numbers
            foreach (string set in sets)
            {
                if (IsOnlyNumbers(set))
                {
                    return set;
                }
            }

            return string.Empty; // Return empty string if no valid set is found
        }

        // Helper function to check if a string consists of only numbers
        private bool IsOnlyNumbers(string input)
        {
            return input.All(char.IsDigit);
        }

    }
}
