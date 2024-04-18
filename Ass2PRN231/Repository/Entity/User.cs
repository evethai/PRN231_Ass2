﻿using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Source { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? RoleId { get; set; }

    public int? PubId { get; set; }

    public DateTime? HireDate { get; set; }

    public virtual Publisher? Pub { get; set; }

    public virtual Role? Role { get; set; }
}
