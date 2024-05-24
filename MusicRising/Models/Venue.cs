using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MusicRising.Helpers;

namespace MusicRising.Models;

public class Venue
{
    [Key]
    public string VenueId { get; set;}

    [Required]
    public string IdentityUserId { get; set;}
    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set;}

    public string VenueName { get; set;}

    public LocationEnum Location { get; set;}

    // Proper navigation properties for EF relationships
    public List<Show>? Shows { get; set;}
    public string? Details { get; set; }
    public List<PromoItem>? PromoItems { get; set;}
    
    public string? VenuePicture { get; set; }

    public GenreEnum Genre { get; set;}
    public List<Rating>? Ratings { get; set;}
    
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}