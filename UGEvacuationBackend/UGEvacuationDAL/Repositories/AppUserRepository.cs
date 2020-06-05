using System.Linq;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UGEvacuationContext _dbContext;
        
        public AppUserRepository(UGEvacuationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AppUser GetByUsername(string userName)
        {
            var appUser = _dbContext.AppUsers.FirstOrDefault(u => u.Username == userName && u.IsDeleted == false);
            return appUser;
        }
    }
}