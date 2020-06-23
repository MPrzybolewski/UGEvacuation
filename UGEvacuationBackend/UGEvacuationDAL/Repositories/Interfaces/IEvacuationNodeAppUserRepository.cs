using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IEvacuationNodeAppUserRepository
    {
        Task<EvacuationNodeAppUser> Create(Guid appUserId, Guid evacuationNodeId);
        Task<IEnumerable<EvacuationNodeAppUser>> SearchByEvacuationIdAndNodeId(Guid evacuationId, int nodeId);
    }
}