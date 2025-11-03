using System.Net;

namespace Waracle.HotelBooking.Exceptions
{
    /// <summary>
    /// Http Exception containing http status code and message
    /// These need to be caught in the controllers and converted to appropriate http response
    /// </summary>
    public class HttpException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public HttpException(string? message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpException(string? message, HttpStatusCode httpStatusCode, Exception? innerException) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }

    }
}
