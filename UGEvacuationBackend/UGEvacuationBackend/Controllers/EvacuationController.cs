using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGEvacuationBackend.Models;
using UGEvacuationBLL.Services.Interfaces;

namespace UGEvacuationBackend.Controllers
{
    [Route("api/evacuation")]
    [ApiController]
    public class EvacuationController : BaseController
    {
        private readonly INotificationsService _notificationsService;
        
        public EvacuationController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult EvacuationAlert([FromBody] EvacuationAlertRequest evacuationAlertRequest)
        {
            try
            {
                _notificationsService.SendLocationRequestNotifications(evacuationAlertRequest.BlockedEdges);
                return Ok();
            }
            catch (Exception ex)
            {
                return GetStatusCodeFromException(ex);
            }
        }
    }
}