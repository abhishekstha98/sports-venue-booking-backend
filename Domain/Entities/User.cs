using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsPlayer { get; set; }

    public string? ProfilePicUrl { get; set; }

    public int? Points { get; set; }

    public int? Ranking { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Leaderboard? Leaderboard { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
