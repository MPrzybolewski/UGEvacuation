using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser> Create(string token);
        Task<List<AppUser>> GetAll();
    }
}