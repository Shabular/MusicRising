using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MusicRising.Helpers;

namespace MusicRising.Models;

public class Venue
{
    public string Id { get; set;}
    // If the user who owns the venue is deleted remove the band
    [Required]
    public string IdentityUserId { get; set;}
    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set;}
    public string VenueName { get; set;}
    public LocationEnum Location { get; set;}
    public List<Show>? Shows { get; set;}
    public List<PromoItem>? PromoItems { get; set;}
    private string? _bankAccount { get; set;}
    public GenreEnum Genre { get; set;}
    public List<Rating>? Ratings { get; set; }

}