using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DSI_Tube
    {
        public string Length { get; private set; }
        public string Insulations { get; set; }
        public string StartNode { get; private set; }
        public string EndNode { get; private set; }

        public DSI_Tube(string length, string insulations, string startnode, string endnode)
        {
            Length = length;
            Insulations = insulations;
            StartNode = startnode;
            EndNode = endnode;
        }

        public override string ToString()
        {
            return $"Length: {Length}, Insulations: {Insulations}, StartNode: {StartNode}, EndNode: {EndNode}";
        }
    }
}
