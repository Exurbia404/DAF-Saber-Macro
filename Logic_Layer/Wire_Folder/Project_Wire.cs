using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Project_Wire
    {
        public string Wire { get; set; }
        public string Diameter { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Connector_1 { get; set; }
        public string Port_1 { get; set; }
        public string Term_1 { get; set; }
        public string Seal_1 { get; set; }

        //Well aware this isn't spelled correctly
        public string Lokation_1 { get; set; }
        public string Wire_connection { get; set; }
        public string Term_2 { get; set; }
        public string Seal_2 { get; set; }
        public string Connector_2 { get; set; }
        public string Port_2 { get; set; }
        public string Lokation_2 { get; set; }
        public string Harness { get; set; }
        public string Variant { get; set; }
        public string Bundle { get; set; }

        //Also this isn't proper english
        public string Kodenr_wire { get; set; }
        public string Tag { get; set; }

        public Project_Wire(string wire, string diameter, string color, string type, string connector_1, string port_1, string term_1, string seal_1, string lokation_1, string wire_connection, string term_2, string seal_2, string connector_2, string port_2, string lokation_2, string harness, string variant, string bundle, string kodenr_wire, string tag)
        {
            Wire = wire;
            Diameter = diameter;
            Color = color;
            Type = type;
            Connector_1 = connector_1;
            Port_1 = port_1;
            Term_1 = term_1;
            Seal_1 = seal_1;
            Lokation_1 = lokation_1;
            Wire_connection = wire_connection;
            Term_2 = term_2;
            Seal_2 = seal_2;
            Connector_2 = connector_2;
            Port_2 = port_2;
            Lokation_2 = lokation_2;
            Harness = harness;
            Variant = variant;
            Bundle = bundle;
            Kodenr_wire = kodenr_wire;
            Tag = tag;
        }

        public Project_Wire()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
