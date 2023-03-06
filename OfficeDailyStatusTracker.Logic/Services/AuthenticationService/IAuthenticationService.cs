using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Logic.Services
{
    public interface IAuthenticationService
    {
        ResponseModel UserLogin(AuthenticationModel authenticationModel);
        ResponseModel UserRegister(AuthenticationModel authenticationModel);
    }
}