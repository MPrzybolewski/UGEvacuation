using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IAdminUserRepository
    {
        AdminUser GetByUsername(string userName);
    }
}