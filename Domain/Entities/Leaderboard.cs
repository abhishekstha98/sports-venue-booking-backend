using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Leaderboard
{
    public int UserId { get; set; }

    public int? Points { get; set; }

    public int? Ranking { get; set; }

    public virtual User User { get; set; } = null!;
}
