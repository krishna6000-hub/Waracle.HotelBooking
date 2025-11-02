using Waracle.HotelBooking.Domain.Models;

namespace Waracle.HotelBooking.Domain.Interfaces;

public interface IBookingService
{
    public Task<List<Room>> GetAvailableRooms(int hotelId, DateTime start, DateTime end, int guests);

    public Task<Booking?> BookRoom(int hotelId, DateTime start, DateTime end, int guests);

    public Task<Booking?> GetBookingByReference(string reference);

    public Task<Hotel> FindHotelByName(string name);

}

