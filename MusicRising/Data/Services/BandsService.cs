using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services;

public class BandsService : IBandsService
{
    
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public BandsService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public IQueryable<Band> GetAll()
    {
        try
        {
            var bands = _context.Bands.Include(b => b.User);
            Debug.WriteLine($"Found {bands.Count()} bands");
            return bands;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching bands: {ex.Message}");
            return Enumerable.Empty<Band>().AsQueryable();
        }
    }

    public async Task Add(Band band)
    {
        _context.Bands.Add(band);
        await _context.SaveChangesAsync();
    }

    public Task Delete(Band band)
    {
        throw new NotImplementedException();
    }

    public Task Update(Band band)
    {
        throw new NotImplementedException();
    }
}