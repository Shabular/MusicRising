using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MusicRising.Controllers;
using MusicRising.Data.Services;
using MusicRising.Models;
using Microsoft.AspNetCore.Hosting;
using MockQueryable.Moq;

namespace MusicRising.Tests.BandTests
{
    public class BandControllerTests
    {
        private readonly Mock<IBandsService> _mockBandsService;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly Mock<IVenuesService> _mockVenuesService;
        private readonly BandsController _controller;

        public BandControllerTests()
        {
            _mockBandsService = new Mock<IBandsService>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockVenuesService = new Mock<IVenuesService>();
            _controller = new BandsController(_mockBandsService.Object, _mockUserManager.Object, _mockWebHostEnvironment.Object, _mockVenuesService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfBands()
        {
            // Arrange
            var bands = new List<Band>
            {
                new Band { BandId = "1", IdentityUserId = "1", Address = "Test Address" },
                new Band { BandId = "2", IdentityUserId = "2", Address = "Another Address" }
            }.AsQueryable().BuildMock().Object;

            _mockBandsService.Setup(service => service.GetAll()).Returns(bands);

            // Act
            var result = await _controller.Index(null, null, false);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Band>>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

     
    }
}
