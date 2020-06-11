using System;
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
        public IActionResult RegisterAppUser([FromBody] RegisterAppUserRequest registerAppUserRequest)
        {
            try
            {
                if (registerAppUserRequest == null)
                    return BadRequest();
                
                _registrationService.RegisterAppUser(registerAppUserRequest.Token);
                return Ok();
            }
            catch (Exception ex)
            {
                return GetStatusCodeFromException(ex);
            }
        }
    }
}