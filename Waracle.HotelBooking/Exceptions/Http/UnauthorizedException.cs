using System.Net;

namespace Waracle.HotelBooking.Exceptions
{
    public class UnauthorizedException(string? message) : HttpException(message, HttpStatusCode.Unauthorized)
    {
    }
}
