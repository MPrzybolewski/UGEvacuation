using System.Collections.Generic;

namespace UGEvacuation.Models
{
    public class Path
    {
        public List<Node> NodesList;
        public bool IsRejected;

        public bool HasEdge(Edge edge)
        {
            if (NodesList != null)
            {
                for (var i = 1; i < NodesList.Count; i++)
                {
                    var tempEdge = new Edge(NodesList[i-1], NodesList[i]);
                    if (tempEdge == edge)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}