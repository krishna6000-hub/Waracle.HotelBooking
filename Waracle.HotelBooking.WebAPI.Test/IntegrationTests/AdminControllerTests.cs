using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Waracle.HotelBooking.Helpers;
using Waracle.HotelBooking.Infrastructure.DbContext;

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

    [TestMethod]
    public async Task Reset_ShouldReturnOk()
    {
        var response = await _client.PostAsync("/api/admin/reset", null);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [TestMethod]
    public async Task Seed_ShouldReturnOk()
    {
        var response = await _client.PostAsync("/api/admin/seed", null);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


}

