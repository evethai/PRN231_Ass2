using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

    }
}
