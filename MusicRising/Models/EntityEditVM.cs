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

        public string Name { get; set; }
        public IFormFile Picture { get; set; }
        public string PictureUrl { get; set; }

        public LocationEnum Location { get; set; }
        public GenreEnum Genre { get; set; }
        public string BankAccount { get; set; }
        public object IsOwner { get; set; }
    }
}