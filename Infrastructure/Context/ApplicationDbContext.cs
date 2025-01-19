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

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Amenitie__3214EC072A56F3C2");

                entity.Property(e => e.Amenity1)
                    .HasMaxLength(100)
                    .HasColumnName("Amenity");

                entity.HasOne(d => d.Venue).WithMany(p => p.AmenitiesNavigation)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__Amenities__Venue__75A278F5");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Images__3214EC0786F8015B");

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.HasOne(d => d.Venue).WithMany(p => p.ImagesNavigation)
                    .HasForeignKey(d => d.VenueId)
                    .HasConstraintName("FK__Images__VenueId__787EE5A0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D24B5DB5");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C820F2D6").IsUnique();

                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Venues__3214EC07D3D8012C");

                entity.Property(e => e.OpenHoursFrom).HasMaxLength(10);
                entity.Property(e => e.OpenHoursTo).HasMaxLength(10);
                entity.Property(e => e.PricePerHour).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Sport).HasMaxLength(50);
                entity.Property(e => e.VenueAddress).HasMaxLength(255);
                entity.Property(e => e.VenueType).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
