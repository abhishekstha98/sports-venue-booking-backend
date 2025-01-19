using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Amenity
{
    public int Id { get; set; }

    public int VenueId { get; set; }

    public string Amenity1 { get; set; } = null!;

    public virtual Venue Venue { get; set; } = null!;
}
