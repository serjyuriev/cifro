using Lib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastrCalcTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rastr = new RastrCalc();

            rastr.LoadRegime(@"Z:\Магистратура\Семестр 1\" + 
                @"Режимы и устойчивость электроэнергетических систем\" + 
                @"Лабораторная работа №4\Юрьев.rg2");

            var nodes = new Dictionary<string, bool>()
            {
                {"30", true },
                {"31", true }
            };

            var branches = new Dictionary<string, bool>()
            {
                { "29-30", true },
                { "29-31", true }
            };

            rastr.ChangeState(nodes, branches);
            rastr.SaveRegime();

            Console.Read();
        }
    }
}
