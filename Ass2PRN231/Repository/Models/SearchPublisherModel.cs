using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class SearchPublisherModel
    {
        [FromQuery(Name = "current-page")]
        public int? currentPage { get; set; }
        [FromQuery(Name = "page-size")]
        public int? pageSize { get; set; }  
        [FromQuery(Name = "name")]
        public string? name { get; set; }
        [FromQuery(Name = "city")]
        public string? city { get; set; }
        [FromQuery(Name = "country")]
        public string? country { get; set; }
    }
}
