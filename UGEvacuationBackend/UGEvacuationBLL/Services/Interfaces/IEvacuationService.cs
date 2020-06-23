using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGEvacuationCommon.Models;
using UGEvacuationDAL.Entities;

namespace UGEvacuationBLL.Services.Interfaces
{
    public interface IEvacuationService
    {
        Task<Guid> CreateEvacuation(List<EdgeTemplate> blockedEdges);
        Task CreateBestPaths(Guid evacuationId);
    }
}