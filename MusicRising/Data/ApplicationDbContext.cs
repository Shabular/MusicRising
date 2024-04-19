using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MusicRising.Models;

namespace MusicRising.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Show> Shows { get; set; }
    public DbSet<Band> Bands { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<PromoItem> PromoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);

        // Specify key length for the Id column in AspNetRoles table
        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(255); // Set the appropriate length for your Id column
        });
        
        
        
        // Configure entity relationships
        modelBuilder.Entity<Show>()
            .HasOne(s => s.HeadLiner)
            .WithMany(b => b.Shows)
            .HasForeignKey(s => s.Id);
        

        base.OnModelCreating(modelBuilder);
    }
    
    
    
}