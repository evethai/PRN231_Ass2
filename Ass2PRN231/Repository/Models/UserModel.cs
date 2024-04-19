using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("source")]
        public string? Source { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("roleId")]
        public int? RoleId { get; set; }
        [JsonPropertyName("pubId")]
        public int? PubId { get; set; }
        [JsonPropertyName("hireDate")]
        public DateTime? HireDate { get; set; }
    }
    public class Login
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class UserUpdateDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("source")]
        public string? Source { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("roleId")]
        public int? RoleId { get; set; }
        [JsonPropertyName("pubId")]
        public int? PubId { get; set; }
        [JsonPropertyName("hireDate")]
        public DateTime? HireDate { get; set; }


    }

    public class UserCreateDTO
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("source")]
        public string? Source { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("roleId")]
        public int? RoleId { get; set; }
        [JsonPropertyName("pubId")]
        public int? PubId { get; set; }
        [JsonPropertyName("hireDate")]
        public DateTime? HireDate { get; set; }

    }
}
