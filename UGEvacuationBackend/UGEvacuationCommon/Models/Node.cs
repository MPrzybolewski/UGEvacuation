using System.Collections.Generic;

namespace UGEvacuationCommon.Models
{
    public class Node
    {
        public int Id;
        public bool IsExit;
        public bool IsStaircase;
        public int? Floor;
        public List<Node> AdjacentNodes = new List<Node>();

        public Node(int id, int? floor, bool isExit = false, bool isStaircase = false)
        {
            Id = id;
            Floor = floor;
            IsExit = isExit;
            IsStaircase = isStaircase;
        }

        public void AddAdjacentNodes(List<Node> nodes)
        {
            AdjacentNodes.AddRange(nodes);
        }
    }
}