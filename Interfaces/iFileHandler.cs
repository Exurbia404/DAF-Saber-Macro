using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Data_Interfaces
{
    public interface iFileHandler
    {
        public void WriteToFile(List<iConverted_Wire> wires, List<iConverted_Component> components, List<iBundle> extractedBundles, string fileName, string filePath);

    }
}
