using System;

namespace UGEvacuationDAL.Entities
{
    public class Evacuation
    {
        public Guid Id { get; set; }
        public string BlockedEdges { get; set; }
        public bool IsDeleted { get; set; }
    }
}