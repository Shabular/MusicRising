using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using MusicRising.Data;
using MusicRising.Helpers;
using MusicRising.Data.Services;
using MusicRising.Models;

namespace MusicRising.Tests.ShowTests
{
    public class ShowsServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly ShowsService _service;

        public ShowsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{System.Guid.NewGuid()}")
                .Options;

            _context = new ApplicationDbContext(options);
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _service = new ShowsService(_context);
        }

        [Fact]
        public async Task Add_AddsShowToDatabase()
        {
            // Arrange
            var show = new Show { ShowId = "1", VenueId = "1", Genre = GenreEnum.Rock, Date = DateTime.Now };

            // Act
            await _service.Add(show);
            var addedShow = await _context.Shows.FindAsync("1");

            // Assert
            Assert.NotNull(addedShow);
            Assert.Equal(show.ShowId, addedShow.ShowId);
        }

        [Fact]
        public async Task Delete_RemovesShowFromDatabase()
        {
            // Arrange
            var show = new Show { ShowId = "1", VenueId = "3", Genre = GenreEnum.Rock, Date = DateTime.Now };
            await _service.Add(show);

            // Act
            await _service.Delete(show);
            var deletedShow = await _context.Shows.FindAsync("1");

            // Assert
            Assert.Null(deletedShow);
        }

        [Fact]
        public async Task Update_UpdatesShowInDatabase()
        {
            // Arrange
            var show = new Show {ShowId = "1", VenueId = "1", Genre = GenreEnum.Rock, Date = DateTime.Now };
            await _service.Add(show);
            show.Details = "Updated Show";

            // Act
            await _service.Update(show);
            var updatedShow = await _context.Shows.FindAsync("1");

            // Assert
            Assert.NotNull(updatedShow);
            Assert.Equal("Updated Show", updatedShow.Details);
        }

        
    }
}
