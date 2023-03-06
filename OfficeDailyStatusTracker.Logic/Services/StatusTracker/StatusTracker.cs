using OfficeDailyStatusTracker.Common.Models;
using OfficeDailyStatusTracker.Data.Tables;

namespace OfficeDailyStatusTracker.Logic.Services
{
    public class StatusTracker : IStatusTracker
    {
        #region Fields
        private readonly OfficeStatusTrackerContext _context;
        #endregion

        #region Constructors
        public StatusTracker(OfficeStatusTrackerContext _context)
        {
            this._context = _context;
        }
        #endregion

        #region Publics
        public List<DailyStatusModel> GetAllRecords(int nAdminKey)
        {
            List<DailyStatus> lstDailyStatus = _context.DailyStatuses.Where(dailyStatus => dailyStatus.UserId == nAdminKey).ToList();

            return lstDailyStatus.Select(dailyStatus => new DailyStatusModel()
                                                        {
                                                            DailyStatusId = dailyStatus.DailyStatusId,
                                                            Date = dailyStatus.Date,
                                                            Name = dailyStatus.Name,
                                                            Project = dailyStatus.Project,
                                                            Task = dailyStatus.Task,
                                                            Description = dailyStatus.Description,
                                                            ActualTime = dailyStatus.ActualTime,
                                                            Category = dailyStatus.Category
                                                        }).ToList();
        }

        public ResponseModel AddNewDailyStatus(DailyStatusModel dailyStatus)
        {
            ResponseModel response;

            try
            {
                var dailyStatusNeedToAdd = new DailyStatus()
                                           {
                                               UserId = dailyStatus.UserId,
                                               DailyStatusId = _context.DailyStatuses.ToList().Count + 1,
                                               Date = dailyStatus.Date,
                                               Name = dailyStatus.Name,
                                               Project = dailyStatus.Project,
                                               Task = dailyStatus.Task,
                                               Description = dailyStatus.Description,
                                               ActualTime = dailyStatus.ActualTime,
                                               Category = dailyStatus.Category
                                           };
                _context.Add(dailyStatusNeedToAdd);
                _context.SaveChanges();

                response = new ResponseModel
                           {
                               StatusCode = 200,
                               StatusMessage = "Daily Status Added Successfully."
                           };
            }
            catch(Exception err)
            {
                response = new ResponseModel
                           {
                               StatusCode = 400,
                               StatusMessage = $"Something wrong happend. {err.Message}"
                           };
            }

            return response;
        }

        public ResponseModel UpdateDailyStatus(DailyStatusModel dailyStatus)
        {
            ResponseModel response;

            try
            {
                DailyStatus? dailyStatusNeedToUpdate = _context.DailyStatuses.FirstOrDefault(d => d.DailyStatusId == dailyStatus.DailyStatusId);

                if(dailyStatusNeedToUpdate != null)
                {
                    dailyStatusNeedToUpdate.Date = dailyStatus.Date;
                    dailyStatusNeedToUpdate.Name = dailyStatus.Name;
                    dailyStatusNeedToUpdate.Project = dailyStatus.Project;
                    dailyStatusNeedToUpdate.Task = dailyStatus.Task;
                    dailyStatusNeedToUpdate.Description = dailyStatus.Description;
                    dailyStatusNeedToUpdate.ActualTime = dailyStatus.ActualTime;
                    dailyStatusNeedToUpdate.Category = dailyStatus.Category;

                    _context.DailyStatuses.Update(dailyStatusNeedToUpdate);
                    _context.SaveChanges();
                }

                response = new ResponseModel
                           {
                               StatusCode = 200,
                               StatusMessage = "Daily Status Updated Successfully."
                           };
            }
            catch(Exception err)
            {
                response = new ResponseModel
                           {
                               StatusCode = 400,
                               StatusMessage = $"Something wrong happend. {err.Message}"
                           };
            }

            return response;
        }

        public ResponseModel DeleteDailyStatus(int nId)
        {
            ResponseModel response;

            try
            {
                DailyStatus? dailyStatusNeedToDelete = _context.DailyStatuses.FirstOrDefault(d => d.DailyStatusId == nId);

                if(dailyStatusNeedToDelete != null)
                {
                    _context.DailyStatuses.Remove(dailyStatusNeedToDelete);
                    _context.SaveChanges();
                }

                response = new ResponseModel
                           {
                               StatusCode = 200,
                               StatusMessage = "Daily Status Deleted Successfully."
                           };
            }
            catch(Exception err)
            {
                response = new ResponseModel
                           {
                               StatusCode = 400,
                               StatusMessage = $"Something wrong happend. {err.Message}"
                           };
            }

            return response;
        }
        #endregion
    }
}