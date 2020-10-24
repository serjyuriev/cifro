using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGv0
{
    /// <summary>
    /// Алгоритм обхода графа вширь
    /// </summary>
    public class BreadthFirstSearchAlgoritm
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
        /// Очередь вершин
        /// </summary>
        public Queue<int> queueQ { get; set; }

        /// <summary>
        /// Стартовая вершина
        /// </summary>
        public int startV { get; set; }

        /// <summary>
        /// Массив посещения
        /// </summary>
        public int[] UsedMatrix { get; set; }

        /// <summary>
        /// Расстояния
        /// </summary>
        public int[] Distans { get; set; }

        /// <summary>
        /// Предки
        /// </summary>
        public int[] Ances { get; set; }

        /// <summary>
        /// Путь
        /// </summary>
        public List<int> Path { get; set; }

        /// <summary>
        /// Алгоритм обхода графа вширь
        /// </summary>
        public BreadthFirstSearchAlgoritm(int[,] matrix, int v)
        {
            Matrix = matrix;
            ListOfMatrix = GetListOfMatrix();
            startV = v;
            queueQ = new Queue<int>();
            queueQ.Enqueue(startV);
            var lenght = Matrix.GetUpperBound(0) + 1;
            UsedMatrix = new int[lenght];
            Distans = new int[lenght];
            Ances = new int[lenght];
            UsedMatrix[startV] = 1;
            Ances[startV] = -1;
            Path = new List<int>();
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
                    if (Matrix[i, j] == 1)
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
        /// Алгоритм обхода графа вширину
        /// </summary>
        /// <param name="v">элемента начала обхода</param>
        public void BreadthFirstSearch()
        {
            while(queueQ.Any())
            {
                startV = queueQ.Dequeue();

                for (int i = 0; i < ListOfMatrix[startV].Length; i++)
                {
                    int to = ListOfMatrix[startV][i];

                    if (UsedMatrix[to] == 0)
                    {
                        UsedMatrix[to] = 1;

                        queueQ.Enqueue(to);

                        Distans[to] = Distans[startV] + 1;

                        Ances[to] = startV;
                    }
                }
            }
        }

        /// <summary>
        /// Кратчайший путь до узла
        /// </summary>
        /// <returns>Кратчайший путь</returns>
        public List<int> GetShortestWay(int to)
        {
            var cout = new List<int>();

            if (UsedMatrix[to] == 0)
            {
                cout.Add(-1);
            }
            else
            {
                for (int v = to; v != -1; v = Ances[v])
                {
                    Path.Add(v);
                }

                Path.Reverse();

                for (int i = 0; i < Path.Count(); i++)
                {
                    cout.Add(Path[i]);
                }
            }

            return cout;
        }

    }
}
