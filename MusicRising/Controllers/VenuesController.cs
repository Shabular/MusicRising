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
    // comments one how this works are in bandcontroller
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
        public async Task<IActionResult> Index(string location, string genre, bool liked = false)
        {
            var venues = await _venuesService.GetAll().ToListAsync();

            if (!string.IsNullOrEmpty(location))
            {
                venues = venues.Where(v => v.Location.ToString() == location).ToList();
            }

            if (!string.IsNullOrEmpty(genre))
            {
                venues = venues.Where(v => v.Genre.ToString() == genre).ToList();
            }

            // not implemented jet provisions for
            /*if (liked)
            {
                venues = venues.Where(v => v.Liked).ToList(); // Assuming Liked is a boolean property in the Venue model
            }*/

            ViewData["Location"] = new SelectList(Enum.GetValues(typeof(LocationEnum)).Cast<LocationEnum>());
            ViewData["Genre"] = new SelectList(Enum.GetValues(typeof(GenreEnum)).Cast<GenreEnum>());
            ViewData["Liked"] = true;

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

            var venueVM = new VenueVM
            {
                VenueId = venue.VenueId,
                IdentityUserId = venue.IdentityUserId,
                User = venue.User,
                VenueName = venue.VenueName,
                Image = null,
                ImageFileName = venue.VenuePicture,
                Location = venue.Location,
                Genre = venue.Genre,
                Shows = venue.Shows,
                PromoItems = venue.PromoItems,
                Ratings = venue.Ratings,
                IsOwner = venue.IdentityUserId == _userManager.GetUserId(User)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VenueVM venue)
        {
            if (venue.Image != null)
            {
                string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, venue.Image);

                var VenueCoordinates = await GeocodingHelper.GetCoordinatesAsync(venue.Address);
                
                var venueObj = new Venue
                {
                    VenueId = Guid.NewGuid().ToString(),
                    IdentityUserId = venue.IdentityUserId,
                    User = venue.User,
                    VenueName = venue.VenueName,
                    VenuePicture = filePath,
                    Location = venue.Location,
                    Genre = venue.Genre,
                    Latitude = VenueCoordinates.Latitude,
                    Longitude = VenueCoordinates.Longitude,
                    Address = VenueCoordinates.Address
                };

                await _venuesService.Add(venueObj);
                return RedirectToAction(nameof(Index));
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

            var venueVM = new EntityEditVM
            {
                Id = venue.VenueId,
                IdentityUserId = venue.IdentityUserId,
                Name = venue.VenueName,
                PictureUrl = venue.VenuePicture,
                Location = venue.Location,
                Address = venue.Address,
                Genre = venue.Genre,
                IsOwner = venue.IdentityUserId == _userManager.GetUserId(User)
            };

            ViewBag.Title = "Venue";
            return View("_EntityEdit", venueVM);
        }

        // POST: Venues/Edit/5
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
                        string filePath = ImageHelper.UpdateImageOnServer(_webHostEnvironment, venueVM.Picture, venueVM.PictureUrl);
                        venue.VenuePicture = filePath;
                    }
                    
                    var VenueCoordinates = await GeocodingHelper.GetCoordinatesAsync(venue.Address);
                    

                    venue.VenueName = venueVM.Name;
                    venue.Location = venueVM.Location;
                    venue.Genre = venueVM.Genre;
                   
                    venue.Latitude = VenueCoordinates.Latitude;
                    venue.Longitude = VenueCoordinates.Longitude;
                    venue.Address = VenueCoordinates.Address;

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
            return View("_EntityEdit", venueVM);
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
