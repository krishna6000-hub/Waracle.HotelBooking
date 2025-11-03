namespace Waracle.HotelBooking.Exceptions
{
    public class RoomNotFoundException(string? message) : NotFoundException(message)
    {
    }
}
