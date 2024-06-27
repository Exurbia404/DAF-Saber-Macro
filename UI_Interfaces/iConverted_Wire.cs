using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Interfaces
{
    public interface iConverted_Wire
    {
        public string Wire { get; set; }
        public string Diameter { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Code_no { get; set; }
        public string Length { get; set; }
        public string Connector_1 { get; set; }
        public string Port_1 { get; set; }
        public string Term_1 { get; set; }
        public string Seal_1 { get; set; }
        public string Wire_connection { get; set; }
        public string Term_2 { get; set; }
        public string Seal_2 { get; set; }
        public string Connector_2 { get; set; }
        public string Port_2 { get; set; }
        public string Variant { get; set; }
        public string Bundle { get; set; }
        public string Temp_Class { get; set; }
        public string CC_T { get; set; }
        public string CC_S { get; set; }
    }
}
