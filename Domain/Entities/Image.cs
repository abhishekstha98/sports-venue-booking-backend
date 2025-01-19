using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Image
{
    public int Id { get; set; }

    public int VenueId { get; set; }

    public string FilePath { get; set; } = null!;

    public virtual Venue Venue { get; set; } = null!;
}
