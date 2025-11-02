namespace Waracle.HotelBooking.Domain.Models;
public class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Room> Rooms { get; set; } = new List<Room>();
}

