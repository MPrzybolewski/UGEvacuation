using System.Threading.Tasks;
using UGEvacuationBLL.Models;

namespace UGEvacuationBLL.Services.Interfaces
{
    public interface ILoginService
    {
        Task<AuthenticatedUser> AuthenticateUser(Login login);
    }
}