using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section5PoC
{
    public class WCSPP_Convertor
    {
        //WCSPP contains:
        //Wire, Diameter, Color, Type, Code_no, Length, Connector_1, Port_1, Term_1, Seal_1, Wire_connection, Term_2, Seal_2, Connector_2, Port_2, Variant, Bundle, Loc_1, Loc_2

        ExcelHandler wcsppExcelHandler;

        public WCSPP_Convertor() 
        {
            wcsppExcelHandler = new ExcelHandler();
        }

        public void ConvertListToWCSPPTextFile(List<Wire> listToConvert)
        {

        }

        public void ConvertListToWCSPPExcelFile(List<Wire> listToConvert) 
        {
            List<WCSPP_Wire> convertedList = new List<WCSPP_Wire>();

            foreach (Wire wire in listToConvert)
            {
                WCSPP_Wire wCSPP_Wire = new WCSPP_Wire(wire.WireName, wire.CrossSectionalArea, wire.Color, wire.Material, wire.WireNote, wire.WireNote, wire.End1NodeName, wire.End1Route, "?", "?", "combination", "?", "?", "?", "?", "?", "?", "?");
                convertedList.Add(wCSPP_Wire);            
            }

            wcsppExcelHandler.CreateExcelSheet(convertedList);
        }

    }
}
