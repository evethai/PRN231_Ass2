using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class AuthorModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("last-name")]
        public string? LastName { get; set; }
        [JsonPropertyName("first-name")]
        public string? FirstName { get; set; }
        [JsonPropertyName("phone")]
        [MaxLength(12)]
        public string? Phone { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }
        [JsonPropertyName("zip")]
        public string? Zip { get; set; }
        [JsonPropertyName("email")]
        [EmailAddress]
        public string? Email { get; set; }
        public List<BookAuthorModel> BookAuthors { get; set; }
    }
}
