using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MusicRising.Helpers;

namespace MusicRising.Models;

public class Show
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set;}
    [Required]
    public string IdentityUserId { get; set;}

    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }
    [Required]
    public string? VenueId { get; set; }
    [ForeignKey("VenueId")]
    public Venue? Venue { get; set; }
    
    [Required]
    public string? BandID { get; set; }
    [ForeignKey("BandId")]
    public Band? HeadLiner { get; set; }
    
    public List<Band>? Bands { get; set;}
    public GenreEnum Genre { get; set;}
    public JSType.Date date { get; set;}
    public string? PromoLink { get; set;}
    
}