using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicRising.Data;
using MusicRising.Data.Services;
using MusicRising.Models;

namespace MusicRising.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingsService _ratingsService;
        private readonly UserManager<IdentityUser> _userManager;


        public RatingsController(IRatingsService ratingsService)
        {
            _ratingsService = ratingsService;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _ratingsService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }

        /*// GET: Ratings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(r => r.Band)
                .Include(r => r.User)
                .Include(r => r.Venue)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingId,IdentityUserId,BandId,VenueId,Stars,Comment,Date")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId", rating.BandId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", rating.IdentityUserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", rating.VenueId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId", rating.BandId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", rating.IdentityUserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", rating.VenueId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RatingId,IdentityUserId,BandId,VenueId,Stars,Comment,Date")] Rating rating)
        {
            if (id != rating.RatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId", rating.BandId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", rating.IdentityUserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", rating.VenueId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(r => r.Band)
                .Include(r => r.User)
                .Include(r => r.Venue)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(string id)
        {
            return _context.Ratings.Any(e => e.RatingId == id);
        }*/
    }
}
