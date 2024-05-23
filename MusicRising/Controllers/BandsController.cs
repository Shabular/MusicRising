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
    public class BandsController : Controller
    {
        private readonly IBandsService _bandsService;
        private readonly IVenuesService _venuesService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DebugHelper _debugHelper = new DebugHelper();

        public BandsController(IBandsService bandsService, UserManager<IdentityUser> userManager,
            IWebHostEnvironment webHostEnvironment, IVenuesService venuesService)
        {
            _bandsService = bandsService;
            _venuesService = venuesService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Bands
        public async Task<IActionResult> Index()
        {
            var bands = await _bandsService.GetAll().ToListAsync();
            Debug.WriteLine($"Found {bands.Count} bands in the Index method.");
            return View(bands);
        }

        // GET: Bands/Landing
        public async Task<IActionResult> Landing()
        {
            var userId = _userManager.GetUserId(User);
            var userBands = await _bandsService.GetAll()
                .Where(b => b.IdentityUserId == userId)
                .ToListAsync();

            return View(userBands);
        }

        // GET: Bands/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _bandsService.GetAll()
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            var bandVM = new BandVM
            {
                BandId = band.BandId,
                IdentityUserId = band.IdentityUserId,
                User = band.User,
                BandName = band.BandName,
                Image = null,
                ImageFileName = band.BandPicture,
                Location = band.Location,
                Genre = band.Genre,
                Shows = band.Shows,
                PromoItems = band.PromoItems,
                Ratings = band.Ratings,
                Address = band.Address,
                Latitude = band.Latitude,
                Longitude = band.Longitude,
                IsOwner = band.IdentityUserId == _userManager.GetUserId(User)
            };

            return View(bandVM);
        }

        // GET: Bands/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_userManager.Users, "Id", "Id");
            return View();
        }

        // POST: Bands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BandVM band)
        {
            if (band.Image != null)
            {
                string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, band.Image);
                var bandCoordinates = await GeocodingHelper.GetCoordinatesAsync(band.Address);

                var bandObj = new Band
                {
                    BandId = Guid.NewGuid().ToString(),
                    IdentityUserId = band.IdentityUserId,
                    User = band.User,
                    BandName = band.BandName,
                    BandPicture = filePath,
                    Location = band.Location,
                    Genre = band.Genre,
                    Latitude = bandCoordinates.Latitude,
                    Longitude = bandCoordinates.Longitude,
                    Address = band.Address
                };

                await _bandsService.Add(bandObj);
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdentityUserId"] = new SelectList(_userManager.Users, "Id", "Id", band.IdentityUserId);
            return View(band);
        }

        // GET: Bands/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _bandsService.GetAll().FirstOrDefaultAsync(b => b.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            var bandVM = new BandVM
            {
                BandId = band.BandId,
                IdentityUserId = band.IdentityUserId,
                BandName = band.BandName,
                ImageFileName = band.BandPicture,
                Location = band.Location,
                Address = band.Address,
                Genre = band.Genre,
                Latitude = band.Latitude,
                Longitude = band.Longitude,
                IsOwner = band.IdentityUserId == _userManager.GetUserId(User)
            };

            ViewBag.Title = "Band";
            return View(bandVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BandVM bandVM)
        {
            _debugHelper.DebugWriteLine($"Received edit request for band ID: {id}");
            if (id != bandVM.BandId)
            {
                return NotFound();
            }

            _debugHelper.DebugWriteLine("the picture url = " + bandVM.ImageFileName);

            if (ModelState.IsValid)
            {
                try
                {
                    var band = await _bandsService.GetAll().FirstOrDefaultAsync(b => b.BandId == id);
                    if (band == null)
                    {
                        _debugHelper.DebugWriteLine("band was not found");
                        return NotFound();
                    }

                    if (bandVM.Image != null)
                    {
                        _debugHelper.DebugWriteLine("foto update is there");
                        string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, bandVM.Image);
                        bandVM.ImageFileName = filePath;
                        band.BandPicture = filePath;
                    }

                    var bandCoordinates = await GeocodingHelper.GetCoordinatesAsync(bandVM.Address);

                    band.BandName = bandVM.BandName;
                    band.Location = bandVM.Location;
                    band.Genre = bandVM.Genre;
                    band.Latitude = bandCoordinates.Latitude;
                    band.Longitude = bandCoordinates.Longitude;
                    band.Address = bandVM.Address;

                    _debugHelper.DebugWriteLine("before update band");

                    await _bandsService.Update(band);
                    _debugHelper.DebugWriteLine("after update band");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandExists(bandVM.BandId))
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
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _debugHelper.DebugWriteLine($"ModelState error: {error.ErrorMessage}");
                }
            }

            ViewBag.Title = "Band";
            return View(bandVM);
        }

        // GET: Bands/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _bandsService.GetAll()
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            return View(band);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var band = await _bandsService.GetAll().FirstOrDefaultAsync(b => b.BandId == id);
            if (band != null)
            {
                await _bandsService.Delete(band);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Bands/BookShow/5
        public async Task<IActionResult> BookShow(string bandID)
        {
            var band = await _bandsService.GetAll().FirstOrDefaultAsync(b => b.BandId == bandID);
            ViewBag.Title = "Show";
            return await Task.FromResult<IActionResult>(RedirectToAction("Create", "Shows", band));
        }

        private bool BandExists(string id)
        {
            return _bandsService.GetAll().Any(e => e.BandId == id);
        }
    }
}

