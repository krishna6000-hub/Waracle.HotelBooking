using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;


namespace Waracle.HotelBooking.WebAPI.Test.ControllerTests;


[TestClass]
public class BookingsControllerTests
{
    private static HttpClient _client;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [TestMethod]
    public async Task GetAvailableRooms_ShouldReturnRooms_WhenAvailable()
    {
        var response = await _client.GetAsync("/api/bookings/available?start=2025-11-10&end=2025-11-12&guests=2");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        //content.Should().Contain("Double").Or.Contain("Deluxe");
    }

    [TestMethod]
    public async Task GetBookingByReference_ShouldReturnBooking_WhenAvailable()
    {
        var response = await _client.GetAsync("/api/bookings/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        //content.Should().Contain("Double").Or.Contain("Deluxe");
    }

    [TestMethod]
    public async Task BookRoom_ShouldReturnBooking_WhenSuccessful()
    {
        var bookingRequest = new
        {
            hotelId = 1,
            startDate = "2025-11-10",
            endDate = "2025-11-12",
            guestCount = 2
        };

        var response = await _client.PostAsJsonAsync("/api/bookings/book", bookingRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Reference");
    }
}
