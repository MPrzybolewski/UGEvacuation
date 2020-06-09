namespace UGEvacuationCommon.Models
{
    public class BucketElement
    {
        public Edge Edge;
        public int Density;

        public BucketElement(Edge edge, int density)
        {
            Edge = edge;
            Density = density;
        }
    }
}