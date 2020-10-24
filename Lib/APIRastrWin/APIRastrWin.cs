using ASTRALib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Класс для взаимодействия с RastrWin3
    /// </summary>
    public class APIRastrWin
    {
        /// <summary>
        /// Интерфейс для взаимодейтсвия с RastrWin
        /// </summary>
        private IRastr _rastr = new Rastr();

        /// <summary>
        /// Загрузить файл режима в APIRastrWin
        /// </summary>
        /// <param name="FilePath">Путь к файлу формата *.rg2</param>
        public void LoadRegime(string FilePath)
        {
            _rastr.Load(RG_KOD.RG_REPL, FilePath, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Node> GetNodes()
        {
            var nodes = new List<Node>();
            ITable rastrNodes = _rastr.Tables.Item("node");
            string nodesCSVFilePath = Environment.CurrentDirectory + @"\tmp\nodes.csv";
            rastrNodes.WriteCSV(CSV_KOD.CSV_REPL, nodesCSVFilePath, "sta,ny,tip,name,uhom,nsx,pn,qn,pg,qg,vzd,qmin,qmax,bsh,vras,delta", ";");
            var parametrs = ReadCSVFile(nodesCSVFilePath);
            foreach (var item in parametrs)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i]=="" || item[i] == " ")
                    {
                        item[i] = "0";
                    }
                }
                nodes.Add(new Node(item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7], item[8], item[9], item[10], item[11], item[12], item[13], item[14], item[15]));
            }
            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Link> GetLinks()
        {
            var links = new List<Link>();
            ITable rastrLinks = _rastr.Tables.Item("vetv");
            string linksCSVFilePath = Environment.CurrentDirectory + @"\tmp\links.csv";
            rastrLinks.WriteCSV(CSV_KOD.CSV_REPL, linksCSVFilePath, "sta,tip,ip,iq,np,gropid,name,r,x,b,ktr,n_anc,bd,pl_ip,ql_ip,na,i_max,i_zaq", ";");
            var parametrs = ReadCSVFile(linksCSVFilePath);
            foreach (var item in parametrs)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == "" || item[i] == " ")
                    {
                        item[i] = "0";
                    }
                }
                links.Add(new Link(item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7], item[8], item[9], item[10], item[11], item[12], item[13], item[14], item[15], item[16], item[17]));
            }
            return links;
        }

        private List<string[]> ReadCSVFile(string path) 
        {
            List<string[]> parameters = new List<string[]>();  
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    parameters.Add(reader.ReadLine().Replace('.', ',').Split(';'));
                }
            }
            return parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckRegimeStatus() 
        {
           ITable ParamRgm = _rastr.Tables.Item("com_regim");
           ICol statusRgm = ParamRgm.Cols.Item("status");
           _rastr.rgm(""); 
           int status = statusRgm.get_ZN(0);
           if (status == 0)
               return true;
            else
               return false;
        }
    }
}
