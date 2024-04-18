using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleDesc { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
