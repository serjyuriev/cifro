using ASTRALib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class RastrCalc
    {
        private string _pathToSHABLON = $@"{ Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments) }\RastrWin3\" + 
            @"SHABLON\режим.rg2";
        
        private int _balancingNodeNumber;

        private Rastr _rastr = new Rastr();

        private table _nodeTable;
        private table _branchTable;

        //private Dictionary<string, bool> _normNodeStates;
        //private Dictionary<string, bool> _normBranchStates;

        private double _initialBalance;

        public bool SwitchingFlag { get; private set; }

        public void LoadNormRegime(string path)
        {
            LoadRegime(path);
            FindBalance();
        }

        public void LoadKZRegime(string path)
        {
            LoadRegime(path);
        }

        /// <summary>
        /// Загрузка файла режима
        /// </summary>
        /// <param name="path">Путь до файла rg2</param>
        private void LoadRegime(string path)
        {
            if (System.IO.File.Exists(path))
            {
                if (System.IO.File.Exists(_pathToSHABLON))
                {
                    _rastr.Load(RG_KOD.RG_REPL, path, _pathToSHABLON);
                    _nodeTable = (table)_rastr.Tables.Item("node");
                    _branchTable = (table)_rastr.Tables.Item("vetv");
                }
                else
                {
                    throw new System.IO.FileNotFoundException(
                        $"Файл { _pathToSHABLON } не существует!");
                }
            }
            else
            {
                throw new System.IO.FileNotFoundException(
                    $"Файл { path } не существует!");
            }
        }

        public void CalculateSwitching(IDictionary<string, bool> nodes,
            IDictionary<string, bool> branches)
        {
            var nodeVoltageDiv = _nodeTable.Cols.Item("otv");
            var nodeActivePower = _nodeTable.Cols.Item("pg");
            var nodeNumber = _nodeTable.Cols.Item("ny");

            ChangeState(nodes, branches);
            _rastr.rgm("");

            _nodeTable.SetSel("tip=1");
            var index = _nodeTable.FindNextSel[-1];
            
            while (index != -1)
            {
                if (nodeVoltageDiv.Z[index] > 10)
                {
                    Console.WriteLine($"Отклонение напряжения в узле { nodeNumber.Z[index] } больше 10%.");
                    SwitchingFlag = false;
                    return;
                }

                index = _nodeTable.FindNextSel[index];
            }

            _nodeTable.SetSel($"ny={ _balancingNodeNumber }");
            index = _nodeTable.FindNextSel[-1];

            if (nodeActivePower.Z[index] > _initialBalance + 10 ||
                nodeActivePower.Z[index] < _initialBalance - 10)
            {
                Console.WriteLine($"Баланс в исходной схеме: { _initialBalance } МВт,\n" +
                    $"Баланс в новой схеме: { nodeActivePower.Z[index] } МВт." +
                    $"\nОтклонение более 10 МВт.");
                SwitchingFlag = false;
                return;
            }

            SwitchingFlag = true;
        }

        public void SaveRegime()
        {
            _rastr.Save(@"D:\test.rg2", _pathToSHABLON);
        }

        /// <summary>
        /// Изменить состояние узлов и ветвей
        /// </summary>
        /// <param name="nodes">Список с номерами узлов</param>
        /// <param name="branches">Список с номерами ветвей
        /// в формате n-n</param>
        private void ChangeState(IDictionary<string, bool> nodes,
            IDictionary<string, bool> branches)
        {
            if (!(nodes is null))
            {
                // Меняем состояние всех указанных узлов
                var nodeState = (col)_nodeTable.Cols.Item("sta");

                foreach (string node in nodes?.Keys)
                {
                    _nodeTable.SetSel($"ny={ node }");
                    nodeState.Z[_nodeTable.FindNextSel[-1]] = 
                        nodes[node];
                }
            }

            if (!(branches is null))
            {
                // Меняем состояние всех указанных ветвей
                var branchState = (col)_branchTable.Cols.Item("sta");

                foreach (string branch in branches?.Keys)
                {
                    var splits = branch.Split('-');

                    _branchTable.SetSel($"ip={ splits[0] }&iq={ splits[1] }");
                    branchState.Z[_branchTable.FindNextSel[-1]] = 
                        branches[branch];
                }
            }
        }

        //private void FillNormalStates()
        //{
        //    var nodeNumber = (col)_nodeTable.Cols.Item("ny");
        //    var nodeState = (col)_nodeTable.Cols.Item("sta");

        //    _normNodeStates = new Dictionary<string, bool>();

        //    for (int i = 0; i < _nodeTable.Count; i++)
        //    {
        //        _normNodeStates.Add(nodeNumber.Z[i], nodeState.Z[i]);
        //    }

        //    var branchStart = (col)_branchTable.Cols.Item("ip");
        //    var branchEnd = (col)_branchTable.Cols.Item("iq");
        //    var branchState = (col)_nodeTable.Cols.Item("sta");

        //    _normBranchStates = new Dictionary<string, bool>();

        //    for (int i = 0; i < _branchTable.Count; i++)
        //    {
        //        _normBranchStates.Add($"{ branchStart.Z[i] }-" +
        //            $"{ branchEnd.Z[i] }", branchState.Z[i]);
        //    }
        //}

        private void FindBalance()
        {
            var nodeNumber = (col)_nodeTable.Cols.Item("ny");
            var nodeType = (col)_nodeTable.Cols.Item("tip");
            var nodeActivePower = (col)_nodeTable.Cols.Item("pg");

            for (int i = 0; i < _nodeTable.Count; i++)
            {
                if (nodeType.Z[i] == 0)
                {
                    _balancingNodeNumber = nodeNumber.Z[i];
                    _initialBalance = nodeActivePower.Z[i];
                }
            }
        }
    }
}
