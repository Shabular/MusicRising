using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicRising.Helpers;

namespace MusicRising.Models;

public class ShowVM
{
    [Key]
    public string ShowId { get; set;}

    [Required]
    public string VenueId { get; set; }
    [ForeignKey("VenueId")]
    public Venue? Venue { get; set; }

    public string? BandId { get; set; }  // Ensure this matches the ForeignKey attribute below
    [ForeignKey("BandId")]
    public Band? HeadLiner { get; set; }

    // Assuming you want to store multiple bands, you would typically need another entity to handle this
    // [NotMapped] Removed for now - consider using a many-to-many relationship if applicable

    public GenreEnum Genre { get; set; }
    public DateTime Date { get; set; }
    public string? PromoItem { get; set; }
    public double? ShowFee { get; set; }
    

    public double BandFee { get; private set; } // Consider providing a public getter if needed
    public bool Payed { get; private set; } // Consider changing to public or providing a getter/setter
}