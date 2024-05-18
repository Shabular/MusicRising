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
        var applicationDbContext = _context.Bands.Include(b => b.User);
        return applicationDbContext;
    }

    public async Task Add(Band band)
    {
        _context.Bands.Add(band);
        await _context.SaveChangesAsync();
    }

   
}