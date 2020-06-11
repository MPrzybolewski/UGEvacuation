using System;
using UGEvacuationBLL.Services.Interfaces;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationBLL.Services
{
    public class RegistrationService : BaseService, IRegistrationService
    {
        private readonly IAppUserRepository _appUserRepository;

        public RegistrationService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public void RegisterAppUser(string token)
        {
            try
            {
                _appUserRepository.Create(token);
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - Authenticate User");
            }
        }
    }
}