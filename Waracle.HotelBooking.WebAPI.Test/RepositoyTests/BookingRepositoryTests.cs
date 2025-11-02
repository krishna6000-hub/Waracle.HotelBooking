using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Helpers;
using Waracle.HotelBooking.Infrastructure.DbContext;

[TestClass]
public class BookingRepositoryTests
{
    private AppDbContext _context = null!;
    private BookingRepository _repository = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


        _context = new AppDbContext(options);
        SeedData.Reset(_context);
        SeedData.Seed(_context);
        _repository = new BookingRepository(_context);
    }

    [TestMethod]
    public async Task GetAvailableRooms_ShouldReturnRooms_WhenAvailable()
    {

        var hotel = await _repository.FindHotelByName("Alpha");
        hotel.Should().NotBeNull();


        var start = DateTime.Today.AddDays(1);
        var end = start.AddDays(2);
        var rooms = await _repository.GetAvailableRooms(hotel.Id, start, end, guests: 2);

        rooms.Should().NotBeEmpty();
        rooms.All(r => r.Capacity >= 2).Should().BeTrue();
    }

    [TestMethod]
    public async Task CreateBooking_ShouldReturnBooking_WhenRoomAvailable()
    {
        var start = DateTime.Today.AddDays(3);
        var end = start.AddDays(2);
        var booking = await _repository.CreateBooking(hotelId: 1, start, end, guests: 2);

        booking.Should().NotBeNull();
        booking!.GuestCount.Should().Be(2);
        booking.Room.Should().NotBeNull();
    }

    [TestMethod]
    public async Task CreateBooking_ShouldReturnNull_WhenNoRoomAvailable()
    {

        var hotel = await _repository.FindHotelByName("Alpha");
        hotel.Should().NotBeNull();

        var start = DateTime.Today.AddDays(5);
        var end = start.AddDays(2);

        // Fill all rooms with overlapping bookings
        var rooms = await _repository.GetAvailableRooms(hotel.Id, start, end, guests: 2);
        foreach (var room in rooms)
        {
            _context.Bookings.Add(new Booking
            {
                RoomId = room.Id,
                StartDate = start,
                EndDate = end,
                GuestCount = room.Capacity
            });
        }
        await _context.SaveChangesAsync();

        var booking = await _repository.CreateBooking(hotelId: 1, start, end, guests: 2);
        booking.Should().BeNull();
    }

    [TestMethod]
    public async Task GetBookingByReference_ShouldReturnBooking_WhenExists()
    {
        var start = DateTime.Today.AddDays(7);
        var end = start.AddDays(2);
        var booking = await _repository.CreateBooking(hotelId: 1, start, end, guests: 2);

        var result = await _repository.GetBookingByReference(booking!.Reference);
        result.Should().NotBeNull();
        result!.Reference.Should().Be(booking.Reference);
    }

    [TestMethod]
    public async Task GetBookingByReference_ShouldReturnNull_WhenNotFound()
    {
        var result = await _repository.GetBookingByReference("non-existent-ref");
        result.Should().BeNull();
    }
}

