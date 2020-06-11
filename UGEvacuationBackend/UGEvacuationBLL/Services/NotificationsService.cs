using System;
using System.Collections.Generic;
using System.Linq;
using UGEvacuationBLL.Helpers;
using UGEvacuationBLL.Services.Interfaces;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;

namespace UGEvacuationBLL.Services
{
    public class NotificationService : BaseService, INotificationsService
    {
        public void SendLocationRequestNotifications(List<EdgeTemplate> blockedEdges)
        {
            try
            {
                var data = new Data();
                var graph = data.GenerateGraph(blockedEdges);

                CheckIfThereIsPathForEveryNode(graph);
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - Send Location Request Notifications");
            }
        }

        private void CheckIfThereIsPathForEveryNode(List<Node> graph)
        {
            var startNodeIds = Data.GetAllStartNodesIds();

            var result = PathManager.GetBestPathForNodesIds(graph, startNodeIds);

            var paths = result.Values.ToList();

            if (paths.FirstOrDefault(p => p.NodesList == null || p.NodesList.Count == 0) != null)
            {
                throw new UGEvacuationException(message: "CheckIfThereIsPathForEveryNode - there is node without any path", type: ErrorType.NoPath);
            }
        }
    }
}