using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MusicRising.Helpers;

namespace MusicRising.Models;

public class Band 
{
    [Key]
    public string BandId { get; set; }

    [Required]
    public string IdentityUserId { get; set; }
    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }

    public string? BandName { get; set; }
    public string? BandPicture { get; set; }

    public LocationEnum Location { get; set; }
    public GenreEnum Genre { get; set; }

    public List<Show>? Shows { get; set; }  // Ensure Show has a BandId foreign key
    public List<PromoItem>? PromoItems { get; set; }  // Ensure PromoItem has a BandId foreign key

    public string? BankAccount { get; set; }  // Made public and renamed
    public List<Rating>? Ratings { get; set; }
}