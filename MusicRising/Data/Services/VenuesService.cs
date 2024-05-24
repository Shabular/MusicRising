using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services
{
    public class VenuesService : IVenuesService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VenuesService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IQueryable<Venue> GetAll()
        {
            try
            {
                var venues = _context.Venues.Include(b => b.User);
                Debug.WriteLine($"Found {venues.Count()} venues");
                return venues;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching venues: {ex.Message}");
                return Enumerable.Empty<Venue>().AsQueryable();
            }
        }

        public async Task Add(Venue venue)
        {
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Venue venue)
        {
            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Venue venue)
        {
            _context.Venues.Update(venue);
            await _context.SaveChangesAsync();
        }

        public bool IsVenueHolder(string? userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return false;
            }

            return _context.Venues.Any(v => v.IdentityUserId == userID);
        }
    }
}