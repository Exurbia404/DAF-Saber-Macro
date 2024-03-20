using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Project_Component
    {
        public string Type { get; set; }
        public string Ref { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Connector { get; set; }
        public string SecLock { get; set; }
        public string Harness { get; set; }
        public string Variant { get; set; }
        public string Bundle { get; set; }
        public string Tag { get; set; }
        public string System { get; set; }
        public string Fuse_value { get; set; }
        public string Color { get; set; }
        public string N_pins { get; set; }

        public Project_Component(string type, string @ref, string description, string location, string connector, string secLock, string harness, string variant, string bundle, string tag, string system, string fuse_value, string color, string n_pins)
        {
            Type = type;
            Ref = @ref;
            Description = description;
            Location = location;
            Connector = connector;
            SecLock = secLock;
            Harness = harness;
            Variant = variant;
            Bundle = bundle;
            Tag = tag;
            System = system;
            Fuse_value = fuse_value;
            Color = color;
            N_pins = n_pins;
        }

        public Project_Component()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
