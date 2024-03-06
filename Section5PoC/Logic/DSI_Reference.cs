using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC.Logic
{
    public class DSI_Reference
    {
        public string YearWeek { get; private set; }
        public string BundleNumber { get; private set; }
        public string ProjectName { get; private set; }
        public string Description { get; private set; }
        
        public DSI_Reference(string yearWeek, string bundleNumber, string projectName, string description) 
        {
            YearWeek = yearWeek;
            BundleNumber = bundleNumber;
            ProjectName = projectName;
            Description = description;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
