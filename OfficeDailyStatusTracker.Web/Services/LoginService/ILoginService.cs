using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Web.Services
{
    public interface ILoginService
    {
        Task<ResponseModel?> UserLogin(LoginModel? loginModel);
    }
}