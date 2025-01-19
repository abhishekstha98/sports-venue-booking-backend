using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Venue
{
    public int Id { get; set; }

    public string VenueAddress { get; set; } = null!;

    public string OpenHoursFrom { get; set; } = null!;

    public string OpenHoursTo { get; set; } = null!;

    public string Sport { get; set; } = null!;

    public string VenueType { get; set; } = null!;

    public bool ParkingAvailable { get; set; }

    public decimal PricePerHour { get; set; }

    public string? Description { get; set; }

    public string? Amenities { get; set; }

    public string? Images { get; set; }

    public virtual ICollection<Amenity> AmenitiesNavigation { get; set; } = new List<Amenity>();

    public virtual ICollection<Image> ImagesNavigation { get; set; } = new List<Image>();
}
