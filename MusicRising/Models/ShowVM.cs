using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicRising.Helpers;

namespace MusicRising.Models;
public class ShowVM
{
    public string ShowId { get; set; }
    public string VenueId { get; set; }
    public Venue Venue { get; set; }
    public string BandId { get; set; }
    public Band HeadLiner { get; set; }
    public GenreEnum Genre { get; set; }
    public DateTime Date { get; set; }
    public string PromoItem { get; set; }
    public string? PromoLink { get; set; }
    public double? ShowFee { get; set; }
    public double BandFee { get; set; } = 50.00;
    public bool Payed { get; set; }
    public bool IsOwner { get; set; } // Add this property
}