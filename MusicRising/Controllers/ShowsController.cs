using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicRising.Data.Services;
using MusicRising.Models;

namespace MusicRising.Controllers
{
    public class ShowsController : Controller
    {
        private readonly IShowsService _showsService;
        private readonly UserManager<IdentityUser> _userManager;

        public ShowsController(IShowsService showsService, UserManager<IdentityUser> userManager)
        {
            _showsService = showsService;
            _userManager = userManager;
        }

        // GET: Shows
        public async Task<IActionResult> Index()
        {
            var shows = await _showsService.GetAll().ToListAsync();
            return View(shows);
        }

        // GET: Shows/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _showsService.GetAll()
                .Include(s => s.Venue)
                .Include(s => s.HeadLiner)
                .FirstOrDefaultAsync(s => s.ShowId == id);
            if (show == null)
            {
                return NotFound();
            }

            var showVM = new ShowVM
            {
                ShowId = show.ShowId,
                VenueId = show.VenueId,
                Venue = show.Venue,
                BandId = show.BandId,
                HeadLiner = show.HeadLiner,
                Genre = show.Genre,
                Date = show.Date,
                PromoItem = show.PromoLink,
                ShowFee = show.ShowFee,
                BandFee = show.BandFee,
                Payed = show.Payed,
                IsOwner = show.Venue.IdentityUserId == _userManager.GetUserId(User)
            };

            return View(showVM);
        }

        // GET: Shows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shows/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShowVM show)
        {
            if (show.BandId != null)
            {
                var showObj = new Show
                {
                    ShowId = Guid.NewGuid().ToString(),
                    VenueId = show.VenueId,
                    BandId = show.BandId,
                    Genre = show.Genre,
                    Date = show.Date,
                    PromoLink = show.PromoItem,
                    ShowFee = show.ShowFee,
                    BandFee = show.BandFee,
                    Payed = show.Payed
                };
                await _showsService.Add(showObj);
                return RedirectToAction(nameof(Index));
            }

            return View(show);
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _showsService.GetAll().FirstOrDefaultAsync(s => s.ShowId == id);
            if (show == null)
            {
                return NotFound();
            }

            var showVM = new ShowVM
            {
                ShowId = show.ShowId,
                VenueId = show.VenueId,
                Venue = show.Venue,
                BandId = show.BandId,
                HeadLiner = show.HeadLiner,
                Genre = show.Genre,
                Date = show.Date,
                PromoItem = show.PromoLink,
                ShowFee = show.ShowFee,
                BandFee = show.BandFee,
                Payed = show.Payed,
                IsOwner = show.Venue.IdentityUserId == _userManager.GetUserId(User)
            };

            return View(showVM);
        }

        // POST: Shows/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ShowVM showVM)
        {
            if (id != showVM.ShowId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var show = await _showsService.GetAll().FirstOrDefaultAsync(s => s.ShowId == id);
                    if (show == null)
                    {
                        return NotFound();
                    }

                    show.VenueId = showVM.VenueId;
                    show.BandId = showVM.BandId;
                    show.Genre = showVM.Genre;
                    show.Date = showVM.Date;
                    show.PromoLink = showVM.PromoItem;
                    show.ShowFee = showVM.ShowFee;
                    show.BandFee = showVM.BandFee;
                    show.Payed = showVM.Payed;

                    await _showsService.Update(show);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(showVM.ShowId))
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
            return View(showVM);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _showsService.GetAll()
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(s => s.ShowId == id);
            if (show == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (show.Venue.IdentityUserId != userId)
            {
                return Forbid();
            }

            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var show = await _showsService.GetAll().FirstOrDefaultAsync(s => s.ShowId == id);
            if (show != null)
            {
                var userId = _userManager.GetUserId(User);
                if (show.Venue.IdentityUserId != userId)
                {
                    return Forbid();
                }

                await _showsService.Delete(show);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(string id)
        {
            return _showsService.GetAll().Any(e => e.ShowId == id);
        }
    }
}
