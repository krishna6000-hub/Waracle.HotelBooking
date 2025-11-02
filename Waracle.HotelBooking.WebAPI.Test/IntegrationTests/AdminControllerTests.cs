using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Waracle.HotelBooking.WebAPI.Test.ControllerTests;

[TestClass]
public class AdminControllerTests
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

        var response = await _client.PostAsync("/api/admin/seed", null);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        Task.Delay(2000).Wait();

        var response2 = await _client.PostAsync("/api/admin/reset", null);
        response2.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [TestMethod]
    public async Task Reset_ShouldReturnOk()
    {



        var response2 = await _client.PostAsync("/api/admin/reset", null);
        response2.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [TestMethod]
    public async Task Seed_ShouldReturnOk()
    {
        var response = await _client.PostAsync("/api/admin/seed", null);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


}

