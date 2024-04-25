using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    public class Extractor_Copy
    {
        public List<Converted_Component> Components { get; private set; }
        public List<Converted_Wire> Wires { get; private set; }

        public List<Bundle> Bundles { get; private set; }

        private Logger _logger;
        private List<List<string[]>> sections;
        private string filePath;
        
        public Extractor_Copy(Logger logger, string filepath)
        {
            _logger = logger;
            filePath = filepath;

            //1 get the sections
            sections = RetrieveSectionsFromFile();

            //2 Get the Wires

            //3 Get the Components

            //4 Get the bundles

            //Conversion 

            //Load them into public properties
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
    }
}
