using System;
using System.Collections.Generic;

namespace HealthLink.API.Models;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Information { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual User User { get; set; } = null!;
}
