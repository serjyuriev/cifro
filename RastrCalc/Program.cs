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
            var pathToNormal = @"D:\testNorm.rg2";
            var pathToKZ = @"D:\testKZ.rg2";

            rastr.LoadNormRegime(pathToNormal);
            rastr.LoadKZRegime(pathToKZ);

            // В таком виде должна (на данный момент)
            // быть информация об изменении состояния
            // ветвей и узлов в схеме
            // true - выключена, false - включена
            //var nodes = new Dictionary<string, bool>()
            //{
            //    {"30", true },
            //    {"31", true }
            //};

            var branches = new Dictionary<string, bool>()
            {
                { "27-7", false }
            };

            rastr.CalculateSwitching(null, branches);

            rastr.SaveRegime();

            Console.WriteLine(rastr.SwitchingFlag);

            Console.Read();
        }
    }
}
