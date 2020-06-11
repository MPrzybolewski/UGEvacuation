using System.Collections.Generic;
using UGEvacuationCommon.Models;

namespace UGEvacuationBLL.Services.Interfaces
{
    public interface INotificationsService
    {
        void SendLocationRequestNotifications(List<EdgeTemplate> blockedEdges);
    }
}