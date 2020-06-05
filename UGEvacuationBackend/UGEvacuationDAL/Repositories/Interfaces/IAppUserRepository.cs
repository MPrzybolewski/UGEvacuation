using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        AppUser GetByUsername(string userName);
    }
}