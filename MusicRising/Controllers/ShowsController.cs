using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using MusicRising.Data.Services;
using MusicRising.Helpers;
using MusicRising.Models;

namespace MusicRising.Controllers
{
    
    // due to the fact this works ( almost) like the bandcontroller go there for comments
    
    public class ShowsController : Controller
    {
        private readonly IShowsService _showsService;
        private readonly IBandsService _bandsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IVenuesService _venuesService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DebugHelper _debugHelper = new DebugHelper();

        public ShowsController(IShowsService showsService, IBandsService bandsService, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, IVenuesService venuesService)
        {
            _showsService = showsService;
            _userManager = userManager;
            _bandsService = bandsService;
            _venuesService = venuesService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Shows
        public async Task<IActionResult> Index(string location, string genre, bool liked = false)
        {
            var shows = await _showsService.GetAll().Where(s => s.Booked.ToString() == "true").ToListAsync();


            if (!string.IsNullOrEmpty(location))
            {
                shows = shows.Where(s => s.Venue.Location.ToString() == location).ToList();
            }

            if (!string.IsNullOrEmpty(genre))
            {
                shows = shows.Where(s => s.Genre.ToString() == genre).ToList();
            }

            // not implemented jet
            /*if (liked)
            {
                shows = shows.Where(s => s.Liked).ToList(); // see bandcontroller for specialized comments
            }*/

            ViewData["Location"] = new SelectList(Enum.GetValues(typeof(LocationEnum)).Cast<LocationEnum>());
            ViewData["Genre"] = new SelectList(Enum.GetValues(typeof(GenreEnum)).Cast<GenreEnum>());
            ViewData["Liked"] = true;

            return View(shows);
        }
        
        // GET: Shows/Landing
        public async Task<IActionResult> Landing()
        {
            var userId = _userManager.GetUserId(User);

            // Fetch user's venues
            var userVenues = await _venuesService.GetAll()
                .Where(v => v.IdentityUserId == userId)
                .Select(v => v.VenueId)
                .ToListAsync();

            // Fetch user's bands
            var userBands = await _bandsService.GetAll()
                .Where(b => b.IdentityUserId == userId)
                .Select(b => b.BandId)
                .ToListAsync();

            // Fetch shows for user's venues
            var venueShows = await _showsService.GetAll()
                .Where(s => userVenues.Contains(s.VenueId))
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .ToListAsync();

            // Fetch shows for user's bands
            var bandShows = await _showsService.GetAll()
                .Where(s => userBands.Contains(s.BandId))
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .ToListAsync();

            // Combine and remove duplicates
            var allShows = venueShows.Union(bandShows).Distinct().ToList();

            // Create a list of ShowVM
            var showVMList = allShows.Select(show => new ShowVM
            {
                ShowId = show.ShowId,
                VenueId = show.VenueId,
                Venue = show.Venue,
                BandId = show.BandId,
                HeadLiner = show.HeadLiner,
                Genre = show.Genre,
                Date = show.Date,
                PromoLink = show.PromoLink,
                ShowFee = show.ShowFee,
                BandFee = show.BandFee,
                Booked = show.Booked,
                IsVenueOwner = userVenues.Contains(show.VenueId),
                IsBandMember = userBands.Contains(show.BandId)
            }).ToList();

            return View(showVMList);
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
                Details = show.Details,
                PromoLink = show.PromoLink,
                ShowFee = show.ShowFee,
                BandFee = show.BandFee,
                Booked = show.Booked,
                IsVenueOwner = show.IsVenueOwner = show.Venue.IdentityUserId == _userManager.GetUserId(User),
                IsBandMember = show.IsBandMember = show.Venue.IdentityUserId == _userManager.GetUserId(User)
            };

            return View(showVM);
        }

