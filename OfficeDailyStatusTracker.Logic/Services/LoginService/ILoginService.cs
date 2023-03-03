using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Logic.Services
{
    public interface ILoginService
    {
        ResponseModel UserLogin(LoginModel loginModel);
    }
}