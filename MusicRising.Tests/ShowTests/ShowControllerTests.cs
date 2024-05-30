using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MusicRising.Controllers;
using MusicRising.Data.Services;
using MusicRising.Models;
using MusicRising.Helpers;
using Microsoft.AspNetCore.Hosting;
using MockQueryable.Moq;

namespace MusicRising.Tests.ShowTests
{
    public class ShowsControllerTests
    {
        private readonly Mock<IShowsService> _mockShowsService;
        private readonly Mock<IBandsService> _mockBandsService;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly Mock<IVenuesService> _mockVenuesService;
        private readonly ShowsController _controller;
        public ShowsControllerTests()
        {
            _mockShowsService = new Mock<IShowsService>();
            _mockBandsService = new Mock<IBandsService>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockVenuesService = new Mock<IVenuesService>();
            _controller = new ShowsController(_mockShowsService.Object, _mockBandsService.Object, _mockUserManager.Object, _mockWebHostEnvironment.Object, _mockVenuesService.Object);
        }


        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfShows()
        {
            // Arrange
            var shows = new List<Show>
            {
                new Show { ShowId = "1", VenueId = "1", Genre = GenreEnum.Rock, Date = DateTime.Now },
                new Show { ShowId = "2", VenueId = "3", Genre = GenreEnum.Rock, Date = DateTime.Now }
            }.AsQueryable().BuildMock().Object;

            _mockShowsService.Setup(service => service.GetAll()).Returns(shows);

            // Act
            var result = await _controller.Index(null, null, true);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Show>>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

       
    }
}
