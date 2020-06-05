using UGEvacuationBLL.Models;

namespace UGEvacuationBLL.Services.Interfaces
{
    public interface ILoginService
    {
        AuthenticatedUser AuthenticateUser(Login login);
    }
}