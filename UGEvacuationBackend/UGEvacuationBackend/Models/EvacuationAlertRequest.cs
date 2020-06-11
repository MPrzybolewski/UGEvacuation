using System.Collections.Generic;
using UGEvacuationCommon.Models;

namespace UGEvacuationBackend.Models
{
    public class EvacuationAlertRequest
    {
        public List<EdgeTemplate> BlockedEdges { get; set; }
    }
}