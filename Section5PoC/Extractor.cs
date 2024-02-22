using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class Extractor
    {
        
        public List<Wire> ExtractSection5FromTextFile(string filePath)
        {
            List<Wire> wireList = new List<Wire>();
            string sectionStart = "%Section 5";
            string sectionEnd = "%Section 6";
            bool isInSection5 = false;

            foreach (string line in File.ReadLines(filePath))
            {
                if (line.StartsWith(sectionEnd))
                {
                    // Exit the loop when reaching the end of Section 5
                    break;
                }

                if (isInSection5)
                {
                    // Process lines between Section 5 and Section 6
                    Wire wireObject = ExtractSection5ObjectFromString(line);
                    wireList.Add(wireObject);
                }

                if (line.StartsWith(sectionStart))
                {
                    // Start processing lines when entering Section 5
                    isInSection5 = true;
                }
            }

            return wireList;
        }


        private Wire  ExtractSection5ObjectFromString(string inputString)
        {
            string[] fields = inputString.Split(':');

            // Create a new Wire object and set its properties based on the fields
            Wire wireObject = new Wire
            {
                WireName = GetStringAtIndex(fields, 0),
                WireOption = GetStringAtIndex(fields, 1),
                WireType = GetStringAtIndex(fields, 2),
                Color = GetStringAtIndex(fields, 3),
                CrossSectionalArea = GetStringAtIndex(fields, 4),
                Material = GetStringAtIndex(fields, 5),
                UserModule = GetStringAtIndex(fields, 6),
                MulticoreName = GetStringAtIndex(fields, 7),
                End1NodeName = GetStringAtIndex(fields, 8),
                End1Route = GetStringAtIndex(fields, 9),
                End1Cavity = GetStringAtIndex(fields, 10),
                End1MaterialCode = GetStringAtIndex(fields, 11),
                End2NodeName = GetStringAtIndex(fields, 12),
                End2Route = GetStringAtIndex(fields, 13),
                End2Cavity = GetStringAtIndex(fields, 14),
                End2MaterialCode = GetStringAtIndex(fields, 15),
                IncludeOnBOM = GetStringAtIndex(fields, 16),
                IncludeOnChart = GetStringAtIndex(fields, 17),
                WireTag = GetStringAtIndex(fields, 18),
                WireNote = GetStringAtIndex(fields, 19),
                WireLengthChangeType = GetStringAtIndex(fields, 20),
                WireLengthChangeValue = GetStringAtIndex(fields, 21),
                AssemblyItemNumber = GetStringAtIndex(fields, 22),
                MulticoreOption = GetStringAtIndex(fields, 23),
            };

            return wireObject;
        }

        private static string GetStringAtIndex(string[] fields, int index)
        {
            return index < fields.Length ? fields[index].Trim() : "";
        }
    }
}
