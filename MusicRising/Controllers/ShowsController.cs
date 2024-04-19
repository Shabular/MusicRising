using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicRising.Data;
using MusicRising.Data.Services;
using MusicRising.Models;

namespace MusicRising.Controllers
{
    public class ShowsController : Controller
    {
        private readonly IShowsService _showsService;

        public ShowsController(IShowsService showsService)
        {
            _showsService = showsService;
        }

        // GET: Shows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _showsService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }

        /*// GET: Shows/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // GET: Shows/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Bands, "Id", "Id");
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Id");
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VenueId,BandID,Genre,Date,PromoLink,ShowFee")] Show show)
        {
            if (ModelState.IsValid)
            {
                _context.Add(show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Bands, "Id", "Id", show.Id);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Id", show.VenueId);
            return View(show);
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Bands, "Id", "Id", show.Id);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Id", show.VenueId);
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,VenueId,BandID,Genre,Date,PromoLink,ShowFee")] Show show)
        {
            if (id != show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.Id))
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
            ViewData["Id"] = new SelectList(_context.Bands, "Id", "Id", show.Id);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Id", show.VenueId);
            return View(show);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show != null)
            {
                _context.Shows.Remove(show);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(string id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }*/
    }
}
