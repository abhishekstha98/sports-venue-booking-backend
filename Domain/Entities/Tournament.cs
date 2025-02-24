using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Tournament
{
    public int TournamentId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Status { get; set; }

    public int? VenueId { get; set; }

    public virtual Venue? Venue { get; set; }
}
