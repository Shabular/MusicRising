using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MusicRising.Helpers;

namespace MusicRising.Models;

public class VenueVM
{
    public string VenueId { get; set; }
    public string IdentityUserId { get; set; }
    public IdentityUser User { get; set; }
    public string VenueName { get; set; }
    public IFormFile Image { get; set; }
    public string ImageFileName { get; set; }
    public LocationEnum Location { get; set; }
    public GenreEnum Genre { get; set; }
    public List<Show> Shows { get; set; }
    public List<PromoItem> PromoItems { get; set; }
    public List<Rating> Ratings { get; set; }
    public string BankAccount { get; set; }
    public bool IsOwner { get; set; } // Add this property
}
