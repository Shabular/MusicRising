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
    public class PromoItemsController : Controller
    {
        private readonly IPromoItemsService _promoItemsService;
        private readonly UserManager<IdentityUser> _userManager;

        
        public PromoItemsController(IPromoItemsService promoItemsService)
        {
            _promoItemsService = promoItemsService;
        }

        // GET: PromoItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _promoItemsService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }

        /*
         *
         * // GET: PromoItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoItem = await _context.PromoItems
                .Include(p => p.Band)
                .Include(p => p.User)
                .Include(p => p.Venue)
                .FirstOrDefaultAsync(m => m.PromoItemId == id);
            if (promoItem == null)
            {
                return NotFound();
            }

            return View(promoItem);
        }

        // GET: PromoItems/Create
        public IActionResult Create()
        {
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId");
            return View();
        }

        // POST: PromoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromoItemId,IdentityUserId,BandId,VenueId,Link")] PromoItem promoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId", promoItem.BandId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", promoItem.IdentityUserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", promoItem.VenueId);
            return View(promoItem);
        }

        // GET: PromoItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoItem = await _context.PromoItems.FindAsync(id);
            if (promoItem == null)
            {
                return NotFound();
            }
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId", promoItem.BandId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", promoItem.IdentityUserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", promoItem.VenueId);
            return View(promoItem);
        }

        // POST: PromoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PromoItemId,IdentityUserId,BandId,VenueId,Link")] PromoItem promoItem)
        {
            if (id != promoItem.PromoItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromoItemExists(promoItem.PromoItemId))
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
            ViewData["BandId"] = new SelectList(_context.Bands, "BandId", "BandId", promoItem.BandId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", promoItem.IdentityUserId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", promoItem.VenueId);
            return View(promoItem);
        }

        // GET: PromoItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoItem = await _context.PromoItems
                .Include(p => p.Band)
                .Include(p => p.User)
                .Include(p => p.Venue)
                .FirstOrDefaultAsync(m => m.PromoItemId == id);
            if (promoItem == null)
            {
                return NotFound();
            }

            return View(promoItem);
        }

        // POST: PromoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var promoItem = await _context.PromoItems.FindAsync(id);
            if (promoItem != null)
            {
                _context.PromoItems.Remove(promoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromoItemExists(string id)
        {
            return _context.PromoItems.Any(e => e.PromoItemId == id);
        }*/
    }
}
