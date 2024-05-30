using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MusicRising.Models;

public class Rating
{
    [Key]
    public string RatingId { get; set; }

    [Required]
    public string IdentityUserId { get; set; }

    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }

    public string? BandId { get; set; }

    [ForeignKey("BandId")]
    public Band? Band { get; set; }

    public string? VenueId { get; set; }

    [ForeignKey("VenueId")]
    public Venue? Venue { get; set; }

    [Range(1, 5)] // Ensures that Stars are within a valid range (assuming a 1-5 scale)
    public int Stars { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Comment { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }
}