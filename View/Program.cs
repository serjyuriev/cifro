using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            APIRastrWin _rastrAPI = new APIRastrWin();
            //_rastrAPI.LoadRegime(@"C:\Users\Ruslan\Desktop\IEEE-57\IEEE-57.rg2");
            _rastrAPI.LoadRegime(@"C:\Users\Ruslan\Desktop\Режим 2.rg2");
            var nodes = _rastrAPI.GetNodes();
            var links = _rastrAPI.GetLinks();

            GrafAnalyzer grafAnalyzer = new GrafAnalyzer();
            var fullAdjacencyMatrix = grafAnalyzer.GetFullAdjacencyMatrix(nodes, links);
            var currentAdjacencyMatrix = grafAnalyzer.GetCurrentAdjacencyMatrix(nodes, links);
            var a = _rastrAPI.CheckRegimeStatus();

        }

    }
}
