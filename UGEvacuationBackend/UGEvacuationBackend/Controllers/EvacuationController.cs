using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IEvacuationService _evacuationService;
        private readonly IEvacuationNodeService _evacuationNodeService;
        
        public EvacuationController(IEvacuationService evacuationService, IEvacuationNodeService evacuationNodeService)
        {
            _evacuationService = evacuationService;
            _evacuationNodeService = evacuationNodeService;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EvacuationAlert([FromBody] EvacuationAlertRequest evacuationAlertRequest)
        {
            try
            {
                var evacuationId = await _evacuationService.CreateEvacuation(evacuationAlertRequest.BlockedEdges);
                return Ok(evacuationId);
            }
            catch (Exception ex)
            {
                return GetStatusCodeFromException(ex);
            }
        }
        
        [HttpPost]
        [Route("generatePaths")]
        [Authorize]
        public async Task<IActionResult> EvacuationAlertGeneratePaths([FromBody] EvacuationAlertGeneratePathsRequest evacuationAlertGeneratePathsRequest)
        {
            try
            {
                await _evacuationService.CreateBestPaths(evacuationAlertGeneratePathsRequest.EvacuationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return GetStatusCodeFromException(ex);
            }
        }

        [HttpPost]
        [Route("sendLocationData")]
        [AllowAnonymous]
        public async Task<IActionResult> SendLocationData([FromBody] SendLocationDataRequest sendLocationDataRequest)
        {
            try
            {
                await _evacuationNodeService.AddUserLocationToEvacuation(sendLocationDataRequest.EvacuationId,
                    sendLocationDataRequest.NodeLocationId, sendLocationDataRequest.AppUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return GetStatusCodeFromException(ex);
            }
        }
    }
}