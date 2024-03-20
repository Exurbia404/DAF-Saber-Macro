using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Bundle
    {
        //I cannot use the DSI v7 spec as it does not seem to match
        public string VariantNumber { get; set; }
        public string Issue {  get; set; }
        public string Date {  get; set; }

        //Atleast i think they are references?
        public string[] References { get; set; }

        public Bundle() { }

        public override string ToString()
        {
            return base.ToString();
        }


    }
}
