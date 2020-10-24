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

        private Rastr _rastr = new Rastr();

        /// <summary>
        /// Загрузка файла режима
        /// </summary>
        /// <param name="path">Путь до файла rg2</param>
        public void LoadRegime(string path)
        {
            if (System.IO.File.Exists(path))
            {
                if (System.IO.File.Exists(_pathToSHABLON))
                {
                    _rastr.Load(RG_KOD.RG_REPL, path, _pathToSHABLON);
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
        public void ChangeState(IDictionary<string, bool> nodes, 
            IDictionary<string, bool> branches)
        {
            // Меняем состояние всех указанных узлов
            var nodeTable = (table)_rastr.Tables.Item("node");
            var nodeState = (col)nodeTable.Cols.Item("sta");

            foreach (string node in nodes.Keys)
            {
                nodeTable.SetSel($"ny={ node }");
                nodeState.Z[nodeTable.FindNextSel[-1]] = 
                    nodes[node];
            }

            // Меняем состояние всех указанных ветвей
            var branchTable = (table)_rastr.Tables.Item("vetv");
            var branchState = (col)branchTable.Cols.Item("sta");

            foreach (string branch in branches.Keys)
            {
                var splits = branch.Split('-');

                branchTable.SetSel($"ip={ splits[0] }&iq={ splits[1] }");
                branchState.Z[branchTable.FindNextSel[-1]] = 
                    branches[branch];
            }
        }
    }
}
