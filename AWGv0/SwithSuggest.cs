using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGv0
{
    /// <summary>
    /// Предложения по включению
    /// </summary>
    public class SwithSuggest
    {
        /// <summary>
        /// Алгоритм вглубь
        /// </summary>
        private DepthFirstSearchAlgoritm _depth;

        /// <summary>
        /// Алгоритм вширь
        /// </summary>
        private BreadthFirstSearchAlgoritm _breadth;

        /// <summary>
        /// Количество узлов
        /// </summary>
        private int _length;

        /// <summary>
        /// Матрица смежности всех связей
        /// </summary>
        private int[,] _fullMatrix;

        /// <summary>
        /// Матрица смежности сложившейся схемно-режимной ситуации
        /// </summary>
        private int[,] _EPRMatrix;

        /// <summary>
        /// Матрица смежности готовых к включению КА
        /// </summary>
        private int[,] _readySwitchMatrix;

        /// <summary>
        /// Матрица приоритетов 
        /// </summary>
        private int[] _priorityMatrix;

        /// <summary>
        /// Матрица смежности всех связей
        /// </summary>
        public int[,] FullMatrix
        {
            get => _fullMatrix;
            set => _fullMatrix = value;
        }

        /// <summary>
        /// Матрица смежности сложившейся схемно-режимной ситуации
        /// </summary>
        public int[,] EPRMatrix
        {
            get => _EPRMatrix;
            set => _EPRMatrix = value;
        }

        /// <summary>
        /// Матрица смежности готовых к включению КА
        /// </summary>
        public int[,] ReadySwitchMatrix
        {
            get => _readySwitchMatrix;
            set => _readySwitchMatrix = value;
        }

        /// <summary>
        /// Матрица приоритетов 
        /// </summary>
        public int[] PriorityMatrix
        {
            get => _priorityMatrix;
            set => _priorityMatrix = value;
        }

        /// <summary>
        /// Матрица пути 
        /// </summary>
        public int[,] Way { get; set; }

        /// <summary>
        /// Узлы по островам
        /// </summary>
        public Dictionary<int,int[]> Islands { get; set; }


        /// <summary>
        /// Предложения по включению
        /// </summary>
        public SwithSuggest(
            int[,] fullMatrix,
            int[,] EPRMatrixx,
            int[,] readySwithcMatrix,
            int length,
            int from,
            int to)
        {
            _fullMatrix = fullMatrix;
            _EPRMatrix = EPRMatrixx;
            _readySwitchMatrix = readySwithcMatrix;
            //_priorityMatrix = priorityMatrix;
            // получить число узлов
            _length = length;
            _depth = new DepthFirstSearchAlgoritm(EPRMatrix);
            Islands = GetIsland();
            // затестим функции поиска пути сначала
            _breadth = new BreadthFirstSearchAlgoritm(FullMatrix, from);
            Way = new int[length, length];
            Way = SearchWay(to);

        }

        /// <summary>
        /// Поиск островов
        /// </summary>
        /// <returns>Узлы по островам</returns>
        private Dictionary<int, int[]> GetIsland()
        {
            var listOfComponents = new Dictionary<int, int[]>();
            _depth.SearchConnDFS();
            var comp = _depth.Comp;
            for (int i = 0; i < comp.Max(); i++)
            {
                var length = 0;

                for (int j = 0; j < comp.Length; j++)
                {
                    if (comp[j] == i)
                    {
                        length++;
                    }
                }

                var components = new int[length];

                var count = 0;

                for (int j = 0; j < comp.Length; j++)
                {
                    if (comp[j] == i)
                    {
                        components[count] = comp[j];
                        count++;
                    }
                }

                listOfComponents.Add(i, components);

            }

            return listOfComponents;
        }

        /// <summary>
        /// Поиск пути между узлами
        /// </summary>
        /// <param name="to"></param>
        public int[,] SearchWay(int to)
        {
            _breadth.BreadthFirstSearch();

            var shortestWay = _breadth.GetShortestWay(to);

            var wayMatrix = new int[_length, _length];

            for (int i = 1; i < shortestWay.Count; i++)
            {
                wayMatrix[shortestWay[i - 1], shortestWay[i]] = 1;
                wayMatrix[shortestWay[i], shortestWay[i - 1]] = 1;
            }

            // Проверка, включены ли КА на пути
            var checkWayMatrix = MultiplyUnits(EPRMatrix, wayMatrix, _length);

            // КА, которые надо включить, если они отключены
            var beONtheWayMatrix = new int[_length, _length];

            if (checkWayMatrix != wayMatrix)
            {
                beONtheWayMatrix = DifferenceUnits(wayMatrix, checkWayMatrix, _length);
            }
            // надо else на случай включенного пути

            // Проверка на ограничения по включению КА
            var possibilityONWaymatrix = MultiplyUnits(ReadySwitchMatrix, beONtheWayMatrix, _length);

            if (possibilityONWaymatrix == beONtheWayMatrix)
            {
                return beONtheWayMatrix;
            }
            else
            {
                return null;
                // else надо переписать
            }
        }

        /// <summary>
        /// Поэлементное перемножение матриц
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int[,] MultiplyUnits(int[,] array1, int[,] array2, int length)
        {
            var resultArray = new int[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    resultArray[i, j] = array1[i, j] * array2[i, j];
                }
            }

            return resultArray;
        }

        /// <summary>
        /// Поэлементное вычитание матриц
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int[,] DifferenceUnits(int[,] array1, int[,] array2, int length)
        {
            var resultArray = new int[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    resultArray[i, j] = array1[i, j] - array2[i, j];
                }
            }

            return resultArray;
        }


    }
}
