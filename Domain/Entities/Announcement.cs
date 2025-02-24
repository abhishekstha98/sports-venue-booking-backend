using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Announcement
{
    public int AnnouncementId { get; set; }

    public string Title { get; set; } = null!;

    public string? Message { get; set; }
}
