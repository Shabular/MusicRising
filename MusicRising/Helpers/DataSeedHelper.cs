using Microsoft.AspNetCore.Identity;
using MusicRising.Data.Services;
using MusicRising.Models;

namespace MusicRising.Helpers;

public class DataSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IBandsService _bandsService;
    private readonly IVenuesService _venuesService;
    private readonly IShowsService _showsService;

    public DataSeeder(UserManager<IdentityUser> userManager, IBandsService bandsService, IVenuesService venuesService, IShowsService showsService)
    {
        _userManager = userManager;
        _bandsService = bandsService;
        _venuesService = venuesService;
        _showsService = showsService;
    }

    public async Task SeedData()
    {
        // Create users
        var user1 = new IdentityUser { UserName = "user1", Email = "user1@example.com" };
        var user2 = new IdentityUser { UserName = "user2", Email = "user2@example.com" };
        await _userManager.CreateAsync(user1, "Password123!");
        await _userManager.CreateAsync(user2, "Password123!");

        // Create bands
        var band1 = new Band
        {
            BandId = Guid.NewGuid().ToString(),
            IdentityUserId = user1.Id,
            BandName = "Band 1",
            Location = LocationEnum.NoordHolland,
            Genre = GenreEnum.Rock,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        var band2 = new Band
        {
            BandId = Guid.NewGuid().ToString(),
            IdentityUserId = user2.Id,
            BandName = "Band 2",
            Location = LocationEnum.NoordBrabant,
            Genre = GenreEnum.Pop,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        await _bandsService.Add(band1);
        await _bandsService.Add(band2);

        // Create venues
        var venue1 = new Venue
        {
            VenueId = Guid.NewGuid().ToString(),
            IdentityUserId = user1.Id,
            VenueName = "Venue 1",
            Location = LocationEnum.Utrecht,
            Genre = GenreEnum.Rock,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        var venue2 = new Venue
        {
            VenueId = Guid.NewGuid().ToString(),
            IdentityUserId = user2.Id,
            VenueName = "Venue 2",
            Location = LocationEnum.Limburg,
            Genre = GenreEnum.Pop,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        await _venuesService.Add(venue1);
        await _venuesService.Add(venue2);

        // Create shows
        var show1 = new Show
        {
            ShowId = Guid.NewGuid().ToString(),
            BandId = band1.BandId,
            VenueId = venue1.VenueId,
            Date = DateTime.Now.AddMonths(1),
            Genre = GenreEnum.Rock,
            ShowFee = 100.00,
            PromoLink = "http://example.com/show1"
        };

        var show2 = new Show
        {
            ShowId = Guid.NewGuid().ToString(),
            BandId = band2.BandId,
            VenueId = venue2.VenueId,
            Date = DateTime.Now.AddMonths(2),
            Genre = GenreEnum.Pop,
            ShowFee = 150.00,
            PromoLink = "http://example.com/show2"
        };

        await _showsService.Add(show1);
        await _showsService.Add(show2);
    }
}