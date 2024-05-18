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
    public List<PromoItem>? PromoItems { get; set;}
    
    public string? VenuePicture { get; set; }

    public string BankAccount { get; private set;}  // Made public getter for external access if needed
    public GenreEnum Genre { get; set;}
    public List<Rating>? Ratings { get; set;}
}