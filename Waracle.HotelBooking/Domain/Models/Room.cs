using System.Text.Json.Serialization;
using Waracle.HotelBooking.Domain.Definitions;

namespace Waracle.HotelBooking.Domain.Models;

public class Room
{
    public int Id { get; set; }
    public RoomType Type { get; set; }
    public int Capacity { get; set; }
    public int HotelId { get; set; }
    [JsonIgnore]
    public Hotel? Hotel { get; set; }
    [JsonIgnore]
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}
