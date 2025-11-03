using System.Net;

namespace Waracle.HotelBooking.Exceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(string? message) : base(message, HttpStatusCode.NotFound)
        {
        }
        public NotFoundException(string? message, Exception? innerException) : base(message, HttpStatusCode.NotFound, innerException)
        {
        }

    }
}
