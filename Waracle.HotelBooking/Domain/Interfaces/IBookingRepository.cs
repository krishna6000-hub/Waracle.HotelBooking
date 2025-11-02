using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.HotelBooking.Domain.Models;

namespace Waracle.HotelBooking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Room>> GetAvailableRooms(DateTime start, DateTime end, int guests);
        Task<Booking?> CreateBooking(int hotelId, DateTime start, DateTime end, int guests);
        Task<Booking?> GetBookingByReference(string reference);
        Task<Hotel> FindHotelByName(string name);
    }

}
