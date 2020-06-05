using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UGEvacuationBLL.Helpers;
using UGEvacuationBLL.Models;
using UGEvacuationBLL.Services.Interfaces;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationBLL.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppSettings _appSettings;
        
        public LoginService(IAppUserRepository appUserRepository, IAppSettings appSettings)
        {
            _appUserRepository = appUserRepository;
            _appSettings = appSettings;
        }
        public AuthenticatedUser AuthenticateUser(Login login)
        {
            try
            {
                var appUser = _appUserRepository.GetByUsername(login.Username);

                if (appUser == null)
                    return null;

                var inputPasswordHash = Encryption.CalculateSHA1Hash(login.Password);

                if (inputPasswordHash != appUser.PasswordHash)
                    throw new UGEvacuationException("Error - Wrong password", type: ErrorType.AccessDenied);

                var authenticatedUser = new AuthenticatedUser()
                {
                    AppUserId = appUser.Id,
                    Token = GenerateJsonWebToken()
                };

                return authenticatedUser;
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - Authenticate User");
            }
        }
        
        private string GenerateJsonWebToken()    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var token = new JwtSecurityToken(_appSettings.Jwt.Issuer,    
                _appSettings.Jwt.Issuer,    
                null,    
                expires: DateTime.Now.AddMinutes(120),    
                signingCredentials: credentials);    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }
    }
}