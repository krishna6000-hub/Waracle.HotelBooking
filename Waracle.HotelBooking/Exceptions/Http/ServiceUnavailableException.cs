using System.Net;

namespace Waracle.HotelBooking.Exceptions
{
    public class ServiceUnavailableException(string? message) :
        HttpException(message, HttpStatusCode.ServiceUnavailable)
    {
    }
}
