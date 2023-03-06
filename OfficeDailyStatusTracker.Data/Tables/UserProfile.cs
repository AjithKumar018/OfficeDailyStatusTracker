using System;
using System.Collections.Generic;

namespace OfficeDailyStatusTracker.Data.Tables;

public partial class UserProfile
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? LastLogin { get; set; }

    public string? CreatedOn { get; set; }

    public string? UpdatedOn { get; set; }

    public virtual ICollection<DailyStatus> DailyStatuses { get; } = new List<DailyStatus>();
}
