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

    public string? Name { get; set; }

    public decimal? Ratings { get; set; }

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
