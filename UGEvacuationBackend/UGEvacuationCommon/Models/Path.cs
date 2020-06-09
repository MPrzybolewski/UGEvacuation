using System.Collections.Generic;
using System.Linq;

namespace UGEvacuationCommon.Models
{
    public class Path
    {
        public List<Node> NodesList;

        public bool HasEdge(Edge edge)
        {
            if (NodesList != null)
            {
                return GetEdges().Any(e => (e.From == edge.From || e.From == edge.To) && (e.To == edge.From || e.To == edge.To));
            }

            return false;
        }

        private List<Edge> GetEdges()
        {
            var edges = new List<Edge>();
            for (var i = 1; i < NodesList.Count; i++)
            {
                edges.Add(new Edge(NodesList[i - 1], NodesList[i]));
            }

            return edges;
        }
    }
}