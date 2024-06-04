using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MusicRising.Helpers;

namespace MusicRising.Models
{
    public class EntityEditVM
    {
        public string Id { get; set; }

        [Required]
        public string IdentityUserId { get; set; }
        public string HeadLinerID { get; set; }
        public string HeadLinerName { get; set; }

        public Venue? Venue { get; set; }
        public Band? HeadLiner { get; set; }

        public string Name { get; set; }
        public IFormFile? Picture { get; set; }
        public string? PictureUrl { get; set; }
        public string? Details { get; set; }
        public DateTime? Date { get; set; }


        public LocationEnum Location { get; set; }
        public GenreEnum Genre { get; set; }
        public string BankAccount { get; set; }
        public double? ShowFee { get; set; }
        public double? BandFee { get; set; }
        public bool? Booked { get; set; } 
        public bool IsOwner { get; set; }
        
        public bool BookableLink { get; set; } = false;
        
        public string Address { get; set; }
    }
}