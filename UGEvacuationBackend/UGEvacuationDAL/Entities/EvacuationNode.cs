using System;

namespace UGEvacuationDAL.Entities
{
    public class EvacuationNode
    {
        public Guid Id { get; set; }
        public int NodeId { get; set; }
        public Guid EvacuationId { get; set; }
        public int Density { get; set; }
        public bool IsDeleted { get; set; }
    }
}