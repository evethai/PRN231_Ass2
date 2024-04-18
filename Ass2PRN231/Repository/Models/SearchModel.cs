using Microsoft.AspNetCore.Mvc;

namespace Repository.Models
{
    public class SearchModel
    {

    }
    public class BookSearchModel
    {
        [FromQuery(Name = "current-page")]
        public int? currentPage { get; set; }
        [FromQuery(Name = "page-size")]
        public int? pageSize { get; set; }
        [FromQuery(Name = "title")]
        public string? title { get; set; }
        [FromQuery(Name = "type")]
        public string? type { get; set; }
        [FromQuery(Name = "pub-id")]
        public int? pubId { get; set; }
        [FromQuery(Name = "min-price")]
        public decimal? minPrice { get; set; }
        [FromQuery(Name = "max-price")]
        public decimal? maxPrice { get; set; }
        [FromQuery(Name = "min-date")]
        public DateTime? minDate { get; set; }
        [FromQuery(Name = "max-date")]
        public DateTime? maxDate { get; set; }
        [FromQuery(Name = "sort-by-price")]
        public bool? sortByPrice { get; set; } = false;
        [FromQuery(Name = "sort-by-date")]
        public bool? sortByDate { get; set; } = false;
        [FromQuery(Name = "descending")]
        public bool? descending { get; set; } = false;
    }
}
