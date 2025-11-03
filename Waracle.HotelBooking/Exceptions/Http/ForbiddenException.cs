using System.Net;

namespace Waracle.HotelBooking.Exceptions
{
    public class ForbiddenException(string? message) : HttpException(message, HttpStatusCode.Forbidden)
    {
    }
}
