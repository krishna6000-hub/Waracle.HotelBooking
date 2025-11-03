using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;



namespace Waracle.HotelBooking.WebAPI.Test.ControllerTests;

[TestClass]
public class HotelsControllerUnderTest
{
    private static HttpClient _client;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }


    [TestInitialize]
    public async Task ReseedDB()
    {

        // Seed
        var seedResponse = await _client.PostAsync("/api/admin/seed", null);

    }

    [TestCleanup]
    public async Task ResetDB()
    {
        // reset
        var resetResponse = await _client.PostAsync("/api/admin/reset", null);

    }



    [TestMethod]
    public async Task FindHotelByName_ShouldReturnHotel_WhenExists()
    {



        var response = await _client.GetAsync("/api/hotels/search?name=Alpha");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Alpha");


    }

    [TestMethod]
    public async Task FindHotelByName_ShouldReturnNotFound_WhenMissing()
    {

        var response = await _client.GetAsync("/api/hotels/search?name=UnknownHotel");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);



    }
}
