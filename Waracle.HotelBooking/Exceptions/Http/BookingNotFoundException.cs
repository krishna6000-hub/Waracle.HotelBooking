namespace Waracle.HotelBooking.Exceptions.Http
{
    public class BookingNotFoundException(string? message) : NotFoundException(message)
    {
    }
}
