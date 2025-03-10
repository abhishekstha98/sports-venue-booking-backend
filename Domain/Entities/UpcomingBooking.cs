using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UpcomingBooking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int Venue { get; set; }

    public DateTime BookingDate { get; set; }

    public string? Time { get; set; }

    public string? Status { get; set; }
}
