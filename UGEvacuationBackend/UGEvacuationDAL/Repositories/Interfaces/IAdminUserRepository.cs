using System.Threading.Tasks;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IAdminUserRepository
    {
        Task<AdminUser> GetByUsername(string userName);
    }
}