using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGEvacuationBLL.Helpers;
using UGEvacuationBLL.Services.Interfaces;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationBLL.Services
{
    public class EvacuationService : BaseService, IEvacuationService
    {
        private readonly IEvacuationRepository _evacuationRepository;
        private readonly INotificationsService _notificationsService;
        private readonly IEvacuationNodeService _evacuationNodeService;
        private readonly IEvacuationNodeAppUserRepository _evacuationNodeAppUserRepository;
        private readonly IAppUserRepository _appUserRepository;

        public EvacuationService(IEvacuationRepository evacuationRepository, INotificationsService notificationsService, IEvacuationNodeService evacuationNodeService, IEvacuationNodeAppUserRepository evacuationNodeAppUserRepository, IAppUserRepository appUserRepository)
        {
            _evacuationRepository = evacuationRepository;
            _notificationsService = notificationsService;
            _evacuationNodeService = evacuationNodeService;
            _evacuationNodeAppUserRepository = evacuationNodeAppUserRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task<Guid> CreateEvacuation(List<EdgeTemplate> blockedEdges)
        {
            try
            {
                var data = new Data();
                var graph = data.GenerateGraph(blockedEdges);

                CheckIfThereIsPathForEveryNode(graph);
                var blockedEdgesString = EdgesConverter.GetBlockedEdgesString(blockedEdges);
                var evacuation = await _evacuationRepository.Create(blockedEdgesString);
                await _evacuationNodeService.CreateDefaultEvacuationNodes(evacuation.Id);

                var allAppUsers = await _appUserRepository.GetAll();
                
                await _notificationsService.SendLocationRequestNotifications(evacuation.Id, allAppUsers.Select(a => a.Token).ToList());
                return evacuation.Id;
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - CreateEvacuation");
            }
        }

        public async Task CreateBestPaths(Guid evacuationId)
        {
            try
            {
                var evacuation = await _evacuationRepository.Get(evacuationId);
                if (evacuation == null)
                    throw new UGEvacuationException(message: "CreateBestPaths - evacuation not exists", type: ErrorType.InvalidArgument);

                var blockedEdges = EdgesConverter.GetListByString(evacuation.BlockedEdges);
                var data = new Data();
                var graph = data.GenerateGraph(blockedEdges);

                var startNodesIds =
                    (await _evacuationNodeService.SearchByEvacuationId(evacuation.Id)).Where(en => en.Density > 0).ToList();
                
                if (!startNodesIds.Any())
                    return;
                
                var result = PathManager.GetBestPathForNodesIds(graph, startNodesIds.ToDictionary(x => x.NodeId, y => y.Density));

                foreach (var node in startNodesIds)
                {
                    var appUsersToNotify = (await _evacuationNodeAppUserRepository.SearchByEvacuationIdAndNodeId(evacuationId, node.NodeId)).Select(e => e.AppUser);
                    var bestPathString = string.Join(',', result[node.NodeId].NodesList.Select(n => n.Id.ToString()));
                    
                    await _notificationsService.SendBestPathNotifications(appUsersToNotify.Select(a => a.Token).ToList(), bestPathString);
                }
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - CreateBestPaths");
            }

        }

        private void CheckIfThereIsPathForEveryNode(List<Node> graph)
        {
            var startNodeIds = Data.GetAllStartNodesIds();

            var result = PathManager.GetBestPathForNodesIds(graph, startNodeIds.ToDictionary(x => x, y => 0));

            var paths = result.Values.ToList();

            if (paths.FirstOrDefault(p => p.NodesList == null || p.NodesList.Count == 0) != null)
            {
                throw new UGEvacuationException(message: "CheckIfThereIsPathForEveryNode - there is node without any path", type: ErrorType.NoPath);
            }
        }
    }
}