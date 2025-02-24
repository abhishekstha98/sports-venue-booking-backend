using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Player
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? SportId { get; set; }

    public virtual Sport? Sport { get; set; }

    public virtual User User { get; set; } = null!;
}
