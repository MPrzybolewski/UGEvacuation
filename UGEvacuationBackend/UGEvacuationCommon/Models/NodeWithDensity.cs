namespace UGEvacuationCommon.Models
{
    public class NodeWithDensity
    {
        public Node Node;
        public int Density;

        public NodeWithDensity(Node node, int density)
        {
            Node = node;
            Density = density;
        }
    }
}