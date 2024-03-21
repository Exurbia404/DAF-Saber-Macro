using Logic;

namespace Data_Access
{
    public class FileHandler
    {
        public void WriteToFile(List<WCSPP_Wire> wires, List<WCSPP_Component> components, List<Bundle> extractedBundles, string fileName)
        {
            fileName = fileName.Replace("_DSI", "");

            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Construct the new file name
            string newFileName = $"{fileName}_generated_WCSPP.txt";

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

            newFileName = $"{fileName}_generated_parts.txt";


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
                                $"{wcsppComponent.Description},{wcsppComponent.Lokation},,,,,,,,,,,,,,,,,,,,{wcsppComponent.EndText}";
                    // Write the formatted line to the file
                    writer.WriteLine(line);
                }
            }
        }

        public void LoadFile()
        {

        }

        public void LoadFilesFromFolder()
        {

        }
    }
}
