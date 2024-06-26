﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MusicRising.Helpers;

namespace MusicRising.Models;

public class Show
{
    public Show()
    {
    }

    [Key]
    public string ShowId { get; set;}

    [Required]
    public string VenueId { get; set; }
    [ForeignKey("VenueId")]
    public Venue? Venue { get; set; }

    public string? BandId { get; set; }  // Ensure this matches the ForeignKey attribute below
    [ForeignKey("BandId")]
    public Band? HeadLiner { get; set; }
    
    public GenreEnum Genre { get; set; }
    public DateTime Date { get; set; }
    public string? Details { get; set; }
    public string? PromoLink { get; set; }
    public double? ShowFee { get; set; }

    public double BandFee { get; set; } 
    public bool Booked { get; set; }
    
    public bool BidByVenue { get; set; }
    public bool IsVenueOwner { get; set; }
    public bool IsBandMember { get; set; }
}