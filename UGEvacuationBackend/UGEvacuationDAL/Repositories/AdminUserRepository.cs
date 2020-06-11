using System.Linq;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly UGEvacuationContext _dbContext;
        
        public AdminUserRepository(UGEvacuationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AdminUser GetByUsername(string userName)
        {
            var appUser = _dbContext.AdminUsers.FirstOrDefault(u => u.Username == userName && u.IsDeleted == false);
            return appUser;
        }
    }
}