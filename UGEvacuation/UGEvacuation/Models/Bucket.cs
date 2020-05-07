using System.Collections.Generic;
using System.Linq;

namespace UGEvacuation.Models
{
    public class Bucket
    {
        public int TimeInstant;
        public List<BucketElement> BucketElements;

        public void AddEdgeOrDensity(Edge edge)
        {
            if (BucketElements != null)
            {
                if (BucketElements.Any(be => be.Edge != edge))
                {
                    var bucketElement = BucketElements.First(be => be.Edge == edge);
                    bucketElement.Density += 10;
                }
                else
                {
                    BucketElements.Add(new BucketElement(edge, 10));
                }
            }
        }
    }
}