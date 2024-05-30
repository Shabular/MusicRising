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
        var user1 = new IdentityUser { UserName = "admin", Email = "admin@mr.com" };
        var user2 = new IdentityUser { UserName = "user2", Email = "user2@example.com" };
        await _userManager.CreateAsync(user1, "Password123!");
        await _userManager.CreateAsync(user2, "Password123!");

        // Create bands
        var band1 = new Band
        {
            BandId = Guid.NewGuid().ToString(),
            IdentityUserId = user1.Id,
            BandName = "The Side Chicks",
            Location = LocationEnum.NoordBrabant,
            BandPicture = "sidechicks.jpg",
            Genre = GenreEnum.Rock,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        var band2 = new Band
        {
            BandId = Guid.NewGuid().ToString(),
            IdentityUserId = user2.Id,
            BandName = "Bad Religion",
            Location = LocationEnum.NoordBrabant,
            BandPicture = "badreligion.jpg",
            Genre = GenreEnum.Pop,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };
        
        var band3 = new Band
        {
            BandId = Guid.NewGuid().ToString(),
            IdentityUserId = user2.Id,
            BandName = "Imagine Dragons",
            BandPicture = "id.jpg",
            Location = LocationEnum.Friesland,
            Genre = GenreEnum.Classical,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        await _bandsService.Add(band1);
        await _bandsService.Add(band2);
        await _bandsService.Add(band3);

        // Create venues
        var venue1 = new Venue
        {
            VenueId = Guid.NewGuid().ToString(),
            IdentityUserId = user1.Id,
            VenueName = "De klomp",
            Location = LocationEnum.Utrecht,
            VenuePicture = "klomp.jpg",
            Genre = GenreEnum.Rock,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        var venue2 = new Venue
        {
            VenueId = Guid.NewGuid().ToString(),
            IdentityUserId = user2.Id,
            VenueName = "Krisjes",
            Location = LocationEnum.NoordBrabant,
            VenuePicture = "krisjes.jpg",
            Genre = GenreEnum.Pop,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };
        
        var venue3 = new Venue
        {
            VenueId = Guid.NewGuid().ToString(),
            IdentityUserId = user2.Id,
            VenueName = "Zalinaz",
            Location = LocationEnum.Zeeland,
            VenuePicture = "zalinaz.jpg",
            Genre = GenreEnum.Pop,
            Address = "1600 Amphitheatre Parkway, Mountain View, CA 94043, USA",
            Latitude = 37.4224764,
            Longitude = -122.0842499
        };

        await _venuesService.Add(venue1);
        await _venuesService.Add(venue2);
        await _venuesService.Add(venue3);

        // Create shows
        var show1 = new Show
        {
            ShowId = Guid.NewGuid().ToString(),
            BandId = band1.BandId,
            HeadLiner = band1,
            VenueId = venue1.VenueId,
            Date = DateTime.Now.AddMonths(1),
            Genre = GenreEnum.Rock,
            ShowFee = 100.00,
            PromoLink = "rockfl2.jpg"
        };

        var show2 = new Show
        {
            ShowId = Guid.NewGuid().ToString(),
            BandId = band2.BandId,
            VenueId = venue2.VenueId,
            HeadLiner = band2,
            Date = DateTime.Now.AddMonths(2),
            Genre = GenreEnum.Pop,
            ShowFee = 150.00,
            PromoLink = "rockfl1.jpg"
        };

        await _showsService.Add(show1);
        await _showsService.Add(show2);
    }
}