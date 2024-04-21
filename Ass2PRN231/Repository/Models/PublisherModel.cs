using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class PublisherModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }

    public class PublisherModelResponse
    {
        public int total { get; set; }
        public int currentPage { get; set; }
        public List<PublisherModel> pubs { get; set; }
    }
    
    public class PublisherUpdateModel
    {
        [FromForm(Name = "name")]
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        [FromForm(Name = "city")]
        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }
        [FromForm(Name = "state")]
        [Required(ErrorMessage = "State is required.")]
        public string? State { get; set; }
        [FromForm(Name = "country")]
        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }
    }
}
