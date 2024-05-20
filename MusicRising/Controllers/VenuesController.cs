using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicRising.Data.Services;
using MusicRising.Helpers;
using MusicRising.Models;

namespace MusicRising.Controllers
{
    public class VenuesController : Controller
    {
        private readonly IVenuesService _venuesService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VenuesController(IVenuesService venuesService, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _venuesService = venuesService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            var venues = await _venuesService.GetAll().ToListAsync();
            Debug.WriteLine($"Found {venues.Count} venues in the Index method.");
            return View(venues);
        }

        // GET: Venues/Landing
        public async Task<IActionResult> Landing()
        {
            var userId = _userManager.GetUserId(User);
            var userVenues = await _venuesService.GetAll()
                .Where(v => v.IdentityUserId == userId)
                .ToListAsync();

            return View(userVenues);
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _venuesService.GetAll()
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.VenueId == id);
            if (venue == null)
            {
                return NotFound();
            }

            // Check if the current user is the owner
            var isOwner = venue.IdentityUserId == _userManager.GetUserId(User);

            // Convert Venue to VenueVM
            var venueVM = new VenueVM
            {
                VenueId = venue.VenueId,
                IdentityUserId = venue.IdentityUserId,
                User = venue.User,
                VenueName = venue.VenueName,
                Image = null, // Assuming the image upload is not needed here, you may need to handle this differently
                ImageFileName = venue.VenuePicture,
                Location = venue.Location,
                Genre = venue.Genre,
                Shows = venue.Shows,
                PromoItems = venue.PromoItems,
                Ratings = venue.Ratings,
                BankAccount = venue.BankAccount,
                IsOwner = isOwner // Add this line
            };

            return View(venueVM);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_userManager.Users, "Id", "Id");
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VenueVM venue)
        {
            if (venue.Image != null)
            {
                string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, venue.Image);

                var venueObj = new Venue
                {
                    VenueId = Guid.NewGuid().ToString(),
                    IdentityUserId = venue.IdentityUserId,
                    User = venue.User,
                    VenueName = venue.VenueName,
                    VenuePicture = filePath,
                    Location = venue.Location,
                    Genre = venue.Genre,
                    BankAccount = venue.BankAccount
                };

                await _venuesService.Add(venueObj);

                return RedirectToAction("Index");
            }
            ViewData["IdentityUserId"] = new SelectList(_userManager.Users, "Id", "Id", venue.IdentityUserId);
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _venuesService.GetAll().FirstOrDefaultAsync(v => v.VenueId == id);
            if (venue == null)
            {
                return NotFound();
            }

            // Convert Venue to EntityEditVM
            var venueVM = new EntityEditVM
            {
                Id = venue.VenueId,
                IdentityUserId = venue.IdentityUserId,
                Name = venue.VenueName,
                PictureUrl = venue.VenuePicture,
                Location = venue.Location,
                Genre = venue.Genre,
                BankAccount = venue.BankAccount
            };

            ViewBag.Title = "Venue";
            return View("_EntityEdit", venueVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EntityEditVM venueVM)
        {
            if (id != venueVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var venue = await _venuesService.GetAll().FirstOrDefaultAsync(v => v.VenueId == id);
                    if (venue == null)
                    {
                        return NotFound();
                    }

                    if (venueVM.Picture != null)
                    {
                        string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, venueVM.Picture);
                        venue.VenuePicture = filePath;
                    }

                    venue.VenueName = venueVM.Name;
                    venue.Location = venueVM.Location;
                    venue.Genre = venueVM.Genre;
                    venue.BankAccount = venueVM.BankAccount;

                    await _venuesService.Update(venue);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venueVM.Id))
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
            ViewBag.Title = "Venue";
            return View(venueVM);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _venuesService.GetAll()
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.VenueId == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var venue = await _venuesService.GetAll().FirstOrDefaultAsync(v => v.VenueId == id);
            if (venue != null)
            {
                await _venuesService.Delete(venue);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(string id)
        {
            return _venuesService.GetAll().Any(e => e.VenueId == id);
        }
    }
}
