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
    [Key]
    public string Id { get; set;}
    
    [Required]
    public string? VenueId { get; set; }
    [ForeignKey("VenueId")]
    public Venue? Venue { get; set; }
    
    public string? BandID { get; set; }
    [ForeignKey("BandId")]
    public Band? HeadLiner { get; set; }
    
    [NotMapped]
    public List<Band>? Bands { get; set;}
    public GenreEnum Genre { get; set;}
    public DateTime Date { get; set;}
    public string? PromoLink { get; set;}
    public double? ShowFee { get; set; }
    protected double BandFee { get; set; }
    protected bool? Payed { get; set; }
    

}