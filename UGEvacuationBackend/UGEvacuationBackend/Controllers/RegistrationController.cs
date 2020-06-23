using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGEvacuationBackend.Models;
using UGEvacuationBLL.Services.Interfaces;

namespace UGEvacuationBackend.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationService _registrationService;
        
        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAppUser([FromBody] RegisterAppUserRequest registerAppUserRequest)
        {
            try
            {
                if (registerAppUserRequest == null)
                    return BadRequest();
                
                var appUserId =  await _registrationService.RegisterAppUser(registerAppUserRequest.Token);
                return Ok(appUserId);
            }
            catch (Exception ex)
            {
                return GetStatusCodeFromException(ex);
            }
        }
    }
}