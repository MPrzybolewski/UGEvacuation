using System.Collections.Generic;
using System.Linq;

namespace UGEvacuationCommon.Models
{
    public class Bucket
    {
        public int TimeInstant;
        public List<BucketElement> BucketElements = new List<BucketElement>();

        public void AddEdgeOrDensity(Edge edge)
        {
            if (BucketElements.Any(be => be.Edge == edge))
            {
                var bucketElement = BucketElements.First(be => be.Edge == edge);
                bucketElement.Density += 30;
            }
            else
            {
                BucketElements.Add(new BucketElement(edge, 10));
            }
        }
    }
}