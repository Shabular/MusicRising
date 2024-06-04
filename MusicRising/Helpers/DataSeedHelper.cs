using System.Diagnostics;
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

    public static void RunEfDatabaseUpdate()
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "ef database update",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = new Process { StartInfo = processStartInfo })
        {
            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            process.ErrorDataReceived += (sender, args) => Console.WriteLine("ERROR: " + args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
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
            Details = "The side chicks is a punkrock cover band with a bit of own material",
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
            Details = "One of the most amazing punk rock bands, songs like punk rock song and ephipany realy do the charm",
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
            Details = "Do i realy need to tell who we are?",
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
            Details = "Small venue in Etten-Leur known for the wooden shoes on the ceiling",
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
            Details = "A small bar with loads of fun",
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
            Details = "We still have a party sometimes......",
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
            Details = "Amazing fun to watch cover band and well we ll be playing punk rock",
            ShowFee = 100.00,
            PromoLink = "rockfl2.jpg",
            Booked = true
        };

        var show2 = new Show
        {
            ShowId = Guid.NewGuid().ToString(),
            BandId = band2.BandId,
            VenueId = venue2.VenueId,
            HeadLiner = band2,
            Date = DateTime.Now.AddMonths(2),
            Details = "Lets break stuff together",
            Genre = GenreEnum.Pop,
            ShowFee = 150.00,
            PromoLink = "rockfl1.jpg",
            Booked = true
        };

        await _showsService.Add(show1);
        await _showsService.Add(show2);
    }
}