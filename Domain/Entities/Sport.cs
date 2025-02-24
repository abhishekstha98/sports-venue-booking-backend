using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Sport
{
    public int SportId { get; set; }

    public string SportName { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
