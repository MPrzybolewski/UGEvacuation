using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGEvacuationCommon.Models;

namespace UGEvacuationBLL.Services.Interfaces
{
    public interface INotificationsService
    {
        Task SendLocationRequestNotifications(Guid evacuationId, List<string> tokens);
        Task SendBestPathNotifications(List<string> tokens, string bestPathString);
    }
}