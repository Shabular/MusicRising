using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MusicRising.Models;

public class PromoItem
{
    [Key]
    public string PromoItemId { get; set; }

    [Required]
    public string? IdentityUserId { get; set; }
    [ForeignKey("IdentityUserId")] // Correct Foreign Key for IdentityUser
    public IdentityUser? User { get; set; }
    
    public string? BandId { get; set; }
    [ForeignKey("BandId")] // Correct Foreign Key for Band
    public Band? Band { get; set; }
    
    public string? VenueId { get; set; }
    [ForeignKey("VenueId")] // Correct Foreign Key for Venue
    public Venue? Venue { get; set; }
    
    public string? Link { get; set; }
}