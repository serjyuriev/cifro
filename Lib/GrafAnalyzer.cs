using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class GrafAnalyzer
    {

        public double[,] GetFullAdjacencyMatrix(List<Node> nodes, List<Link> links) 
        {
            double[,] matrix = new double[nodes.Count+1, nodes.Count+1];
            List<Node> sortedNodes = nodes.OrderBy(o => o.Id).ToList();
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                matrix[i + 1, 0] = sortedNodes[i].Id;
            }
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                matrix[0, i+1] = sortedNodes[i].Id;
            }
            foreach (var item in links)
            {
                matrix[item.StartNode, item.EndNode] = item.X;
            }
            DisplayMatrix(matrix);
            return matrix;
        }

        public double[,] GetCurrentAdjacencyMatrix(List<Node> nodes, List<Link> links)
        {
            double[,] matrix = new double[nodes.Count + 1, nodes.Count + 1];
            List<Node> sortedNodes = nodes.OrderBy(o => o.Id).ToList();
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                matrix[i + 1, 0] = sortedNodes[i].Id;
            }
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                matrix[0, i + 1] = sortedNodes[i].Id;
            }
            foreach (var item in links)
            {
                if (item.Status==0)
                {
                    matrix[item.StartNode, item.EndNode] = item.X;
                }
            }
            DisplayMatrix(matrix);
            return matrix;
        }

        public double[,] GetControlActionsMatrix(List<Node> nodes, List<Link> links)
        {
            double[,] matrix = new double[nodes.Count + 1, nodes.Count + 1];
            List<Node> sortedNodes = nodes.OrderBy(o => o.Id).ToList();
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                matrix[i + 1, 0] = sortedNodes[i].Id;
            }
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                matrix[0, i + 1] = sortedNodes[i].Id;
            }
            foreach (var item in links)
            {
                matrix[item.StartNode, item.EndNode] = item.X;
            }
            DisplayMatrix(matrix);
            return matrix;
        }

        public void DisplayMatrix(double[,] matrix) 
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < (matrix.GetLength(1)); j++)
                {
                    Console.Write("{0}\t", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
