using System;

namespace UGEvacuationDAL.Entities
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public bool IsDeleted { get; set; }
    }
}