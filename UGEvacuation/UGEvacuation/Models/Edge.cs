namespace UGEvacuation.Models
{
    public class Edge
    {
        public Node From;
        public Node To;

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }
        
        public static bool operator ==(Edge a, Edge b)
        {
            if (a.From == b.From && a.To == b.To)
            {
                return true;
            }

            if (a.From == b.To && a.To == b.From)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Edge a, Edge b)
        {
            return !(a == b);
        }
    }
}