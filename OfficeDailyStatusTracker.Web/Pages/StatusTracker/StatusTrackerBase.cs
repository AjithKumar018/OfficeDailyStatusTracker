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
        public int nAdminKey = -1;
        #endregion

        #region Properties
        [Inject]
        public IStatusTrackerService? StatusTrackerService { get; set; }

        [CascadingParameter]
        public EventCallback EventNotify { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public ProtectedSessionStorage? ProtectedSessionStorage { get; set; }

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
            var strAdminSession = await this.ProtectedSessionStorage!.GetAsync<string>("AdminKey");
            nAdminKey = strAdminSession.Success && nAdminKey == -1 ? int.Parse(strAdminSession.Value!) : nAdminKey;

            if(firstRender)
            {
                await this.EventNotify.InvokeAsync();
            }

            await FetchAllRecords();

            if(strAdminSession.Success) StateHasChanged();
            else this.NavigationManager!.NavigateTo("/login");
        }

        protected async Task AddDailyStatus()
        {
            this.DailyStatusToAdd!.UserId = nAdminKey;
            await this.StatusTrackerService!.AddNewDailyStatus(this.DailyStatusToAdd!);
            ToggleAddPopup();

            this.DailyStatusToAdd = new DailyStatusModel();

            await FetchAllRecords();
        }

        protected async Task UpdateDailyStatus()
        {
            this.DailyStatusToUpdate!.UserId = nAdminKey;
            await this.StatusTrackerService!.UpdateDailyStatus(this.DailyStatusToUpdate!);
            ToggleUpdatePopup();

            this.DailyStatusToUpdate = new DailyStatusModel();

            await FetchAllRecords();
        }

        protected async Task DeleteDailyStatus()
        {
            this.DailyStatusToDelete!.UserId = nAdminKey;
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
                                        Name = "Mani Raj"
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
            this.DailyStatusList = await this.StatusTrackerService!.GetAllRecords(nAdminKey);
        }
        #endregion
    }
}