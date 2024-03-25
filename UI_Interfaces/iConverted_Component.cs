using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Interfaces
{
    public interface iConverted_Component
    {
        public string Name { get; set; }
        public string Part_no { get; set; }
        //space between here whilst writing to text
        public string Passive { get; set; }
        public string Instruction { get; set; }
        public string Variant { get; set; }
        public string Bundle { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        //9 spaces after this when writing ending with a ,
        public string EndText { get; set; }
    }
}
