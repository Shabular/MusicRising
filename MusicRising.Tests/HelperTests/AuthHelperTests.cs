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

public class AuthHelperTests
{
    [Fact]
    public void MissMAtchIDsShouldBeFalse()
    {
        Assert.False(AuthHelper.Authorize("The string", "A different string"));
    }
    
    [Fact]
    public void MatchedIDsShouldBeTrue()
    {
        Assert.True(AuthHelper.Authorize("The same string", "The same string"));
    }
}