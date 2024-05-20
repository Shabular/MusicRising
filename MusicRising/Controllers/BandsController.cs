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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public BandsController(IBandsService bandsService, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _bandsService = bandsService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // the webhost environment is to save the images

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

        // Details about a band needed a viewmodel
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

            // Check if the current user is the owner
            var isOwner = band.IdentityUserId == _userManager.GetUserId(User);

            // Convert Band to BandVM
            var bandVM = new BandVM
            {
                BandId = band.BandId,
                IdentityUserId = band.IdentityUserId,
                User = band.User,
                BandName = band.BandName,
                Image = null, // Assuming the image upload is not needed here, you may need to handle this differently
                ImageFileName = band.BandPicture,
                Location = band.Location,
                Genre = band.Genre,
                Shows = band.Shows,
                PromoItems = band.PromoItems,
                Ratings = band.Ratings,
                BankAccount = band.BankAccount,
                IsOwner = isOwner // Add this line
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BandVM band)
        {
            if (band.Image != null)
            {
                string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, band.Image);

                var bandObj = new Band
                {
                    BandId = Guid.NewGuid().ToString(), // Generate identifier to be used as bandd,
                    IdentityUserId = band.IdentityUserId,
                    User = band.User,
                    BandName = band.BandName,
                    BandPicture = filePath,
                    Location = band.Location,
                    Genre = band.Genre,
                    BankAccount = band.BankAccount
                };

                await _bandsService.Add(bandObj);

                return RedirectToAction("Index");
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

            // Convert Band to EntityEditVM
            var bandVM = new EntityEditVM
            {
                Id = band.BandId,
                IdentityUserId = band.IdentityUserId,
                Name = band.BandName,
                PictureUrl = band.BandPicture,
                Location = band.Location,
                Genre = band.Genre,
                BankAccount = band.BankAccount
            };

            ViewBag.Title = "Band";
            return View(bandVM);
        }

        // POST: Bands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EntityEditVM bandVM)
        {
            if (id != bandVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var band = await _bandsService.GetAll().FirstOrDefaultAsync(b => b.BandId == id);
                    if (band == null)
                    {
                        return NotFound();
                    }

                    if (bandVM.Picture != null)
                    {
                        string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, bandVM.Picture);
                        band.BandPicture = filePath;
                    }

                    band.BandName = bandVM.Name;
                    band.Location = bandVM.Location;
                    band.Genre = bandVM.Genre;
                    band.BankAccount = bandVM.BankAccount;

                    await _bandsService.Update(band);
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
                return RedirectToAction(nameof(Index));
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
                .FirstOrDefaultAsync(m => m.BandId == id);
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

        private bool BandExists(string id)
        {
            return _bandsService.GetAll().Any(e => e.BandId == id);
        }
    }
}
