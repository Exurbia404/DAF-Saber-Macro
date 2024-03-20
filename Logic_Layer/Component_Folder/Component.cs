using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Component
    {
        public string NodeName { get; set; }
        public string CavityName { get; set; }
        public string WireName { get; set; }
        public string SequenceNumber { get; set; }
        public string ComponentTypeCode { get; set; }
        public string CircuitOption { get; set; }
        public string ServiceFunction { get; set; }
        public string Route { get; set; }
        public string PartNumber1 { get; set; }
        public string Quantity { get; set; }
        public string CrossSectionalArea { get; set; }
        public string PartNumber2 { get; set; }
        public string PartNumber3 { get; set; }
        public string SelectTerminal { get; set; }
        public string Seal { get; set; }
        public string Plugged { get; set; }
        public string BlockNumber { get; set; }
        public string TerminationMethod { get; set; }
        public string MaterialCode { get; set; }
        public string ComponentTypeCode2 { get; set; }

        

        public Component(

        string nodeName,
        string cavityName,
        string wireName,
        string sequenceNumber,
        string componentTypeCode,
        string circuitOption,
        string serviceFunction,
        string route,
        string partNumber1,
        string quantity,
        string crossSectionalArea,
        string partNumber2,
        string partNumber3,
        string selectTerminal,
        string seal,
        string plugged,
        string blockNumber,
        string terminationMethod,
        string materialCode,
        string componentTypeCode2)
        {
            NodeName = nodeName;
            CavityName = cavityName;
            WireName = wireName;
            SequenceNumber = sequenceNumber;
            ComponentTypeCode = componentTypeCode;
            CircuitOption = circuitOption;
            ServiceFunction = serviceFunction;
            Route = route;
            PartNumber1 = partNumber1;
            Quantity = quantity;
            CrossSectionalArea = crossSectionalArea;
            PartNumber2 = partNumber2;
            PartNumber3 = partNumber3;
            SelectTerminal = selectTerminal;
            Seal = seal;
            Plugged = plugged;
            BlockNumber = blockNumber;
            TerminationMethod = terminationMethod;
            MaterialCode = materialCode;
            ComponentTypeCode2 = componentTypeCode2;
        }
        public Component()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
