using Newtonsoft.Json;
using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Web.Services
{
    public class LoginService : ILoginService
    {
        #region Fields
        private readonly HttpClient httpClient;
        #endregion

        #region Constructors
        public LoginService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        #endregion

        #region Publics
        public async Task<ResponseModel?> UserLogin(LoginModel? loginModel)
        {
            HttpResponseMessage? result = await httpClient.PostAsJsonAsync("api/login/UserLogin", loginModel);
            string strResultContent = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseModel>(strResultContent);
        }
        #endregion
    }
}