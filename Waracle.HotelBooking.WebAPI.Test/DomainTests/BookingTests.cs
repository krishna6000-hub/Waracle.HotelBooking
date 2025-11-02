using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Domain.Definitions;
using FluentAssertions;

namespace Waracle.HotelBooking.WebAPI.Test.DomainTests;

[TestClass]
public class BookingTests
{
    [TestMethod]
    public void Booking_ShouldGenerateReferenceAutomatically()
    {
        var booking = new Booking();

        booking.Reference.Should().NotBeNullOrEmpty();
    }

    [TestMethod]
    public void Booking_ShouldStoreDatesAndGuestCount()
    {
        var start = DateTime.Today;
        var end = start.AddDays(2);
        var booking = new Booking
        {
            StartDate = start,
            EndDate = end,
            GuestCount = 3
        };

        booking.StartDate.Should().Be(start);
        booking.EndDate.Should().Be(end);
        booking.GuestCount.Should().Be(3);
    }

    [TestMethod]
    public void Booking_ShouldLinkToRoom()
    {
        var room = new Room { Type = RoomType.Double, Capacity = 2 };
        var booking = new Booking { Room = room };

        booking.Room.Should().Be(room);
    }
}

