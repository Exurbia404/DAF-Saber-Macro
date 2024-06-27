using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Interfaces;
using Data_Interfaces;

namespace Logic
{
    public class Converted_Wire : UI_Interfaces.iConverted_Wire, Data_Interfaces.iConverted_Wire
    {
        public string Wire {  get; set; }
        public string Diameter { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Code_no { get; set; }
        public string Length { get; set; }
        public string Connector_1 { get; set; }
        public string Port_1 { get; set; }
        public string Term_1 { get; set; }
        public string Seal_1 { get; set; }
        public string Wire_connection {  get; set; }
        public string Term_2 { get; set; }
        public string Seal_2 { get; set;}
        public string Connector_2 { get; set; }
        public string Port_2 { get; set; }
        public string Variant {  get; set; }
        public string Bundle { get; set; }
        public string Temp_Class { get; set; }
        public string CC_T { get; set; }
        public string CC_S { get; set; }

        public Converted_Wire(string code_no, string diameter, string color, string type, string part_no, string length,
                  string connector_1, string port_1, string term_1, string seal_1, string wire_connection,
                  string term_2, string seal_2, string connector_2, string port_2, string variant, string bundle,
                  string temp_Class)
        {
            Wire = code_no;
            Diameter = diameter;
            Color = color;
            Type = type;
            Code_no = part_no;
            Length = length;
            Connector_1 = connector_1;
            Port_1 = port_1;
            Term_1 = term_1;
            Seal_1 = seal_1;
            Wire_connection = wire_connection;
            Term_2 = term_2;
            Seal_2 = seal_2;
            Connector_2 = connector_2;
            Port_2 = port_2;
            Variant = variant;
            Bundle = bundle;
            Temp_Class = temp_Class;
        }

        public void SetCoCoData(string cc_t, string cc_s)
        {
            CC_T = cc_t; CC_S = cc_s;
        }

        public Converted_Wire()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
