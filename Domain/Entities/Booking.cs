using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int VenueId { get; set; }

    public DateTime BookingDate { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Status { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Venue Venue { get; set; } = null!;
}
