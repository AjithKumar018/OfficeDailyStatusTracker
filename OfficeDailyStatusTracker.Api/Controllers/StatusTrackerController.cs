using Microsoft.AspNetCore.Mvc;
using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Logic.Services;

namespace OfficeDailyStatusTracker.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTrackerController : ControllerBase
    {
        #region Fields
        private readonly IStatusTracker statusTracker;
        #endregion

        #region Constructors
        public StatusTrackerController(IStatusTracker statusTracker)
        {
            this.statusTracker = statusTracker;
        }
        #endregion

        #region Publics
        [HttpGet]
        [Route("GetAllRecords/{nAdminKey}")]
        public IActionResult GetAllRecords(int nAdminKey)
        {
            var result = statusTracker.GetAllRecords(nAdminKey);

            return Ok(result);
        }

        [HttpPost]
        [Route("AddNewDailyStatus")]
        public IActionResult AddNewDailyStatus(DailyStatusModel dailyStatus)
        {
            var result = statusTracker.AddNewDailyStatus(dailyStatus);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateDailyStatus")]
        public IActionResult UpdateDailyStatus(DailyStatusModel dailyStatus)
        {
            var result = statusTracker.UpdateDailyStatus(dailyStatus);

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteDailyStatus/{nId}")]
        public IActionResult DeleteDailyStatus(int nId)
        {
            var result = statusTracker.DeleteDailyStatus(nId);

            return Ok(result);
        }
        #endregion
    }
}