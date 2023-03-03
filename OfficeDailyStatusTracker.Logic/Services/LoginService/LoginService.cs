using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Data.Tables;

namespace OfficeDailyStatusTracker.Logic.Services
{
    public class LoginService : ILoginService
    {
        #region Fields
        private readonly OfficeStatusTrackerContext _context;
        #endregion

        #region Constructors
        public LoginService(OfficeStatusTrackerContext _context)
        {
            this._context = _context;
        }
        #endregion

        #region Publics
        public ResponseModel UserLogin(LoginModel loginModel)
        {
            ResponseModel loginResponseModel = new();

            try
            {
                UserProfile userData = _context.UserProfiles.FirstOrDefault(user => user.Email == loginModel.Email)!;

                if(userData != null)
                {
                    if(userData.Password == loginModel.Password)
                    {
                        loginResponseModel.StatusCode = 200;
                        loginResponseModel.StatusMessage = $"{userData.Id}|{userData.Name}|{userData.Email}";
                    }
                    else
                    {
                        loginResponseModel.StatusCode = 301;
                        loginResponseModel.StatusMessage = "Please check your credentials.";
                    }
                }
                else
                {
                    loginResponseModel.StatusCode = 301;
                    loginResponseModel.StatusMessage = "You are not registered with us.";
                }
            }
            catch(Exception err)
            {
                loginResponseModel.StatusCode = 400;
                loginResponseModel.StatusMessage = $"An error occured. - {err.Message}";
            }

            return loginResponseModel;
        }
        #endregion
    }
}