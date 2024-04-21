using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models.BookAuthorModelFolder
{
    public class GetAllBookAuthorModel
    {
        [JsonPropertyName("bookauthorid")]
        public int Id { get; set; }
        [JsonPropertyName("bookid")]
        public int? BookId { get; set; }
        [JsonPropertyName("authorid")]
        public int? AuthorId { get; set; }
        [JsonPropertyName("authororder")]
        public int? AuthorOrder { get; set; }
        [JsonPropertyName("royaltyper")]
        public decimal? RoyaltyPer { get; set; }
    }
}
