using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Interfaces
{
    public interface IProject_Component
    {
        string Type { get; set; }
        string Ref { get; set; }
        string Description { get; set; }
        string Location { get; set; }
        string Connector { get; set; }
        string SecLock { get; set; }
        string Harness { get; set; }
        string Variant { get; set; }
        string Bundle { get; set; }
        string Tag { get; set; }
        string System { get; set; }
        string Fuse_value { get; set; }
        string Color { get; set; }
        string N_pins { get; set; }
    }
}
