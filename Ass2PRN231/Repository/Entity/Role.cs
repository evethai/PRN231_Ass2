using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.Entity;

public partial class Role
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("roleDesc")]
    public string? RoleDesc { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
