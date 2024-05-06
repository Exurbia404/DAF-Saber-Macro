using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Interfaces
{
    public interface iProfile
    {
        public string Name { get; set; }
        public List<string> Parameters { get; set; }
        public ProfileType Type { get; set; }
    }
}
