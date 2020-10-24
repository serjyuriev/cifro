using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Link
    {
        public int Status { get; set; }

        public string Type { get; set; }

        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public int ParallelNumber { get; set; }

        public int GroupID { get; set; }

        public string Name { get; set; }

        public double R { get; set; }

        public double X { get; set; }

        public double B { get; set; }

        public double Ktr { get; set; }

        public int NumberAnc { get; set; }

        public int BD { get; set; }

        public double Pl { get; set; }

        public double Ql { get; set; }

        public int Na { get; set; }

        public double Imax { get; set; }

        public double Izaq { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Link(string status, string type, string startNode, string endNode, string parallelNumber,
                    string groupID, string name, string r, string x, string b, string ktr,
                    string numberAnc, string bd, string pl, string ql, string na, string imax, string izaq)
        {
            Status = int.Parse(status);
            Type = type;
            StartNode = int.Parse(startNode);
            EndNode = int.Parse(endNode);
            ParallelNumber = int.Parse(parallelNumber);
            GroupID = int.Parse(groupID);
            Name = name;
            R = double.Parse(r);
            X = double.Parse(x);
            B = double.Parse(b);
            Ktr = double.Parse(ktr);
            NumberAnc = int.Parse(numberAnc);
            BD = int.Parse(bd);
            Pl = double.Parse(pl);
            Ql = double.Parse(ql);
            Na = int.Parse(na);
            Imax = double.Parse(imax);
            Izaq = double.Parse(izaq);
        }
    }
}
