using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Waracle.HotelBooking.Domain.Models;


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

    [TestInitialize]
    public async Task ReseedDB()
    {
        // reset
        var resetResponse = await _client.PostAsync("/api/admin/reset", null);

        Task.Delay(2000).Wait();

        // Seed
        var seedResponse = await _client.PostAsync("/api/admin/seed", null);

    }




    [TestMethod]
    public async Task GetAvailableRooms_ShouldReturnRooms_WhenAvailable()
    {

        var resp = await _client.GetAsync("api/hotels/search?name=Alpha");
        resp.StatusCode.Should().Be(HttpStatusCode.OK);
        var hotel = await resp.Content.ReadFromJsonAsync<Hotel>();
        hotel.Should().NotBeNull();

        var response = await _client.GetAsync($"/api/bookings/{hotel.Id}/available-rooms/2025-11-10/2025-11-12/3");
        response.StatusCode.Should().Be(HttpStatusCode.OK);


        var availableRooms = await response.Content.ReadFromJsonAsync<List<Room>>();
        availableRooms.Should().NotBeNull();
        availableRooms.Count.Should().Be(2);


    }

    [TestMethod]
    public async Task GetBookingByReference_ShouldReturnBooking_WhenAvailable()
    {

        var resp = await _client.GetAsync("api/hotels/search?name=Alpha");
        resp.StatusCode.Should().Be(HttpStatusCode.OK);
        var hotel = await resp.Content.ReadFromJsonAsync<Hotel>();
        hotel.Should().NotBeNull();

        var bookingRequest = new
        {
            hotelId = hotel.Id,
            startDate = "2025-11-10",
            endDate = "2025-11-12",
            guestCount = 2
        };

        var response = await _client.PostAsJsonAsync("api/bookings/book", bookingRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var booking = await response.Content.ReadFromJsonAsync<Booking>();
        booking.Should().NotBeNull();

        var bookingRefResponse = await _client.GetAsync($"api/bookings/{booking.Reference}");
        bookingRefResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var bookingFetch = await bookingRefResponse.Content.ReadFromJsonAsync<Booking>();
        bookingFetch.Should().NotBeNull();
        bookingFetch.Should().BeEquivalentTo(booking);

    }

    [TestMethod]
    public async Task BookRoom_ShouldReturnBooking_WhenSuccessful()
    {


        var resp = await _client.GetAsync("api/hotels/search?name=Alpha");

        resp.StatusCode.Should().Be(HttpStatusCode.OK);
        var hotel = await resp.Content.ReadFromJsonAsync<Hotel>();


        var bookingRequest = new
        {
            hotelId = hotel.Id,
            startDate = "2025-11-10",
            endDate = "2025-11-12",
            guestCount = 2
        };



        var response = await _client.PostAsJsonAsync("api/bookings/book", bookingRequest);



        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("reference");
    }
}
