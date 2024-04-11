using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicRising.Models;

public class Rating
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set;}
    [Required]
    public string? IdentityUserId { get; set; }
    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }
    
    public string? BandId { get; set; }
    [ForeignKey("BandId")]
    public Band? Band { get; set; }
    
    public string? VenueId { get; set; }
    [ForeignKey("VenueId")]
    public Venue? Venue { get; set; }
    
    public int Stars { get; set; }
    public string Comment { get; set; }
}