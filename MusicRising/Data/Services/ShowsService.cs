using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services
{
    public class ShowsService : IShowsService
    {
        private readonly ApplicationDbContext _context;

        public ShowsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Show> GetAll()
        {
            var applicationDbContext = _context.Shows.Include(s => s.HeadLiner).Include(s => s.Venue);
            return applicationDbContext;
        }

        public async Task Add(Show show)
        {
            _context.Shows.Add(show);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Show show)
        {
            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Show show)
        {
            _context.Shows.Update(show);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Show> GetShowByIdAsync(string id)
        {
            return await _context.Shows.FirstOrDefaultAsync(s => s.ShowId == id);
        }
    }
}