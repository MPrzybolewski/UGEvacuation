using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGEvacuationDAL.Entities;

namespace UGEvacuationBLL.Services.Interfaces
{
    public interface IEvacuationNodeService
    {
        Task CreateDefaultEvacuationNodes(Guid evacuationId);
        Task<EvacuationNode> GetByEvacuationIdAndNodeId(Guid evacuationId, int nodeId);
        Task AddUserLocationToEvacuation(Guid evacuationId, int nodeId, Guid appUserId);
        Task<List<EvacuationNode>> SearchByEvacuationId(Guid evacuationId);
    }
}