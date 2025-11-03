using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Domain.Definitions;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Infrastructure.DbContext;

[TestClass]
public class AppDbContextUnderTest
{
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _context.Database.EnsureCreated();
    }

    [TestMethod]
    public async Task CanInsertHotelWithRooms()
    {
        var hotel = new Hotel
        {
            Name = "Test Hotel",
            Rooms = new List<Room>
            {
                new Room { Type = RoomType.Single, Capacity = 1 },
                new Room { Type = RoomType.Double, Capacity = 2 }
            }
        };

        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        var savedHotel = await _context.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync();
        savedHotel.Should().NotBeNull();
        savedHotel!.Rooms.Count.Should().Be(2);
    }

    [TestMethod]
    public async Task CanInsertBookingLinkedToRoom()
    {
        var hotel = new Hotel { Name = "Booking Hotel" };
        var room = new Room { Type = RoomType.Deluxe, Capacity = 4, Hotel = hotel };
        var booking = new Booking
        {
            Room = room,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(2),
            GuestCount = 3
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var savedBooking = await _context.Bookings.Include(b => b.Room).ThenInclude(r => r.Hotel).FirstOrDefaultAsync();
        savedBooking.Should().NotBeNull();
        savedBooking!.Room.Type.Should().Be(RoomType.Deluxe);
        savedBooking.Room.Hotel.Name.Should().Be("Booking Hotel");
    }

    [TestMethod]
    public void DbContext_ShouldHaveDbSets()
    {
        _context.Hotels.Should().NotBeNull();
        _context.Rooms.Should().NotBeNull();
        _context.Bookings.Should().NotBeNull();
    }
}
