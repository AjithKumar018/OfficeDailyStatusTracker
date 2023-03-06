using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Web.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseModel?> UserLogin(AuthenticationModel? authenticationModel);
        Task<ResponseModel?> UserRegister(AuthenticationModel? authenticationModel);
    }
}