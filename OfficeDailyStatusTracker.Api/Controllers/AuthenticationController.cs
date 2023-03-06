using Microsoft.AspNetCore.Mvc;
using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Logic.Services;

namespace Shop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Fields
        private readonly IAuthenticationService authenticationService;
        #endregion

        #region Constructors
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        #endregion

        #region Publics
        [HttpPost]
        [Route("UserLogin")]
        public IActionResult AdminLogin(AuthenticationModel authenticationModel)
        {
            ResponseModel data = authenticationService.UserLogin(authenticationModel);

            return Ok(data);
        }

        [HttpPost]
        [Route("UserRegister")]
        public IActionResult UserRegister(AuthenticationModel authenticationModel)
        {
            ResponseModel data = authenticationService.UserRegister(authenticationModel);

            return Ok(data);
        }
        #endregion
    }
}