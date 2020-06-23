using System;

namespace UGEvacuationDAL.Entities
{
    public class EvacuationNodeAppUser
    {
        public Guid Id { get; set; }
        public Guid EvacuationNodeId { get; set; }
        public Guid AppUserId { get; set; }
        public EvacuationNode EvacuationNode { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}