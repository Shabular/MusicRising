using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MusicRising.Helpers;

namespace MusicRising.Models;

// this will be a child of user 
public class Band 
{
    public string Id { get; set;}
    // If the user who owns the band is deleted remove the band
    [Required]
    public string IdentityUserId { get; set;}
    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set;}
    public string? BandName { get; set;}
    public string? BandPicture { get; set; }
    public LocationEnum? Location { get; set;}
    [ForeignKey("ShowId")]
    public List<Show>? Shows { get; set;}
    [ForeignKey("PromoItemId")]
    public List<PromoItem>? PromoItems { get; set;}
    private string? _bankAccount { get; set;}
    public GenreEnum? Genre { get; set;}
    public List<Rating>? Ratings { get; set; }


    }
