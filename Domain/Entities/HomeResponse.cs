using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HomeResponse
    {
        public User UserProfile { get; set; }
        public List<Venue> FeaturedVenues { get; set; }
        public List<UpcomingBooking> UpcomingBookings { get; set; }
        public List<Leaderboard> Leaderboard { get; set; }
        public List<Tournament> Tournaments { get; set; }
        public List<Announcement> Announcements { get; set; }
    }

}
