using Newtonsoft.Json;
using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Web.Services
{
    public class StatusTrackerService : IStatusTrackerService
    {
        #region Fields
        private readonly HttpClient httpClient;
        #endregion

        #region Constructors
        public StatusTrackerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        #endregion

        #region Publics
        public async Task<List<DailyStatusModel>> GetAllRecords()
        {
            return (await httpClient.GetFromJsonAsync<List<DailyStatusModel?>>("api/StatusTracker/GetAllRecords"))!;
        }

        public async Task<ResponseModel> AddNewDailyStatus(DailyStatusModel dailyStatus)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/StatusTracker/AddNewDailyStatus", dailyStatus);
            string strResultContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseModel?>(strResultContent)!;
        }

        public async Task<ResponseModel> UpdateDailyStatus(DailyStatusModel dailyStatus)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("api/StatusTracker/UpdateDailyStatus", dailyStatus);
            string strResultContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseModel?>(strResultContent)!;
        }

        public async Task<ResponseModel> DeleteDailyStatus(int nId)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"api/StatusTracker/DeleteDailyStatus/{nId}");
            string strResultContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseModel?>(strResultContent)!;
        }
        #endregion
    }
}