using System;

namespace UGEvacuationBLL.Models
{
    public class AuthenticatedUser
    {
        public Guid AppUserId { get; set; }
        public string Token { get; set; }
    }
}