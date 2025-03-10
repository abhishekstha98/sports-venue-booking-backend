using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Entities;
using Domain;

namespace Infrastructure.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amenity> Amenities { get; set; }

        public virtual DbSet<Announcement> Announcements { get; set; }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Leaderboard> Leaderboards { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Sport> Sports { get; set; }

        public virtual DbSet<Tournament> Tournaments { get; set; }

        public virtual DbSet<UpcomingBooking> UpcomingBookings { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Amenitie__3214EC072A56F3C2");

                entity.HasIndex(e => e.VenueId, "IDX_Amenities_VenueId");

                entity.Property(e => e.Amenity1)
                    .HasMaxLength(100)
                    .HasColumnName("Amenity");

                entity.HasOne(d => d.Venue).WithMany(p => p.Amenities)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__Amenities__Venue__75A278F5");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(e => e.AnnouncementId)
                      .HasName("PK__Announce__9DE4457432BB64A1");

                entity.Property(e => e.Title)
                      .HasMaxLength(100);

                entity.Property(e => e.Message)
                      .HasMaxLength(255);

                entity.Property(e => e.ExpiryDate)
                      .HasColumnType("datetime")
                      .IsRequired(false);

                entity.HasOne<Image>() 
                      .WithMany()
                      .HasForeignKey(e => e.ImageId)
                      .OnDelete(DeleteBehavior.SetNull) 
                      .HasConstraintName("FK_Announcements_Images");
            });


            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED168C5F11");

                entity.HasIndex(e => e.UserId, "IDX_Bookings_UserId");

                entity.HasIndex(e => e.VenueId, "IDX_Bookings_VenueId");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Bookings__UserId__114A936A");

                entity.HasOne(d => d.Venue).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__Bookings__VenueI__123EB7A3");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Images__3214EC0786F8015B");

                entity.HasIndex(e => e.VenueId, "IDX_Images_VenueId");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.HasOne(d => d.Venue).WithMany(p => p.Images)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__Images__VenueId__787EE5A0");
            });

            modelBuilder.Entity<Leaderboard>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Leaderbo__1788CC4CE01A8663");

                entity.ToTable("Leaderboard");

                entity.HasIndex(e => e.UserId, "IDX_Leaderboard_UserId");

                entity.Property(e => e.UserId).ValueGeneratedNever();
                entity.Property(e => e.Points).HasDefaultValue(0);
                entity.Property(e => e.Ranking).HasDefaultValue(0);

                entity.HasOne(d => d.User).WithOne(p => p.Leaderboard)
                    .HasForeignKey<Leaderboard>(d => d.UserId)
                    .HasConstraintName("FK__Leaderboa__UserI__17036CC0");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Players__3214EC076D125309");

                entity.HasIndex(e => e.UserId, "IDX_Players_UserId");

                entity.HasOne(d => d.Sport).WithMany(p => p.Players)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK_Players_Sports");

                entity.HasOne(d => d.User).WithMany(p => p.Players)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Players__UserId__02FC7413");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.HasKey(e => e.SportId).HasName("PK__Sports__7A41AF3C2DE9C87E");

                entity.HasIndex(e => e.SportName, "UQ__Sports__14A9DBB039C97DA9").IsUnique();

                entity.Property(e => e.SportName).HasMaxLength(50);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.TournamentId).HasName("PK__Tourname__AC63131354CCFE80");

                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Venue).WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK_Tournaments_Venues");
            });

            modelBuilder.Entity<UpcomingBooking>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("UpcomingBookings");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.Time).HasMaxLength(10);
                entity.Property(e => e.Venue);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D24B5DB5");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C820F2D6").IsUnique();

                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Points).HasDefaultValue(0);
                entity.Property(e => e.ProfilePicUrl).HasMaxLength(255);
                entity.Property(e => e.Ranking).HasDefaultValue(0);
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Venues__3214EC07D3D8012C");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
                entity.Property(e => e.OpenHoursFrom).HasMaxLength(10);
                entity.Property(e => e.OpenHoursTo).HasMaxLength(10);
                entity.Property(e => e.PricePerHour).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Ratings).HasColumnType("decimal(3, 2)");
                entity.Property(e => e.Sport).HasMaxLength(50);
                entity.Property(e => e.VenueAddress).HasMaxLength(255);
                entity.Property(e => e.VenueType).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
