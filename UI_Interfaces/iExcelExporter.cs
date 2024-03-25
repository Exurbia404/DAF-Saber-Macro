using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Interfaces
{
    public interface iExcelExporter
    {
        void CreateProjectExcelSheet(List<iProject_Wire> wires, List<IProject_Component> components);
        void CreateExcelSheet(List<iConverted_Wire> wires, List<iConverted_Component> components);
    }
}