        // GET: Shows/Create
        public async Task<IActionResult> Create(Band band)
        {
            string bandID = band.BandId;
            _debugHelper.DebugWriteLine("In create band");
            _debugHelper.DebugWriteLine("bandID = " + bandID);
            
            // we are here bandID is not null anymore!!
            if (bandID == null)
            {
                return NotFound();
            }

            string userID = _userManager.GetUserId(User);

            
            var venue = await _venuesService.GetAll().FirstOrDefaultAsync(v => v.IdentityUserId == userID);
            List<Venue> venueList = new List<Venue>(_venuesService.GetAll().Where(v => v.IdentityUserId == userID));
            
            
            if ((band == null) | (venue == null))
            {
                _debugHelper.DebugWriteLine("band or venue was null" );
                return NotFound();
            }
            
            

            _debugHelper.DebugWriteLine("create show vm");
            _debugHelper.DebugWriteLine("VenueList" + venueList);
            // create a show item and fill with everything of the show and venue
            var showVM = new ShowVM(){
                ShowId = Guid.NewGuid().ToString(),
                VenueId = venue.VenueId,
                BandId = band.BandId,
                Genre = band.Genre,
                Details = "",
                Date = DateTime.Now,
                PromoLink = null,
                ShowFee = null,
                Booked=  false
            };
            

            // we need a bookingVM because we should be able to select which venue we want to book them on
            // in this bookingVM we need the showVM and a list of venues owned by booker
            // then we put the venue in the show and book the show?

            BookingVM bookingVM = new BookingVM()
            {
                showVM = showVM,
                venues = venueList
            };
            
            return View(bookingVM );
        }

