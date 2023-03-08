using System.ComponentModel.DataAnnotations;

namespace OfficeDailyStatusTracker.Common.Models
{
    public class DailyStatusModel
    {
        public int DailyStatusId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Date Should Not Empty")]
        public string? Date { get; set; }
        [Required(ErrorMessage = "Name Should Not Empty")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Project Name Should Not Empty")]
        public string? Project { get; set; }
        [Required(ErrorMessage = "Task Name Should Not Empty")]
        public string? Task { get; set; }
        public string? Description { get; set; }
        public string? ActualTime { get; set; }
        public string? Category { get; set; }
    }
}
