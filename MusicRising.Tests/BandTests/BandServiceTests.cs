using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using MusicRising.Data;
using MusicRising.Data.Services;
using MusicRising.Models;

namespace MusicRising.Tests.BandTests
{
    public class BandServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly BandsService _service;

        public BandServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{System.Guid.NewGuid()}")
                .Options;

            _context = new ApplicationDbContext(options);
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _service = new BandsService(_context, _mockUserManager.Object);
        }

        [Fact]
        public async Task Add_AddsBandToDatabase()
        {
            // Arrange
            var band = new Band { BandId = "1", IdentityUserId = "1", Address = "Test Address" };

            // Act
            await _service.Add(band);
            var addedBand = await _context.Bands.FindAsync("1");

            // Assert
            Assert.NotNull(addedBand);
            Assert.Equal(band.BandId, addedBand.BandId);
        }

        [Fact]
        public async Task Delete_RemovesBandFromDatabase()
        {
            // Arrange
            var band = new Band { BandId = "1", IdentityUserId = "1", Address = "Test Address" };
            await _service.Add(band);

            // Act
            await _service.Delete(band);
            var deletedBand = await _context.Bands.FindAsync("1");

            // Assert
            Assert.Null(deletedBand);
        }

        [Fact]
        public async Task Update_UpdatesBandInDatabase()
        {
            // Arrange
            var band = new Band { BandId = "1", IdentityUserId = "1", Address = "Test Address" };
            await _service.Add(band);
            band.Address = "Updated Address";

            // Act
            await _service.Update(band);
            var updatedBand = await _context.Bands.FindAsync("1");

            // Assert
            Assert.NotNull(updatedBand);
            Assert.Equal("Updated Address", updatedBand.Address);
        }

       
    }
}
