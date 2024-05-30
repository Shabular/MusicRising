using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MusicRising.Helpers;
using MusicRising.Controllers;
using MusicRising.Data.Services;
using MusicRising.Models;
using Microsoft.AspNetCore.Hosting;
using MockQueryable.Moq;

namespace MusicRising.Tests.VenueTests
{
    public class VenuesControllerTests
    {
        private readonly Mock<IVenuesService> _mockVenuesService;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly VenuesController _controller;

        public VenuesControllerTests()
        {
            _mockVenuesService = new Mock<IVenuesService>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _controller = new VenuesController(_mockVenuesService.Object, _mockUserManager.Object, _mockWebHostEnvironment.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfVenues()
        {
            // Arrange
            var venues = new List<Venue>
            {
                new Venue { VenueId = "1", IdentityUserId = "1", VenueName = "Test Venue", Address = "Test Address" },
                new Venue { VenueId = "2", IdentityUserId = "2", VenueName = "Test Venue", Address = "Test Address" }
            }.AsQueryable().BuildMock().Object;

            _mockVenuesService.Setup(service => service.GetAll()).Returns(venues);

            // Act
            var result = await _controller.Index(null, null, false);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Venue>>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

       
    }
}
