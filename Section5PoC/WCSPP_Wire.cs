using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class WCSPP_Wire
    {
        public string Code_no {  get; set; }
        public string Length { get; set; }
        public string Connector_1 { get; set; }
        public string Port_1 { get; set; }
        public string Term_1 { get; set; }
        public string Seal_1 { get; set; }
        public string Wire_connection {  get; set; }
        public string Term_2 { get; set; }
        public string Seal_2 { get; set;}
        public string Connector_2 { get; set; }
        public string Variant {  get; set; }
        public string Bundle { get; set; }
        public string Loc_1 { get; set; }
        public string Loc_2 { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
