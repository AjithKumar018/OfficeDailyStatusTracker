using System.ComponentModel.DataAnnotations;

namespace OfficeDailyStatusTracker.Common.Models
{
    public class AuthenticationModel
    {
        #region Properties
        public string? UserKey { get; set; }
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        public string? Password { get; set; }
        #endregion
    }
}