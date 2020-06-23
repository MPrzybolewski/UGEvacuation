using System;
using System.Threading.Tasks;

namespace UGEvacuationBLL.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<Guid> RegisterAppUser(string token);
    }
}