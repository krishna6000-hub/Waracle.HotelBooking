
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Helpers;
using Waracle.HotelBooking.Infrastructure.DbContext;
using Waracle.HotelBooking.Services;

namespace Waracle.HotelBooking.WebAPI.Test.UnitTests
{


    [TestClass]
    public class BookingServiceTests
    {
        private AppDbContext _context = null!;
        private BookingRepository _repository = null!;
        private BookingService _service = null!;

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
            _service = new BookingService(_repository);
        }

        [TestMethod]
        public async Task GetAvailableRooms_ShouldReturnRooms_WhenAvailable()
        {
            var hotel = await _repository.FindHotelByName("Alpha");
            hotel.Should().NotBeNull();

            var start = DateTime.Today.AddDays(1);
            var end = start.AddDays(2);
            var rooms = await _service.GetAvailableRooms(hotel.Id, start, end, guests: 2);

            rooms.Should().NotBeEmpty();
            rooms.All(r => r.Capacity >= 2).Should().BeTrue();
        }

        [TestMethod]
        public async Task BookRoom_ShouldCreateBooking_WhenRoomAvailable()
        {
            var start = DateTime.Today.AddDays(3);
            var end = start.AddDays(2);
            var booking = await _service.BookRoom(hotelId: 1, start, end, guests: 2);

            booking.Should().NotBeNull();
            booking!.GuestCount.Should().Be(2);
            booking.Room.Should().NotBeNull();
        }

        [TestMethod]
        public async Task BookRoom_ShouldReturnNull_WhenNoRoomAvailable()
        {

            var hotel = await _repository.FindHotelByName("Alpha");
            hotel.Should().NotBeNull();

            var start = DateTime.Today.AddDays(5);
            var end = start.AddDays(2);

            // Fill all rooms with overlapping bookings
            var rooms = await _service.GetAvailableRooms(hotel.Id, start, end, guests: 2);
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

            var booking = await _service.BookRoom(hotel.Id, start, end, guests: 2);
            booking.Should().BeNull();
        }

        [TestMethod]
        public async Task GetBookingByReference_ShouldReturnBooking_WhenExists()
        {
            var start = DateTime.Today.AddDays(7);
            var end = start.AddDays(2);
            var booking = await _service.BookRoom(hotelId: 1, start, end, guests: 2);

            var result = await _service.GetBookingByReference(booking!.Reference);
            result.Should().NotBeNull();
            result!.Reference.Should().Be(booking.Reference);
        }

        [TestMethod]
        public async Task GetBookingByReference_ShouldReturnNull_WhenNotFound()
        {
            var result = await _service.GetBookingByReference("non-existent-ref");
            result.Should().BeNull();
        }
    }

}
