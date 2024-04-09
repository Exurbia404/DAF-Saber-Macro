using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Interfaces
{
    public class iProfile
    {
        public enum ProfileType
        {
            Wire,
            Component,
            Project_Component,
            Project_Wire,
            User
        }
        public string Name;
        public List<string> Parameters;
        public ProfileType Type;
    }
}
