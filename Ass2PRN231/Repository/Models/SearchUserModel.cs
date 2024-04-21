using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class SearchUserModel
    {
        [FromQuery(Name = "current-page")]
        public int? currentPage { get; set; }
        [FromQuery(Name = "page-size")]
        public int? pageSize { get; set; }
        [FromQuery(Name = "email")]
        public string? email { get; set; }
        [FromQuery(Name = "min-date")]
        public DateTime? minDate { get; set; }
        [FromQuery(Name = "max-date")]
        public DateTime? maxDate { get; set; }
        [FromQuery(Name = "sort-by-hire-date")]
        public bool? sortByHireDate { get; set; } = false;

    }
}