namespace Waracle.HotelBooking.Exceptions
{
    public class HotelNotFoundException(string? message) : NotFoundException(message)
    {
    }
}
