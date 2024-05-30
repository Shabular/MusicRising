using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services;

public class RatingsService : IRatingsService
{

    
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    
    public RatingsService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public IQueryable<Rating> GetAll()
    {
        var applicationDbContext = _context.Ratings.Include(p => p.User);
        return applicationDbContext;
    }
}