        // POST: Shows/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingVM bookingVm )
        {
            _debugHelper.DebugWriteLine("In create show with bookingvm");
            _debugHelper.DebugWriteLine("bandID = " + bookingVm.showVM.BandId);

            string filePath = ImageHelper.SaveImageToServer(_webHostEnvironment, bookingVm.showVM.PromoItem);
            
            // we are here the form is submitted and something is put in the database
            // we now get data from the list of venues a venue owner has
            if (bookingVm.showVM.BandId != null)
            {
                var showObj = new Show
                {
                    ShowId = bookingVm.showVM.ShowId,
                    VenueId = bookingVm.showVM.VenueId,
                    BandId = bookingVm.showVM.BandId,
                    Genre = bookingVm.showVM.Genre,
                    Date = bookingVm.showVM.Date,
                    PromoLink = filePath,
                    Details = bookingVm.showVM.Details,
                    ShowFee = bookingVm.showVM.ShowFee,
                    BandFee = bookingVm.showVM.BandFee,
                    BidByVenue = true,
                    Booked = false
                };
                await _showsService.Add(showObj);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(string id)
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

            var showVM = new EntityEditVM()
            {
                Id = show.ShowId,
                Venue = show.Venue ?? new Venue(), // Ensure Venue is not null
                HeadLiner = show.HeadLiner ?? new Band(), // Ensure HeadLiner is not null
                HeadLinerID = show.HeadLiner?.BandId,
                HeadLinerName = show.HeadLiner?.BandName,
                IdentityUserId = show.Venue.IdentityUserId,
                PictureUrl = show.PromoLink,
                Location = show.Venue.Location,
                Address = show.Venue.Address,
                Genre = show.HeadLiner?.Genre ?? GenreEnum.Unknown, // Assuming GenreEnum.Unknown as default
                Date = show.Date,
                ShowFee = show.ShowFee,
                Details = show.Details,
                BandFee = show.BandFee,
                Booked = show.Booked,
                IsOwner = show.Venue.IdentityUserId == _userManager.GetUserId(User)
            };
            _debugHelper.DebugWriteLine("headliner name show " + (show.HeadLiner?.BandName ?? "Unknown"));
            _debugHelper.DebugWriteLine("headliner name showVM " + (showVM.HeadLiner?.BandName ?? "Unknown"));
    
            return View("_EntityEdit", showVM);
        }


        
        // POST: Shows/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string view, EntityEditVM showVM)
        {
            try
            {
                _debugHelper.DebugWriteLine("headliner name " + (showVM.HeadLiner?.BandName ?? "Unknown"));
        
                var show = await _showsService.GetAll()
                    .Include(s => s.HeadLiner)
                    .Include(s => s.Venue)
                    .FirstOrDefaultAsync(s => s.ShowId == showVM.Id);
                if (show == null)
                {
                    _debugHelper.DebugWriteLine("Show not found");
                    return NotFound();
                }

                string filePath = showVM.PictureUrl;

                if (showVM.Picture != null)
                {
                    _debugHelper.DebugWriteLine("Before update show image");
                    filePath = ImageHelper.UpdateImageOnServer(_webHostEnvironment, showVM.Picture, showVM.PictureUrl);
                }

                show.HeadLiner = showVM.HeadLiner ?? show.HeadLiner;
                show.BandId = showVM.HeadLinerID ?? show.BandId; // Ensure BandId is not overwritten if null
                show.Genre = showVM.Genre;
                show.Date = showVM.Date ?? show.Date;
                show.PromoLink = filePath;
                show.ShowFee = showVM.ShowFee;
                show.Details = showVM.Details;
                show.BandFee = showVM.BandFee ?? show.BandFee;
                show.Booked = showVM.Booked ?? show.Booked;

                _debugHelper.DebugWriteLine("Before update show");
                await _showsService.Update(show);
            }
            catch (DbUpdateConcurrencyException)
            {
                _debugHelper.DebugWriteLine("Database error");
                throw;
            }
            _debugHelper.DebugWriteLine("return to index from edit show");
            return RedirectToAction(nameof(Index));
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
        
        [HttpGet]
        public async Task<IActionResult> Bid(string id)
        {
            _debugHelper.DebugWriteLine("Biding get got showID" + id);
            var show = await _showsService.GetAll()
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(s => s.ShowId == id);
            if (show == null)
            {
                _debugHelper.DebugWriteLine("Biding get has not found show");
                return NotFound("Show not found.");
            }

            string userID = _userManager.GetUserId(User);
            var bandId = show.BandId;
            var venueId = show.VenueId;

            
            

            var showVM = new ShowVM
            {
                ShowId = show.ShowId,
                VenueId = show.VenueId,
                Venue = show.Venue,
                BandId = show.BandId,
                HeadLiner = show.HeadLiner,
                Genre = show.Genre,
                Date = show.Date,
                PromoLink = show.PromoLink,
                Details = show.Details,
                ShowFee = show.ShowFee,
                BandFee = show.BandFee,
                Booked = show.Booked,
                BidByVenue = show.BidByVenue,
                IsVenueOwner = show.IsVenueOwner,
                IsBandMember = show.IsBandMember
            };

            if (userID == showVM.HeadLiner.IdentityUserId)
            {
                showVM.IsBandMember = true;
            }
            else
            {
                showVM.IsBandMember = false;
            }
           
            if (userID == showVM.Venue.IdentityUserId)
            {
                showVM.IsVenueOwner = true;
            }
            else
            {
                showVM.IsVenueOwner = false;
            }
            
            
            _debugHelper.DebugWriteLine("by venue: " + showVM.BidByVenue);
            _debugHelper.DebugWriteLine("band member: " + showVM.IsBandMember);
            _debugHelper.DebugWriteLine("venue owner: " + showVM.IsVenueOwner);
            
            
            return View(showVM);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Bid(string showId, double bidAmount)
        {
            var show = await _showsService.GetAll()
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(s => s.ShowId == showId);
            if (show == null)
            {
                _debugHelper.DebugWriteLine("Biding post has not found show");
                return NotFound("Show not found.");
            }

            show.BandFee = bidAmount;
            show.Booked = false;
            show.BidByVenue = !show.BidByVenue;

            await _showsService.Update(show);
            return RedirectToAction(nameof(Landing));
        }
        
        [HttpPost]
        public async Task<IActionResult> BidAccept(string showId)
        {
            _debugHelper.DebugWriteLine("accepting bid");
            var show = await _showsService.GetAll()
                .Include(s => s.HeadLiner)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(s => s.ShowId == showId);
            if (show == null)
            {
                _debugHelper.DebugWriteLine("Biding post has not found show");
                return NotFound("Show not found.");
            }

            show.Booked = true;

            await _showsService.Update(show);
            return RedirectToAction(nameof(Landing));
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
