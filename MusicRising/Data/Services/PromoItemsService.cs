using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services;

public class PromoItemsService : IPromoItemsService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    
    public PromoItemsService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public IQueryable<PromoItem> GetAll()
    {
        var applicationDbContext = _context.PromoItems.Include(p => p.User);
        return applicationDbContext;
    }
}