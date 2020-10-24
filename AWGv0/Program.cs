using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGv0
{
    class Program
    {
        static void Main(string[] args)
        {
            var allMatrix = GetAllMatrix();

            var matrix = GetNormalMatrix();

            for (int i = 0; i < allMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < allMatrix.GetUpperBound(1) + 1; j++)
                    Console.Write(String.Format("{0,3}", allMatrix[i, j]));
                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                    Console.Write(String.Format("{0,3}", matrix[i, j]));
                Console.WriteLine();
            }

            Console.WriteLine();

            // Авария:
            var matrixAlarm = GetNormalMatrix();
            Console.WriteLine("Авария.");
            Console.WriteLine();
            matrixAlarm[1, 2] = 0;
            matrixAlarm[2, 1] = 0;
            matrixAlarm[0, 5] = 0;
            matrixAlarm[5, 0] = 0;
            for (int i = 0; i < matrixAlarm.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrixAlarm.GetUpperBound(1) + 1; j++)
                    Console.Write(String.Format("{0,3}", matrixAlarm[i, j]));
                Console.WriteLine();
            }
            Console.WriteLine();
            // Авария

            //Тест алгоритма вглубину
            //Depth(matrixAlarm);

            //Тест алгоритма вширину
            //Breadth(allMatrix, 3, 5);

            //Тест поиска пути
            var readySwitch = GetReadySwitchMatrix();
            var length = allMatrix.GetUpperBound(0) + 1;
            SwithSuggest suggest = new SwithSuggest(allMatrix, matrixAlarm, readySwitch, length, 0, 2);
            var matrixON = new int[length, length];
            matrixON = suggest.Way;

            for (int i = 0; i < matrixON.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrixON.GetUpperBound(1) + 1; j++)
                    Console.Write(String.Format("{0,3}", matrixON[i, j]));
                Console.WriteLine();
            }

            Console.WriteLine();

        }

        /// <summary>
        /// Тест алгоритма вширину
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void Breadth(int[,] matrix, int from, int to)
        {
            BreadthFirstSearchAlgoritm algoritm = new BreadthFirstSearchAlgoritm(matrix, from);

            algoritm.BreadthFirstSearch();

            var output = algoritm.GetShortestWay(to);

            var outputString = string.Empty;

            foreach (var elem in output)
            {
                outputString = outputString + $"{elem} ";
            }

            Console.WriteLine(outputString);

            Console.ReadKey();
        }

        /// <summary>
        /// Тест алгоритма в глубину
        /// </summary>
        /// <param name="matrix"></param>
        public static void Depth(int[,] matrix)
        {
            DepthFirstSearchAlgoritm algoritm = new DepthFirstSearchAlgoritm(matrix);

            algoritm.DepthFirstSearch(0, 0);

            var used1 = algoritm.UsedMatrix;

            for (int i = 0; i < used1.GetUpperBound(0) + 1; i++)
            {
                Console.Write(String.Format($"{i}) " + "{0,3}", used1[i]));
                Console.WriteLine();
            }

            Console.WriteLine();

            algoritm.SearchConnDFS();

            var comp = algoritm.Comp;

            for (int i = 0; i < comp.GetUpperBound(0) + 1; i++)
            {
                Console.Write(String.Format($"{i}) " + "{0,3}", comp[i]));
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.ReadKey();
        }

        public static int[,] GetAllMatrix()
        {
            int[,] matrix = new int[6, 6];
            matrix[0, 0] = 0;
            matrix[0, 1] = 1;
            matrix[0, 2] = 0;
            matrix[0, 3] = 0;
            matrix[0, 4] = 0;
            matrix[0, 5] = 1;
            matrix[1, 0] = 1;
            matrix[1, 1] = 0;
            matrix[1, 2] = 1;
            matrix[1, 3] = 0;
            matrix[1, 4] = 0;
            matrix[1, 5] = 0;
            matrix[2, 0] = 0;
            matrix[2, 1] = 1;
            matrix[2, 2] = 0;
            matrix[2, 3] = 1;
            matrix[2, 4] = 1;
            matrix[2, 5] = 1;
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 1;
            matrix[3, 3] = 0;
            matrix[3, 4] = 0;
            matrix[3, 5] = 0;
            matrix[4, 0] = 0;
            matrix[4, 1] = 0;
            matrix[4, 2] = 1;
            matrix[4, 3] = 0;
            matrix[4, 4] = 0;
            matrix[4, 5] = 1;
            matrix[5, 0] = 1;
            matrix[5, 1] = 0;
            matrix[5, 2] = 1;
            matrix[5, 3] = 0;
            matrix[5, 4] = 1;
            matrix[5, 5] = 0;

            return matrix;
        }

        public static int[,] GetNormalMatrix()
        {
            int[,] matrix = new int[6, 6];
            matrix[0, 0] = 0;
            matrix[0, 1] = 1;
            matrix[0, 2] = 0;
            matrix[0, 3] = 0;
            matrix[0, 4] = 0;
            matrix[0, 5] = 1;
            matrix[1, 0] = 1;
            matrix[1, 1] = 0;
            matrix[1, 2] = 1;
            matrix[1, 3] = 0;
            matrix[1, 4] = 0;
            matrix[1, 5] = 0;
            matrix[2, 0] = 0;
            matrix[2, 1] = 1;
            matrix[2, 2] = 0;
            matrix[2, 3] = 1;
            matrix[2, 4] = 1;
            matrix[2, 5] = 0;
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 1;
            matrix[3, 3] = 0;
            matrix[3, 4] = 0;
            matrix[3, 5] = 0;
            matrix[4, 0] = 0;
            matrix[4, 1] = 0;
            matrix[4, 2] = 1;
            matrix[4, 3] = 0;
            matrix[4, 4] = 0;
            matrix[4, 5] = 1;
            matrix[5, 0] = 1;
            matrix[5, 1] = 0;
            matrix[5, 2] = 0;
            matrix[5, 3] = 0;
            matrix[5, 4] = 1;
            matrix[5, 5] = 0;

            return matrix;
        }

        public static int[,] GetReadySwitchMatrix()
        {
            int[,] matrix = new int[6, 6];
            matrix[0, 0] = 0;
            matrix[0, 1] = 1;
            matrix[0, 2] = 0;
            matrix[0, 3] = 0;
            matrix[0, 4] = 0;
            matrix[0, 5] = 1;
            matrix[1, 0] = 1;
            matrix[1, 1] = 0;
            matrix[1, 2] = 1;
            matrix[1, 3] = 0;
            matrix[1, 4] = 0;
            matrix[1, 5] = 0;
            matrix[2, 0] = 0;
            matrix[2, 1] = 1;
            matrix[2, 2] = 0;
            matrix[2, 3] = 1;
            matrix[2, 4] = 1;
            matrix[2, 5] = 1;
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 1;
            matrix[3, 3] = 0;
            matrix[3, 4] = 0;
            matrix[3, 5] = 0;
            matrix[4, 0] = 0;
            matrix[4, 1] = 0;
            matrix[4, 2] = 1;
            matrix[4, 3] = 0;
            matrix[4, 4] = 0;
            matrix[4, 5] = 1;
            matrix[5, 0] = 1;
            matrix[5, 1] = 0;
            matrix[5, 2] = 1;
            matrix[5, 3] = 0;
            matrix[5, 4] = 1;
            matrix[5, 5] = 0;

            return matrix;
        }
    }

}
