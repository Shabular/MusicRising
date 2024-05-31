using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

using MusicRising.Data;
using MusicRising.Helpers;
using MusicRising.Data.Services;
using MusicRising.Models;

namespace MusicRising.Tests.VenueTests
{
    public class VenuesServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly VenuesService _service;

        public VenuesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{System.Guid.NewGuid()}")
                .Options;

            _context = new ApplicationDbContext(options);
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _service = new VenuesService(_context, _mockUserManager.Object);
        }

        [Fact]
        public async Task Add_AddsVenueToDatabase()
        {
            // Arrange
            var venue = new Venue { VenueId = "1", IdentityUserId = "1", VenueName = "Test Venue", Address = "Test Address" };

            // Act
            await _service.Add(venue);
            var addedVenue = await _context.Venues.FindAsync("1");

            // Assert
            Assert.NotNull(addedVenue);
            Assert.Equal(venue.VenueId, addedVenue.VenueId);
        }

        [Fact]
        public async Task Delete_RemovesVenueFromDatabase()
        {
            // Arrange
            var venue = new Venue { VenueId = "1", IdentityUserId = "1", VenueName = "Test Venue", Address = "Test Address" };
            await _service.Add(venue);

            // Act
            await _service.Delete(venue);
            var deletedVenue = await _context.Venues.FindAsync("1");

            // Assert
            Assert.Null(deletedVenue);
        }

        [Fact]
        public async Task Update_UpdatesVenueInDatabase()
        {
            // Arrange
            var venue = new Venue { VenueId = "1", IdentityUserId = "1", VenueName = "Test Venue", Address = "Test Address" };
            await _service.Add(venue);
            venue.VenueName = "Updated Venue";

            // Act
            await _service.Update(venue);
            var updatedVenue = await _context.Venues.FindAsync("1");

            // Assert
            Assert.NotNull(updatedVenue);
            Assert.Equal("Updated Venue", updatedVenue.VenueName);
        }

      
    }
}
