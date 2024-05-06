using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Interfaces;

namespace Logic
{
    public class Profile : iProfile
    {
        public string Name { get; set; }
        public List<string> Parameters { get; set; }
        public ProfileType Type { get; set; }

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
