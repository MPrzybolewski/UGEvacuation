using System;
using System.Threading.Tasks;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IEvacuationRepository
    {
        Task<Evacuation> Get(Guid id);
        Task<Evacuation> Create(string blockedEdgesString);
    }
}