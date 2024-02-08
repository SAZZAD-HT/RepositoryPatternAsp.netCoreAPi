using System;
using System.Collections.Generic;

namespace Samurai_V2_.Net_8.DbContexts.Models;

public partial class TblBook
{
    public int BookId { get; set; }

    public string? BookTitle { get; set; }

    public string? Author { get; set; }

    public string? Genre { get; set; }

    public double? Price { get; set; }
}
