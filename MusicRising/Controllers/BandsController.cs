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

        // GET: Bands
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _bandsService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }
        
        // GET: Bands/Landing
        public async Task<IActionResult> Landing()
        {
            var userId = _userManager.GetUserId(User);
            var userBands = await _bandsService.GetAll()
                .Where(b => b.BandId == userId)
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
                .FirstOrDefaultAsync(m => m.BandId == id);
            if (band == null)
            {
                return NotFound();
            }

            return View(band);
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
                // get the filnamen and add to the foldername to create a place to store band/venue pics
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string fileName = Guid.NewGuid().ToString() + "_" + band.Image.FileName; // this is used to be able to have multiple same filenames just in case
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    band.Image.CopyTo(fileStream);
                }

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
/*p
        // GET: Bands/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Bands.FindAsync(id);
            if (band == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", band.IdentityUserId);
            return View(band);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BandId,IdentityUserId,BandName,BandPicture,Location,Genre,BankAccount")] Band band)
        {
            if (id != band.BandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(band);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandExists(band.BandId))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", band.IdentityUserId);
            return View(band);
        }

        // GET: Bands/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var band = await _context.Bands
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
            var band = await _context.Bands.FindAsync(id);
            if (band != null)
            {
                _context.Bands.Remove(band);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandExists(string id)
        {
            return _context.Bands.Any(e => e.BandId == id);
        }*/
    }
}
