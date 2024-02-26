using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class Wire
    {
        public string WireName { get; set; } // 30 characters
        public string WireOption { get; set; } // 200 characters
        public string WireType { get; set; } // 30 characters
        public string Color { get; set; } // 30 characters
        public string CrossSectionalArea { get; set; } // Number
        public string Material { get; set; } // 30 characters
        public string UserModule { get; set; } // 30 characters
        public string MulticoreName { get; set; } // 30 characters
        public string End1NodeName { get; set; } // 10 characters
        public string End1Route { get; set; } // 10 characters
        public string End1Cavity { get; set; } // 10 characters
        public string End1MaterialCode { get; set; } // 4 characters
        public string End2NodeName { get; set; } // 10 characters
        public string End2Route { get; set; } // 10 characters
        public string End2Cavity { get; set; } // 10 characters
        public string End2MaterialCode { get; set; } // 4 characters
        public string IncludeOnBOM { get; set; } // Logical
        public string IncludeOnChart { get; set; } // Logical
        public string WireTag { get; set; } // 30 characters
        public string WireNote { get; set; } // 100 characters
        public string WireLengthChangeType { get; set; } // Specific Value
        public string WireLengthChangeValue { get; set; } // Number
        public string AssemblyItemNumber { get; set; } // Number
        public string MulticoreOption { get; set; } // 30 characters

        public Wire(string wireName, string wireOption, string wireType, string color,
                string crossSectionalArea, string material, string userModule,
                string multicoreName, string end1NodeName, string end1Route,
                string end1Cavity, string end1MaterialCode, string end2NodeName,
                string end2Route, string end2Cavity, string end2MaterialCode,
                string includeOnBOM, string includeOnChart, string wireTag,
                string wireNote, string wireLengthChangeType, string wireLengthChangeValue,
                string assemblyItemNumber, string multicoreOption)
        {
            WireName = wireName;
            WireOption = wireOption;
            WireType = wireType;
            Color = color;
            CrossSectionalArea = crossSectionalArea;
            Material = material;
            UserModule = userModule;
            MulticoreName = multicoreName;
            End1NodeName = end1NodeName;
            End1Route = end1Route;
            End1Cavity = end1Cavity;
            End1MaterialCode = end1MaterialCode;
            End2NodeName = end2NodeName;
            End2Route = end2Route;
            End2Cavity = end2Cavity;
            End2MaterialCode = end2MaterialCode;
            IncludeOnBOM = includeOnBOM;
            IncludeOnChart = includeOnChart;
            WireTag = wireTag;
            WireNote = wireNote;
            WireLengthChangeType = wireLengthChangeType;
            WireLengthChangeValue = wireLengthChangeValue;
            AssemblyItemNumber = assemblyItemNumber;
            MulticoreOption = multicoreOption;
        }

        public Wire()
        {

        }

        public override string ToString()
        {
            List<string> properties = new List<string>();

            if (!string.IsNullOrEmpty(WireName)) properties.Add($"WireName: {WireName}");
            if (!string.IsNullOrEmpty(WireOption)) properties.Add($"WireOption: {WireOption}");
            if (!string.IsNullOrEmpty(WireType)) properties.Add($"WireType: {WireType}");
            if (!string.IsNullOrEmpty(Color)) properties.Add($"Color: {Color}");
            if (!string.IsNullOrEmpty(CrossSectionalArea)) properties.Add($"CrossSectionalArea: {CrossSectionalArea}");
            if (!string.IsNullOrEmpty(Material)) properties.Add($"Material: {Material}");
            if (!string.IsNullOrEmpty(UserModule)) properties.Add($"UserModule: {UserModule}");
            if (!string.IsNullOrEmpty(MulticoreName)) properties.Add($"MulticoreName: {MulticoreName}");
            if (!string.IsNullOrEmpty(End1NodeName)) properties.Add($"End1NodeName: {End1NodeName}");
            if (!string.IsNullOrEmpty(End1Route)) properties.Add($"End1Route: {End1Route}");
            if (!string.IsNullOrEmpty(End1Cavity)) properties.Add($"End1Cavity: {End1Cavity}");
            if (!string.IsNullOrEmpty(End1MaterialCode)) properties.Add($"End1MaterialCode: {End1MaterialCode}");
            if (!string.IsNullOrEmpty(End2NodeName)) properties.Add($"End2NodeName: {End2NodeName}");
            if (!string.IsNullOrEmpty(End2Route)) properties.Add($"End2Route: {End2Route}");
            if (!string.IsNullOrEmpty(End2Cavity)) properties.Add($"End2Cavity: {End2Cavity}");
            if (!string.IsNullOrEmpty(End2MaterialCode)) properties.Add($"End2MaterialCode: {End2MaterialCode}");
            if (!string.IsNullOrEmpty(IncludeOnBOM)) properties.Add($"IncludeOnBOM: {IncludeOnBOM}");
            if (!string.IsNullOrEmpty(IncludeOnChart)) properties.Add($"IncludeOnChart: {IncludeOnChart}");
            if (!string.IsNullOrEmpty(WireTag)) properties.Add($"WireTag: {WireTag}");
            if (!string.IsNullOrEmpty(WireNote)) properties.Add($"WireNote: {WireNote}");
            if (!string.IsNullOrEmpty(WireLengthChangeType)) properties.Add($"WireLengthChangeType: {WireLengthChangeType}");
            if (!string.IsNullOrEmpty(WireLengthChangeValue)) properties.Add($"WireLengthChangeValue: {WireLengthChangeValue}");
            if (!string.IsNullOrEmpty(AssemblyItemNumber)) properties.Add($"AssemblyItemNumber: {AssemblyItemNumber}");
            if (!string.IsNullOrEmpty(MulticoreOption)) properties.Add($"MulticoreOption: {MulticoreOption}");

            return string.Join(", ", properties);
        }
    }
}
