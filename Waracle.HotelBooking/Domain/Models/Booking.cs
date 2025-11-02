using System.Text.Json.Serialization;

namespace Waracle.HotelBooking.Domain.Models;

public class Booking
{
    public int Id { get; set; }
    public string Reference { get; set; } = Guid.NewGuid().ToString();  
    public int RoomId { get; set; }
    [JsonIgnore]
    public  Room Room { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int GuestCount { get; set; }

}
