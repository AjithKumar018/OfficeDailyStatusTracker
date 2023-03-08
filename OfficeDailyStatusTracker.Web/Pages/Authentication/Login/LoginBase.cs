using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Web.Services;

namespace OfficeDailyStatusTracker.Web.Pages
{
    public class LoginBase : ComponentBase
    {
        #region Properties
        public AuthenticationModel? AuthenticationModel { get; set; }
        public string AlertMessage { get; set; } = null!;

        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public ProtectedSessionStorage? ProtectedSessionStorage { get; set; }

        [CascadingParameter]
        public EventCallback EventNotify { get; set; }
        #endregion

        #region Protecteds
        protected override Task OnInitializedAsync()
        {
            this.AuthenticationModel = new AuthenticationModel();

            return base.OnInitializedAsync();
        }

        protected void NavigateToRegisterPage()
        {
            this.NavigationManager!.NavigateTo("/register");
        }

        protected async Task CheckLogin()
        {
            ResponseModel? response = await this.AuthenticationService!.UserLogin(this.AuthenticationModel);

            if(response!.StatusCode == 200)
            {
                string[] userResponse = response.StatusMessage!.Split("|");

                if(this.ProtectedSessionStorage != null)
                {
                    AuthenticationModel userAuthenticationModel = new()
                                                              {
                                                                  UserKey = userResponse[0],
                                                                  UserName = userResponse[1],
                                                                  Email = userResponse[2]
                                                              };
                    await this.ProtectedSessionStorage.SetAsync("userAuthenticationModel", userAuthenticationModel);
                }

                await this.EventNotify.InvokeAsync();

                this.NavigationManager?.NavigateTo("/");
            }
            else
            {
                this.AlertMessage = response.StatusMessage!;
            }
        }
        #endregion
    }
}