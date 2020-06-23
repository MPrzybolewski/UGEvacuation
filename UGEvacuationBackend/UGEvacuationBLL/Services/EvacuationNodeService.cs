using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGEvacuationBLL.Services.Interfaces;
using UGEvacuationCommon.Models;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationBLL.Services
{
    public class EvacuationNodeService: BaseService, IEvacuationNodeService
    {
        private readonly IEvacuationNodeRepository _evacuationNodeRepository;
        private readonly IEvacuationNodeAppUserRepository _evacuationNodeAppUserRepository;

        public EvacuationNodeService(IEvacuationNodeRepository evacuationNodeRepository, IEvacuationNodeAppUserRepository evacuationNodeAppUserRepository)
        {
            _evacuationNodeRepository = evacuationNodeRepository;
            _evacuationNodeAppUserRepository = evacuationNodeAppUserRepository;
        }

        public async Task CreateDefaultEvacuationNodes(Guid evacuationId)
        {
            try
            {
                var startNodesIds = Data.GetAllStartNodesIds();
                foreach (var startNodeId in startNodesIds)
                {
                    await _evacuationNodeRepository.Create(startNodeId, evacuationId);
                }
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - CreateDefaultEvacuationNodes");
            }
        }
        
        public async Task AddUserLocationToEvacuation(Guid evacuationId, int nodeId, Guid appUserId)
        {
            try
            {
                await AddDensity(evacuationId, nodeId);
                var evacuationNode = await GetByEvacuationIdAndNodeId(evacuationId, nodeId);
                await _evacuationNodeAppUserRepository.Create(appUserId, evacuationNode.Id);
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - AddUserLocationToEvacuation");
            }
        }

        public async Task<List<EvacuationNode>> SearchByEvacuationId(Guid evacuationId)
        {
            try
            {
                return await _evacuationNodeRepository.SearchByEvacuationId(evacuationId);
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - SearchByEvacuationId");
            }
        }

        public async Task AddDensity(Guid evacuationId, int nodeId)
        {
            try
            {
                await _evacuationNodeRepository.AddDensity(nodeId, evacuationId, 1);
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - AddDensity");
            }
        }
        
        public async Task<EvacuationNode> GetByEvacuationIdAndNodeId(Guid evacuationId, int nodeId)
        {
            try
            { 
                return await _evacuationNodeRepository.GetByEvacuationIdAndNodeId(nodeId, evacuationId);
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - GetByEvacuationIdAndNodeId");
            }
        }
    }
}