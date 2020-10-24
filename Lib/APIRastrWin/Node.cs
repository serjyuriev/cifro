using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Node
    {
        public int Status { get; set; }

        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public double Unom { get; set; }

        public int Ncxn { get; set; }

        public double Pn { get; set; }

        public double Qn { get; set; }

        public double Pgn { get; set; }

        public double Qgn { get; set; }

        public double Vzd { get; set; }

        public double Qmin { get; set; }

        public double Qmax { get; set; }

        public double Bshunt { get; set; }

        public double V { get; set; }

        public double Delta { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Node(string status, string id, string type, string name, string unom, string ncxn,
                    string pn, string qn, string pgn, string qgn, string vzd, string qmin,
                    string qmax, string bshunt, string v, string delta)
        {
            Status = int.Parse(status);
            Id = int.Parse(id);
            Type = type;
            Name = name;
            Unom = double.Parse(unom);
            Ncxn = int.Parse(ncxn);
            Pn = double.Parse(pn);
            Qn = double.Parse(qn);
            Pgn = double.Parse(pgn);
            Qgn = double.Parse(qgn);
            Vzd = double.Parse(vzd);
            Qmin = double.Parse(qmin);
            Qmax = double.Parse(qmax);
            Bshunt = double.Parse(bshunt);
            V = double.Parse(v);
            Delta = double.Parse(delta);
        }
    }
}
