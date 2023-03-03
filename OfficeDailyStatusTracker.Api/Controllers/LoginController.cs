using Microsoft.AspNetCore.Mvc;
using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Logic.Services;

namespace Shop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Fields
        private readonly ILoginService loginService;
        #endregion

        #region Constructors
        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }
        #endregion

        #region Publics
        [HttpPost]
        [Route("UserLogin")]
        public IActionResult AdminLogin(LoginModel loginModel)
        {
            ResponseModel data = loginService.UserLogin(loginModel);

            return Ok(data);
        }
        #endregion
    }
}