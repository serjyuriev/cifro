using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AWGv0
{
    /// <summary>
    /// Алгоритм обхода графа вглубь
    /// </summary>
    public class DepthFirstSearchAlgoritm
    {
        /// <summary>
        /// Матрица смежности
        /// </summary>
        public int[,] Matrix { get; set; }

        /// <summary>
        /// Список смежности
        /// </summary>
        public Dictionary<int, int[]> ListOfMatrix { get; set; }

        /// <summary>
        /// Массив посещения
        /// </summary>
        public int[] UsedMatrix { get; set; }

        /// <summary>
        /// Компоненты отдельного графа
        /// </summary>
        public int[] Comp { get; set; }

        /// <summary>
        /// Алгоритм обхода графа вглубь
        /// </summary>
        public DepthFirstSearchAlgoritm(int[,] matrix)
        {
            Matrix = matrix;
            UsedMatrix = GetUsedMatrix();
            ListOfMatrix = GetListOfMatrix();
            Comp = new int[UsedMatrix.Length];
        }

        /// <summary>
        /// Матрица смежности в список смежности
        /// </summary>
        /// <returns>список смежности</returns>
        private Dictionary<int, int[]> GetListOfMatrix()
        {
            var lenght = Matrix.GetUpperBound(0) + 1;
            Dictionary<int, int[]> listOfMatrix = new Dictionary<int, int[]>();

            for (int i = 0; i < lenght; i++)
            {
                var element = i;
                var numberConns = 0;

                for (int j = 0; j < lenght; j++)
                {
                    if (Matrix[i,j] == 1)
                    {
                        numberConns++;
                    }
                }

                var conns = new int[numberConns];
                numberConns = 0;

                for (int j = 0; j < lenght; j++)
                {
                    if (Matrix[i, j] == 1)
                    {
                        conns[numberConns] = j;
                        numberConns++;
                    }
                }

                listOfMatrix.Add(element, conns);
            }

            return listOfMatrix;
        }

        /// <summary>
        /// Массив посещения вершины
        /// </summary>
        /// <returns>массив</returns>
        private int[] GetUsedMatrix()
        {
            var lenght = Matrix.GetUpperBound(0) + 1;
            int[] usedMatrix = new int[lenght];
            return usedMatrix;
        }

        /// <summary>
        /// Алгоритм обхода графа вглубину
        /// </summary>
        /// <param name="v">элемента начала обхода</param>
        public void DepthFirstSearch(int v, int cNum)
        {
            UsedMatrix[v] = 1;
            Comp[v] = cNum;
            foreach (var element in ListOfMatrix[v])
            {
                if (UsedMatrix[element] == 0)
                {
                    DepthFirstSearch(element, cNum);
                }
            }

        }

        /// <summary>
        /// Алгоритм поиска несвязных графов
        /// </summary>
        /// <param name="v">элемент начала обхода</param>
        public void SearchConnDFS()
        {
            var lenght = Matrix.GetUpperBound(0) + 1;

            var cNum = 0;

            for (int i = 0; i < lenght; i++)
            {
                if (UsedMatrix[i] == 0)
                {
                    cNum++;
                    DepthFirstSearch(i, cNum);
                }
            }

        }

    }
}
