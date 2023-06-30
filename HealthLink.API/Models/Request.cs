using System;
using System.Collections.Generic;

namespace HealthLink.API.Models;

public partial class Request
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OrganizationId { get; set; }

    public int StatusId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateProcessed { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
