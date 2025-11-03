using FluentAssertions;
using Waracle.HotelBooking.Domain.Models;

namespace Waracle.HotelBooking.WebAPI.Test.DomainTests;

[TestClass]
public class HoteUnderlTest
{
    [TestMethod]
    public void Hotel_ShouldInitializeWithEmptyRoomList()
    {
        var hotel = new Hotel { Name = "Test Hotel" };

        hotel.Rooms.Should().NotBeNull();
        hotel.Rooms.Should().BeEmpty();
    }

    [TestMethod]
    public void Hotel_ShouldStoreNameCorrectly()
    {
        var hotel = new Hotel { Name = "Ocean View" };

        hotel.Name.Should().Be("Ocean View");
    }
}

