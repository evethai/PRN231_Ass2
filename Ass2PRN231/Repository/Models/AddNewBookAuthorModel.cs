﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class AddNewBookAuthorModel
    {
        [JsonPropertyName("book-id")]
        public int? BookId { get; set; }
        [JsonPropertyName("author-id")]
        public int? AuthorId { get; set; }
        [JsonPropertyName("author-order")]
        public int? AuthorOrder { get; set; }
        [JsonPropertyName("royalty-per")]
        public decimal? RoyaltyPer { get; set; }
    }
}
