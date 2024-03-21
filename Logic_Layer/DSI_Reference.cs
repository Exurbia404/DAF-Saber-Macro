using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DSI_Reference
    {
        public string YearWeek { get; set; }
        public string BundleNumber { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        
        public DSI_Reference(string yearWeek, string bundleNumber, string projectName, string description) 
        {
            YearWeek = yearWeek;
            BundleNumber = bundleNumber;
            ProjectName = projectName;
            Description = description;
        }

        public DSI_Reference() { }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
