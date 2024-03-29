﻿using OfficeDailyStatusTracker.Common.Models;

namespace OfficeDailyStatusTracker.Logic.Services
{
    public interface IStatusTracker
    {
        List<DailyStatusModel> GetAllRecords(int nAdminKey);
        ResponseModel AddNewDailyStatus(DailyStatusModel dailyStatus);
        ResponseModel UpdateDailyStatus(DailyStatusModel dailyStatus);
        ResponseModel DeleteDailyStatus(int nId);
    }
}
