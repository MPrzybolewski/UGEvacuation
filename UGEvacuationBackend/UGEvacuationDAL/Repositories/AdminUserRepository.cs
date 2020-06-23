using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {

        public async Task<AdminUser> GetByUsername(string userName)
        {
            using (var context = new UGEvacuationContext())
            {
                var appUser =
                    await context.AdminUsers.FirstOrDefaultAsync(u =>
                        u.Username == userName && u.IsDeleted == false);
                return appUser;
            }
        }
    }
}