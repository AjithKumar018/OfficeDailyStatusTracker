using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Web.Services;

namespace OfficeDailyStatusTracker.Web.Pages
{
    public class StatusTrackerBase : ComponentBase
    {
        #region Fields
        public bool bShowAddPopup;
        public bool bShowUpdatePopup;
        public bool bShowDeletePopup;
        #endregion

        #region Properties
        [CascadingParameter]
        public EventCallback EventNotify { get; set; }

        [Inject]
        public IStatusTrackerService? StatusTrackerService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public ProtectedSessionStorage? ProtectedSessionStorage { get; set; }

        public AuthenticationModel? UserAuthenticationModel { get; set; }
        public DailyStatusModel? DailyStatus { get; set; }
        public List<DailyStatusModel>? DailyStatusList { get; set; }
        public DailyStatusModel? DailyStatusToAdd { get; set; }
        public DailyStatusModel? DailyStatusToUpdate { get; set; }
        public DailyStatusModel? DailyStatusToDelete { get; set; }
        #endregion

        #region Protecteds
        protected override async Task OnInitializedAsync()
        {
            this.DailyStatus = new DailyStatusModel();
            await FetchAllRecords();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var userAuthenticationModel = await this.ProtectedSessionStorage!.GetAsync<AuthenticationModel>("userAuthenticationModel");
            this.UserAuthenticationModel = userAuthenticationModel.Success && this.UserAuthenticationModel == null ? userAuthenticationModel.Value : this.UserAuthenticationModel;

            if(firstRender)
            {
                await this.EventNotify.InvokeAsync();
            }

            await FetchAllRecords();

            if(userAuthenticationModel.Success) StateHasChanged();
            else this.NavigationManager!.NavigateTo("/login");
        }

        protected async Task AddDailyStatus()
        {
            this.DailyStatusToAdd!.UserId = int.Parse(this.UserAuthenticationModel?.UserKey!);
            await this.StatusTrackerService!.AddNewDailyStatus(this.DailyStatusToAdd!);
            ToggleAddPopup();

            this.DailyStatusToAdd = new DailyStatusModel();

            await FetchAllRecords();
        }

        protected async Task UpdateDailyStatus()
        {
            this.DailyStatusToUpdate!.UserId = int.Parse(this.UserAuthenticationModel?.UserKey!);
            await this.StatusTrackerService!.UpdateDailyStatus(this.DailyStatusToUpdate!);
            ToggleUpdatePopup();

            this.DailyStatusToUpdate = new DailyStatusModel();

            await FetchAllRecords();
        }

        protected async Task DeleteDailyStatus()
        {
            this.DailyStatusToDelete!.UserId = int.Parse(this.UserAuthenticationModel?.UserKey!);
            await this.StatusTrackerService!.DeleteDailyStatus(this.DailyStatusToDelete!.DailyStatusId);
            ToggleDeletePopup();

            this.DailyStatusToDelete = new DailyStatusModel();

            await FetchAllRecords();
        }

        protected void UpdateDailyStatusClicked(DailyStatusModel dailyStatus)
        {
            this.DailyStatusToUpdate = dailyStatus;
            ToggleUpdatePopup();
        }

        protected void DeleteDailyStatusClicked(DailyStatusModel dailyStatus)
        {
            this.DailyStatusToDelete = dailyStatus;
            ToggleDeletePopup();
        }

        protected void ToggleAddPopup()
        {
            this.DailyStatusToAdd = new DailyStatusModel
                                    {
                                        Date = DateTime.Now.ToString("yyyy-MM-dd"),
                                        Name = this.UserAuthenticationModel?.UserName
                                    };
            bShowAddPopup = bShowAddPopup != true;
        }

        protected void ToggleUpdatePopup()
        {
            bShowUpdatePopup = bShowUpdatePopup != true;
        }

        protected void ToggleDeletePopup()
        {
            bShowDeletePopup = bShowDeletePopup != true;
        }
        #endregion

        #region Privates
        private async Task FetchAllRecords()
        {
            if(this.UserAuthenticationModel == null) return;

            this.DailyStatusList = await this.StatusTrackerService!.GetAllRecords(int.Parse(this.UserAuthenticationModel?.UserKey!));
        }
        #endregion
    }
}