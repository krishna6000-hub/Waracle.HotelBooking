using Waracle.HotelBooking.Domain.Definitions;

namespace Waracle.HotelBooking.Domain.Models;

public class Room
{
    public int Id { get; set; }
    public RoomType Type { get; set; }
    public int Capacity { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}
