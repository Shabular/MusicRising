using Moq;
using Xunit;
using MusicRising.Controllers;
using MusicRising.Data.Services;
using MusicRising.Models;
using MusicRising.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public class ImageHelperTests
{
    [Fact]
    public void SaveEmptyImageToServerReturnsDefaultImageName()
    {
        // Arrange
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("wwwroot");

        // Act
        var result = ImageHelper.SaveImageToServer(mockWebHostEnvironment.Object, null);

        // Assert
        Assert.Equal("Default.png", result);
    }

    [Fact]
    public void SaveImageToServerReturnsFileName()
    {
        // Arrange
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("wwwroot");

        var mockImage = new Mock<IFormFile>();
        mockImage.Setup(m => m.FileName).Returns("test.png");
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write("Dummy file content");
        writer.Flush();
        ms.Position = 0;
        mockImage.Setup(m => m.OpenReadStream()).Returns(ms);

        // Act
        var result = ImageHelper.SaveImageToServer(mockWebHostEnvironment.Object, mockImage.Object);

        // Assert
        Assert.EndsWith("_test.png", result);
        string filePath = Path.Combine("wwwroot", "Images", result);
        Assert.True(File.Exists(filePath));

        // Clean up
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    [Fact]
    public void RemoveImageFromServerReturnsTrue()
    {
        // Arrange
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("wwwroot");

        string testFileName = "test_file_to_delete.png";
        string filePath = Path.Combine("wwwroot", "Images", testFileName);

        Directory.CreateDirectory(Path.Combine("wwwroot", "Images"));
        File.WriteAllText(filePath, "Test content");

        // Act
        var result = ImageHelper.RemoveImageFromServer(mockWebHostEnvironment.Object, testFileName);

        // Assert
        Assert.True(result);
        Assert.False(File.Exists(filePath));
    }
    

    [Fact]
    public void RemoveNullImageFromServerReturnsFalse()
    {
        // Arrange
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("wwwroot");

        // Act
        var result = ImageHelper.RemoveImageFromServer(mockWebHostEnvironment.Object, "non_existing_file.png");

        // Assert
        Assert.False(result);
    }
}