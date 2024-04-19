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
    
    public DbSet<Band> Bands { get; set; }
    public DbSet<Show> Shows { get; set; }
    
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<PromoItem> PromoItems { get; set; }

  
    
}