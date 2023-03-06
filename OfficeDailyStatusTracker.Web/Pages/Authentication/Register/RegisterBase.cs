using Microsoft.AspNetCore.Components;
using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Web.Services;

namespace OfficeDailyStatusTracker.Web.Pages
{
    public class RegisterBase : ComponentBase
    {
        #region Properties
        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public AuthenticationModel? AuthenticationModel { get; set; }
        public string AlertMessage { get; set; } = null!;
        #endregion

        #region Protecteds
        protected override Task OnInitializedAsync()
        {
            this.AuthenticationModel = new AuthenticationModel();

            return base.OnInitializedAsync();
        }

        protected async Task RegisterUserAsync()
        {
            ResponseModel? response = await this.AuthenticationService!.UserRegister(this.AuthenticationModel);

            if(response?.StatusCode == 200)
            {
                this.NavigationManager?.NavigateTo("/login");
            }
            else this.AlertMessage = response?.StatusMessage!;
        }
        #endregion
    }
}