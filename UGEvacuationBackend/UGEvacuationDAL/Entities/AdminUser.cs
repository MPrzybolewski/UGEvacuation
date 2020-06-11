using System;

namespace UGEvacuationDAL.Entities
{
    public class AdminUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDeleted { get; set; }
    }
}