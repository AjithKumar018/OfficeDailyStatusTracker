using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Web.Services;

namespace OfficeDailyStatusTracker.Web.Pages
{
    public class LoginBase : ComponentBase
    {
        #region Properties
        public LoginModel? LoginModel { get; set; }
        public string AlertMessage { get; set; } = null!;

        [Inject]
        public ILoginService? LoginService { get; set; }

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
            this.LoginModel = new LoginModel();

            return base.OnInitializedAsync();
        }

        protected async Task CheckLogin()
        {
            ResponseModel? response = await this.LoginService!.UserLogin(this.LoginModel);

            if(response!.StatusCode == 200)
            {
                string[] userResponse = response.StatusMessage!.Split("|");

                if(this.ProtectedSessionStorage != null)
                {
                    await this.ProtectedSessionStorage.SetAsync("AdminKey", userResponse[0]);
                    await this.ProtectedSessionStorage.SetAsync("AdminName", userResponse[1]);
                    await this.ProtectedSessionStorage.SetAsync("AdminEmail", userResponse[2]);
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