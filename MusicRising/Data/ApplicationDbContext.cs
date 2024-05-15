using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data;

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
        base.OnModelCreating(modelBuilder); // This needs to be called first

        // Configure the Band entity
        modelBuilder.Entity<Band>(entity =>
        {
            entity.HasMany(b => b.Shows)
                  .WithOne(s => s.HeadLiner) // Correct navigation property
                  .HasForeignKey(s => s.BandId)
                  .OnDelete(DeleteBehavior.SetNull); // Adjusted for desired behavior

            entity.HasMany(b => b.PromoItems)
                  .WithOne(p => p.Band)
                  .HasForeignKey(p => p.BandId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(b => b.Ratings)
                  .WithOne(r => r.Band)
                  .HasForeignKey(r => r.BandId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Additional configurations for other entities
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
                  .WithMany() // Adjust this if Users should explicitly reference PromoItems
                  .HasForeignKey(p => p.IdentityUserId)
                  .OnDelete(DeleteBehavior.Cascade); // Consider the deletion behavior
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasOne(r => r.Venue)
                  .WithMany(v => v.Ratings)
                  .HasForeignKey(r => r.VenueId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.User)
                  .WithMany() // Adjust this to include navigation property if exists
                  .HasForeignKey(r => r.IdentityUserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}