using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Repository.Models
{
    public class BookModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("publisher-id")]
        public int? PubId { get; set; }
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("advance")]
        public decimal? Advance { get; set; }
        [JsonPropertyName("royalty")]
        public decimal? Royalty { get; set; }
        [JsonPropertyName("ytd-sales")]
        public int? YtdSales { get; set; }
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }
        [JsonPropertyName("is-active")]
        public bool? IsActive { get; set; }
    }

    public class BookModelResponse
    {
        public int total { get; set; }
        public int currentPage { get; set; }
        public List<BookModel> books { get; set; }
    }

    public class BookUpdateModel
    {
        [Required(ErrorMessage ="Title is required.")]
        [FromForm(Name ="title")]
        public string? Title { get; set; }
        [FromForm(Name = "type")]
        [Required(ErrorMessage = "Type is required.")]
        public string? Type { get; set; }
        [FromForm(Name = "pub-id")]
        [Required(ErrorMessage = "Publisher Id is required.")]
        public int? PubId { get; set; }
        [FromForm(Name = "price")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Advance is required.")]
        [FromForm(Name = "advance")]
        public decimal? Advance { get; set; }
        [Required(ErrorMessage = "Royalty is required.")]
        [FromForm(Name = "royalty")]
        public decimal? Royalty { get; set; }
        [Required(ErrorMessage = "Ytd-sales is required.")]
        [FromForm(Name = "ytd-sales")]
        public int? YtdSales { get; set; }
        [MaxLength(200, ErrorMessage = "Notes must be less than 200 characters.")]
        [FromForm(Name = "notes")]
        public string? Notes { get; set; }
    }
}
