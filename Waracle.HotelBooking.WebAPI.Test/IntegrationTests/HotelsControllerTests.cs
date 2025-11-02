using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Waracle.HotelBooking.Helpers;
using Waracle.HotelBooking.Infrastructure.DbContext;
using Waracle.HotelBooking.Services;



namespace Waracle.HotelBooking.WebAPI.Test.ControllerTests;

[TestClass]
public class HotelsControllerTests
{
    private static HttpClient _client;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

 


    [TestMethod]
    public async Task FindHotelByName_ShouldReturnHotel_WhenExists()
    {

        // Seed
        var seedResponse = await _client.PostAsync("/api/admin/seed", null);
        seedResponse.StatusCode.Should().Be(HttpStatusCode.OK);



        var response = await _client.GetAsync("/api/hotels/search?name=Alpha");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Alpha");


        // reset
        var resetResponse = await _client.PostAsync("/api/admin/reset", null);
        resetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task FindHotelByName_ShouldReturnNotFound_WhenMissing()
    {

        // Seed
        var seedResponse = await _client.PostAsync("/api/admin/seed", null);
        seedResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await _client.GetAsync("/api/hotels/search?name=UnknownHotel");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        // reset
        var resetResponse = await _client.PostAsync("/api/admin/reset", null);
        resetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
