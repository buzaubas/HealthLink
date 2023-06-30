using System;
using System.Collections.Generic;

namespace HealthLink.API.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<History> HistoryNewStatuses { get; set; } = new List<History>();

    public virtual ICollection<History> HistoryOldStatuses { get; set; } = new List<History>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
