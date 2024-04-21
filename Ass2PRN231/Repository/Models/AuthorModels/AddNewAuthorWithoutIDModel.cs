using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models.AuthorModels
{
    public class AddNewAuthorWithoutIDModel
    {
        [JsonPropertyName("authorlastname")]
        public string? LastName { get; set; }
        [JsonPropertyName("authorfirstname")]
        public string? FirstName { get; set; }
        [JsonPropertyName("authorphone")]
        [MaxLength(12)]
        public string? Phone { get; set; }
        [JsonPropertyName("authoraddress")]
        public string? Address { get; set; }
        [JsonPropertyName("authorcity")]
        public string? City { get; set; }
        [JsonPropertyName("authorstate")]
        public string? State { get; set; }
        [JsonPropertyName("authorzip")]
        public string? Zip { get; set; }
        [JsonPropertyName("authoremail")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
