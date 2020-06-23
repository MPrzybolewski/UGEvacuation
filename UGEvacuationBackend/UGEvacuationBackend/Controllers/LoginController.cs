using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGEvacuationBackend.Models;
using UGEvacuationBLL.Models;
using UGEvacuationBLL.Services.Interfaces;

namespace UGEvacuationBackend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var login = new Login()
                {
                    Username = loginRequest.Username,
                    Password = loginRequest.Password
                };
                var authenticatedUser = await _loginService.AuthenticateUser(login);

                if (authenticatedUser != null)
                {
                    return Ok(authenticatedUser);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return GetStatusCodeFromException(ex);
            }
        }
    }
}