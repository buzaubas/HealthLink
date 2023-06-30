using System;
using System.Collections.Generic;
using HealthLink.WEB.Models;

public partial class History
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public int OldStatusId { get; set; }

    public int NewStatusId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Status NewStatus { get; set; } = null!;

    public virtual Status OldStatus { get; set; } = null!;

    public virtual Request Request { get; set; } = null!;
}
