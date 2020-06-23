using System;
using System.Threading.Tasks;
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

        public async Task<Guid> RegisterAppUser(string token)
        {
            try
            {
                var appUser = await _appUserRepository.Create(token);
                return appUser.Id;
            }
            catch (Exception ex)
            {
                throw HandleExcpetion(ex, "Error - Authenticate User");
            }
        }
    }
}