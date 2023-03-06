using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Data.Tables;

namespace OfficeDailyStatusTracker.Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly OfficeStatusTrackerContext _context;
        #endregion

        #region Constructors
        public AuthenticationService(OfficeStatusTrackerContext _context)
        {
            this._context = _context;
        }
        #endregion

        #region Publics
        public ResponseModel UserLogin(AuthenticationModel authenticationModel)
        {
            ResponseModel loginResponseModel = new();

            try
            {
                UserProfile userData = _context.UserProfiles.FirstOrDefault(user => user.Email == authenticationModel.Email)!;

                if(userData != null)
                {
                    if(userData.Password == authenticationModel.Password)
                    {
                        loginResponseModel.StatusCode = 200;
                        loginResponseModel.StatusMessage = $"{userData.UserId}|{userData.Name}|{userData.Email}";
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

        public ResponseModel UserRegister(AuthenticationModel authenticationModel)
        {
            ResponseModel loginResponseModel = new();

            try
            {
                UserProfile userProfile = new()
                                          {
                                              Name = authenticationModel.UserName!,
                                              Email = authenticationModel.Email!,
                                              Password = authenticationModel.Password!
                                          };

                _context.UserProfiles.Add(userProfile);
                _context.SaveChanges();

                loginResponseModel.StatusCode = 200;
                loginResponseModel.StatusMessage = "User Registered Successfully";
            }
            catch(Exception err)
            {
                loginResponseModel.StatusCode = 400;
                loginResponseModel.StatusMessage = $"User Registration Failed - {err.Message}";
            }

            return loginResponseModel;
        }
        #endregion
    }
}