using System;
using System.Collections.Generic;

namespace OfficeDailyStatusTracker.Data.Tables;

public partial class DailyStatus
{
    public int Id { get; set; }

    public string? Date { get; set; }

    public string? Name { get; set; }

    public string? Project { get; set; }

    public string? Task { get; set; }

    public string? Description { get; set; }

    public string? ActualTime { get; set; }

    public string? Category { get; set; }
}
