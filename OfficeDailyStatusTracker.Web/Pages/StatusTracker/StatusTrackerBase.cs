using Microsoft.AspNetCore.Components;
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
        [Inject]
        public IStatusTrackerService? StatusTrackerService { get; set; }

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

        protected async Task AddDailyStatus()
        {
            ResponseModel response = await this.StatusTrackerService!.AddNewDailyStatus(this.DailyStatusToAdd!);
            ToggleAddPopup();

            this.DailyStatusToAdd = new DailyStatusModel();

            await FetchAllRecords();
        }

        protected async Task UpdateDailyStatus()
        {
            ResponseModel response = await this.StatusTrackerService!.UpdateDailyStatus(this.DailyStatusToUpdate!);
            ToggleUpdatePopup();

            this.DailyStatusToUpdate = new DailyStatusModel();

            await FetchAllRecords();
        }

        protected async Task DeleteDailyStatus()
        {
            ResponseModel response = await this.StatusTrackerService!.DeleteDailyStatus(this.DailyStatusToDelete!.Id);
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
            this.DailyStatusToAdd = new DailyStatusModel()
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
            this.DailyStatusList = await this.StatusTrackerService!.GetAllRecords();
        }
        #endregion
    }
}