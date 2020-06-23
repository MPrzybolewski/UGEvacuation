using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UGEvacuationDAL.Entities;

namespace UGEvacuationDAL.Repositories.Interfaces
{
    public interface IEvacuationNodeRepository
    {
        Task Create(int nodeId, Guid evacuationId);
        Task<EvacuationNode> AddDensity(int nodeId, Guid evacuationId, int density);
        Task<EvacuationNode> GetByEvacuationIdAndNodeId(int nodeId, Guid evacuationId);
        Task<List<EvacuationNode>> SearchByEvacuationId(Guid evacuationId);
    }
}