using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services;

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
            Debug.WriteLine($"Found {venues.Count()} bands");
            return venues;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching bands: {ex.Message}");
            return Enumerable.Empty<Venue>().AsQueryable();
        }
    }

    public async Task Add(Venue venue)
    {
        _context.Venues.Add(venue);
        await _context.SaveChangesAsync();
    }

    public Task Delete(Venue venue)
    {
        throw new NotImplementedException();
    }

    public Task Update(Venue venue)
    {
        throw new NotImplementedException();
    }
}