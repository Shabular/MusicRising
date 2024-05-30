using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;
using MusicRising.Helpers;

namespace MusicRising.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Band> Bands { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<PromoItem> PromoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Band>(entity =>
            {
                entity.HasKey(b => b.BandId);
                entity.Property(b => b.BandId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(b => b.Genre)
                    .HasConversion<int>();

                entity.Property(b => b.Location)
                    .HasConversion<int>();

                entity.HasMany(b => b.Shows)
                      .WithOne(s => s.HeadLiner)
                      .HasForeignKey(s => s.BandId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(b => b.PromoItems)
                      .WithOne(p => p.Band)
                      .HasForeignKey(p => p.BandId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(b => b.Ratings)
                      .WithOne(r => r.Band)
                      .HasForeignKey(r => r.BandId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Show>(entity =>
            {
                entity.HasOne(s => s.Venue)
                      .WithMany(v => v.Shows)
                      .HasForeignKey(s => s.VenueId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PromoItem>(entity =>
            {
                entity.HasOne(p => p.Venue)
                      .WithMany(v => v.PromoItems)
                      .HasForeignKey(p => p.VenueId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.User)
                      .WithMany()
                      .HasForeignKey(p => p.IdentityUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasOne(r => r.Venue)
                      .WithMany(v => v.Ratings)
                      .HasForeignKey(r => r.VenueId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.User)
                      .WithMany()
                      .HasForeignKey(r => r.IdentityUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}