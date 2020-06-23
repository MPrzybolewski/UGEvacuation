using System.Collections.Generic;
using System.Linq;

namespace UGEvacuationCommon.Models
{
    public class Bucket
    {
        public int TimeInstant;
        public List<BucketElement> BucketElements = new List<BucketElement>();

        public void AddEdgeOrDensity(Edge edge, int density)
        {
            if (BucketElements.Any(be => be.Edge == edge))
            {
                var bucketElement = BucketElements.First(be => be.Edge == edge);
                bucketElement.Density += 0;
            }
            else
            {
                BucketElements.Add(new BucketElement(edge, density));
            }
        }
    }
}