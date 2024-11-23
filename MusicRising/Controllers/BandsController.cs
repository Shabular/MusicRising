using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
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
        // when a selection button is clicked filter for that selection and reload view
        //bool liked will be used to show liked bands or v enues only but that it not jet inplemented
        public async Task<IActionResult> Index(string location, string genre, bool liked = false)
        {
            var bands = await _bandsService.GetAll().ToListAsync();

            if (!string.IsNullOrEmpty(location))
            {
                bands = bands.Where(b => b.Location.ToString() == location).ToList();
            }

            if (!string.IsNullOrEmpty(genre))
            {
                bands = bands.Where(b => b.Genre.ToString() == genre).ToList();
            }

            // 
            /*if (liked)
            {
                // if user has band in his liked list.....//
            }*/

            ViewData["Location"] = new SelectList(Enum.GetValues(typeof(LocationEnum)).Cast<LocationEnum>());
            ViewData["Genre"] = new SelectList(Enum.GetValues(typeof(GenreEnum)).Cast<GenreEnum>());
            ViewData["Liked"] = true;

            return View(bands);
        }

        // GET: Bands/Landing
        // this is used to show all bands a user has or go and create one
        public async Task<IActionResult> Landing()
        {
            var userId = _userManager.GetUserId(User);
            var userBands = await _bandsService.GetAll()
                .Where(b => b.IdentityUserId == userId)
                .ToListAsync();
            
                return View(userBands);

        }

        // GET: Bands/Details/5
        // if the user is the bandmember that registered this band he can edit or delete
        
        public async Task<IActionResult> Details(string id)
        {
            // tu use this the band should exist
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
            

            // create viewmodel to get beter / other data then in model
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
                Details = band.Details,
                Address = band.Address,
                Latitude = band.Latitude,
                Longitude = band.Longitude,
                IsOwner = band.IdentityUserId == _userManager.GetUserId(User),
                CanBeBooked = _venuesService.IsVenueHolder(_userManager.GetUserId(User))
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
        // we use a bandvm to present and get data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BandVM band)
        {
            if (_userManager.GetUserId(User) == null) return RedirectToAction(nameof(Index));

            
            // if you create a band you should upload image
            if (band.Image != null)
            {
                string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, band.Image);
                _debugHelper.DebugWriteLine("filepath = " + filePath);
                _debugHelper.DebugWriteLine("image name = " + band.Image.FileName);
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
                    Details = band.Details,
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
            
            if (!AuthHelper.Authorize(_userManager.GetUserId(User), band.IdentityUserId))
            {
                return RedirectToAction(nameof(Index));
            }

            var bandVM = new EntityEditVM
            {
                Id = band.BandId,
                IdentityUserId = band.IdentityUserId,
                Name = band.BandName,
                PictureUrl = band.BandPicture,
                Location = band.Location,
                Address = band.Address,
                Genre = band.Genre,
                Details = band.Details,
                IsOwner = band.IdentityUserId == _userManager.GetUserId(User)
            };

            ViewBag.Title = "Band";
            return View("_EntityEdit", bandVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EntityEditVM bandVM)
        {
            if (!AuthHelper.Authorize(_userManager.GetUserId(User), bandVM.IdentityUserId))
            {
                return RedirectToAction(nameof(Index));
            }
            _debugHelper.DebugWriteLine($"Received edit request for band ID: {id}");
            // we do this to make shure someone did not get on this page by filling random url
            if (id != bandVM.Id)
            {
                return NotFound();
            }

            // debugging message to know if there is something wrong when uploading and image is not shown
            _debugHelper.DebugWriteLine("the picture url = " + bandVM.PictureUrl);

            if (bandVM != null)
            {
                try
                {
                    var band = await _bandsService.GetAll().FirstOrDefaultAsync(b => b.BandId == id);
                    if (band == null)
                    {
                        _debugHelper.DebugWriteLine("band was not found");
                        return NotFound();
                    }

                    if (bandVM.Picture != null)
                    {
                        _debugHelper.DebugWriteLine("foto update is there");
                        string filePath = ImageHelper.UpdateImageOnServer(_webHostEnvironment, bandVM.Picture, bandVM.PictureUrl);
                        bandVM.PictureUrl = filePath;
                    }

                    var bandCoordinates = await GeocodingHelper.GetCoordinatesAsync(bandVM.Address);

                    band.BandName = bandVM.Name;
                    band.Location = bandVM.Location;
                    band.Genre = bandVM.Genre;
                    band.BandPicture = bandVM.PictureUrl;
                    band.Latitude = bandCoordinates.Latitude;
                    band.Longitude = bandCoordinates.Longitude;
                    band.Details = bandVM.Details;
                    band.BankAccount = "NotImplementedJet";
                    band.Address = bandVM.Address;

                    _debugHelper.DebugWriteLine("before update band");

                    await _bandsService.Update(band);
                    _debugHelper.DebugWriteLine("after update band");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandExists(bandVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Landing));
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
            
            if (!AuthHelper.Authorize(_userManager.GetUserId(User), band.IdentityUserId))
            {
                return RedirectToAction(nameof(Index));
            }
            
            if (band == null)
            {
                return NotFound();
            }

            return View(id);
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

