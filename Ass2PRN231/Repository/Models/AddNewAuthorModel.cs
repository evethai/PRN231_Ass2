using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class AddNewAuthorModel
    {
        [JsonPropertyName("last-name")]
        [Required]
        public string? LastName { get; set; }
        [JsonPropertyName("first-name")]
        [Required]
        public string? FirstName { get; set; }
        [JsonPropertyName("phone")]
        [MaxLength(12)]
        [Required]
        public string? Phone { get; set; }
        [JsonPropertyName("address")]
        [Required]
        public string? Address { get; set; }
        [JsonPropertyName("city")]
        [Required]
        public string? City { get; set; }
        [JsonPropertyName("state")]
        [Required]
        public string? State { get; set; }
        [JsonPropertyName("zip")]
        [Required]
        [StringLength(7)]
        public string? Zip { get; set; }
        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
