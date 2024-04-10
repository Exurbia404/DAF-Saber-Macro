using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Interfaces;

namespace Logic_Layer
{
    public class Profile : iProfile
    {
        public string Name;
        public List<string> Parameters;
        public ProfileType Type;

        public Profile(string profileName, List<string> parameters, ProfileType type)
        {
            Name = profileName;
            Parameters = parameters;
            Type = type;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
