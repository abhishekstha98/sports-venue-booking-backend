using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Context;

public class HomeService : IHomeService
{
    private readonly ApplicationDbContext _context;

    public HomeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HomeResponse> GetHomeDataAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            throw new KeyNotFoundException("User not found");
        var featuredVenues = new List<Venue>();
        try
        {
            featuredVenues = _context.Venues
            .OrderByDescending(v => v.Ratings)
            .Take(5)
            .Select(b => new Venue
            {
                Id = b.Id,
                VenueAddress = b.VenueAddress,
                PricePerHour = b.PricePerHour,
                Name = b.Name,
                Ratings = b.Ratings,
                Images = b.Images
            }).ToList();
        }
        catch (Exception ex)
        {

        }

        var upcomingBookings = new List<UpcomingBooking>();
        try
        {
            upcomingBookings = _context.UpcomingBookings
            .Where(b => b.UserId == userId)
            .OrderBy(b => b.BookingDate)
            .Take(5)
            .Select(b => new UpcomingBooking
            {
                BookingId = b.BookingId,
                VenueName = b.VenueName,
                VenueAddress = b.VenueAddress,
                BookingDate = b.BookingDate,
                Time = b.Time,
                Status = b.Status
            }).ToList();
        }
        catch (Exception ex)
        {

        }

        var leaderboard = new List<Leaderboard>();
        try
        {
            leaderboard = _context.Leaderboards
            .OrderBy(l => l.Ranking)
            .Take(10)
            .Select(l => new Leaderboard
            {
                UserId = l.UserId,
                User = l.User,
                Points = l.Points,
                Ranking = l.Ranking
            }).ToList();
        }
        catch (Exception ex)
        {

        }

        var tournaments = new List<Tournament>();
        try
        {
            tournaments = _context.Tournaments
            .OrderBy(t => t.Date)
            .Take(5)
            .Select(t => new Tournament
            {
                TournamentId = t.TournamentId,
                Name = t.Name,
                Date = t.Date,
                Status = t.Status
            }).ToList();
        }
        catch (Exception ex)
        {

        }

        var announcements = new List<Announcement>();
        try
        {
            announcements = _context.Announcements
            .OrderBy(a => a.ExpiryDate)
            .Take(5)
            .Select(a => new Announcement
            {
                AnnouncementId = a.AnnouncementId,
                Title = a.Title,
                Message = a.Message,
                ExpiryDate = a.ExpiryDate,
            }).ToList();
        }
        catch (Exception ex)
        {

        }

        return new HomeResponse
        {
            FeaturedVenues = featuredVenues,
            UpcomingBookings = upcomingBookings,
            Tournaments = tournaments,
            Announcements = announcements
        };
    }
}
