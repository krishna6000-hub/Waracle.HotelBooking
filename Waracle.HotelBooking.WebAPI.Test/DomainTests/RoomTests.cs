
using Waracle.HotelBooking.Domain.Models;
using FluentAssertions;
using Waracle.HotelBooking.Domain.Definitions;

namespace Waracle.HotelBooking.WebAPI.Test.DomainTests;

[TestClass]
public class RoomTests
{
    [TestMethod]
    public void Room_ShouldStoreTypeAndCapacity()
    {
        var room = new Room { Type = RoomType.Deluxe, Capacity = 4 };

        room.Type.Should().Be(RoomType.Deluxe);
        room.Capacity.Should().Be(4);
    }

    [TestMethod]
    public void Room_ShouldInitializeWithEmptyBookingList()
    {
        var room = new Room();

        room.Bookings.Should().NotBeNull();
        room.Bookings.Should().BeEmpty();
    }

    [TestMethod]
    public void Room_ShouldLinkToHotel()
    {
        var hotel = new Hotel { Name = "Test Hotel" };
        var room = new Room { Hotel = hotel };

        room.Hotel.Should().Be(hotel);
    }
}
