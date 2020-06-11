using System.Collections;
using System.Collections.Generic;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        void Create(string token);
        IEnumerable<AppUser> GetAll();
    }
}