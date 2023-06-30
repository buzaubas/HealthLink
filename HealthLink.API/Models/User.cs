using System;
using System.Collections.Generic;

namespace HealthLink.API.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime DateCreated { get; set; }

    public bool IsBlocked { get; set; }

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual Role Role { get; set; } = null!;
}
