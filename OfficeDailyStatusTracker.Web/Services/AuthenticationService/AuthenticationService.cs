using Newtonsoft.Json;
using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly HttpClient httpClient;
        #endregion

        #region Constructors
        public AuthenticationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        #endregion

        #region Publics
        public async Task<ResponseModel?> UserLogin(AuthenticationModel? authenticationModel)
        {
            HttpResponseMessage result = await httpClient.PostAsJsonAsync("api/authentication/UserLogin", authenticationModel);
            string strResultContent = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseModel>(strResultContent);
        }

        public async Task<ResponseModel?> UserRegister(AuthenticationModel? authenticationModel)
        {
            HttpResponseMessage result = await httpClient.PostAsJsonAsync("api/authentication/UserRegister", authenticationModel);
            string strResultContent = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseModel>(strResultContent);
        }
        #endregion
    }
}