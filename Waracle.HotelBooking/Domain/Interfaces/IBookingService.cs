using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Infrastructure.DbContext;

namespace Waracle.HotelBooking.Domain.Interfaces;

    public interface IBookingService
    {
        public  Task<List<Room>> GetAvailableRooms(DateTime start, DateTime end, int guests);

        public  Task<Booking?> BookRoom(int hotelId, DateTime start, DateTime end, int guests);

        public  Task<Booking?> GetBookingByReference(string reference);

        public  Task<Hotel> FindHotelByName(string name);
           
     }

