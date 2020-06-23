using System;

namespace UGEvacuationBackend.Models
{
    public class SendLocationDataRequest
    {
        public Guid EvacuationId { get; set; }
        public int NodeLocationId { get; set; }
        public Guid AppUserId { get; set; }
    }
}