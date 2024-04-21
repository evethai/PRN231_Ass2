using Microsoft.AspNetCore.Mvc;
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
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class UserUpdateModel
    {
        [FromForm(Name = "email")]
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [FromForm(Name = "password")]
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        [FromForm(Name = "source")]
        [Required(ErrorMessage = "Source is required.")]
        public string? Source { get; set; }
        [FromForm(Name = "first-name")]
        [Required(ErrorMessage = "FirstName is required.")]
        public string? FirstName { get; set; }
        [FromForm(Name = "last-name")]
        [Required(ErrorMessage = "LastName is required.")]
        public string? LastName { get; set; }
        [FromForm(Name = "role-id")]
        [Required(ErrorMessage = "Role Id is required.")]
        public int? RoleId { get; set; }
        [FromForm(Name = "pub-id")]
        [Required(ErrorMessage = "Publisher Id is required.")]
        public int? PubId { get; set; }
        [FromForm(Name = "hire-date")]
        [Required(ErrorMessage = "Hire date is required.")]
        public DateTime? HireDate { get; set; }


    }

    
}
