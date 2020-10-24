using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGv0
{
    /// <summary>
    /// Сетка узлов
    /// </summary>
    public class MeshOfNodes
    {
        private int[] _nodes;

        public int[] Nodes
        {
            get => _nodes;
            set => _nodes = value;
        }

        private int[] _indices;

        private Dictionary<int, int> _mesh;

        private Dictionary<int, int> _reversedMesh;

        public MeshOfNodes(int[] nodes)
        {
            _nodes = nodes;
            _indices = new int[_nodes.Length];

            _mesh = new Dictionary<int, int>();

            for (int i = 0; i < _nodes.Length; i++)
            {
                _mesh.Add(i, _nodes[i]);
            }

            _reversedMesh = _mesh.ToDictionary(
                kvp => kvp.Value,
                kvp => kvp.Key);

        }

        public int GetIndexByNode(int node)
        {
            return _reversedMesh[node];
        }

        public int GetNodeByIndex(int index)
        {
            return _mesh[index];
        }



    }
}
