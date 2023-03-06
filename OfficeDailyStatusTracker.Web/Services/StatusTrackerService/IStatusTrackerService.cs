using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Web.Services
{
    public interface IStatusTrackerService
    {
        Task<List<DailyStatusModel>> GetAllRecords(int nAdminKey);
        Task<ResponseModel> AddNewDailyStatus(DailyStatusModel dailyStatus);
        Task<ResponseModel> UpdateDailyStatus(DailyStatusModel dailyStatus);
        Task<ResponseModel> DeleteDailyStatus(int nId);
    }
}
