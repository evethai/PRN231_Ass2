using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class BookAuthor
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? AuthorId { get; set; }

    public int? AuthorOrder { get; set; }

    public decimal? RoyaltyPer { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Book? Book { get; set; }
}
