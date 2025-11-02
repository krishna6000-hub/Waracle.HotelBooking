using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Domain.Interfaces;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Infrastructure.DbContext;

namespace Waracle.HotelBooking.Services
{

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Room>> GetAvailableRooms(int hotelId, DateTime start, DateTime end, int guests)
            => _repository.GetAvailableRooms(hotelId, start, end, guests);

        public Task<Booking?> BookRoom(int hotelId, DateTime start, DateTime end, int guests)
            => _repository.CreateBooking(hotelId, start, end, guests);

        public Task<Booking?> GetBookingByReference(string reference)
            => _repository.GetBookingByReference(reference);

        public Task<Hotel> FindHotelByName(string name)
           => _repository.FindHotelByName(name);
    }








    /*


    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
     
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime start, DateTime end, int guests)
        {
            var rooms =  await _context.Rooms
                .Include(r => r.Bookings)
                .Where(r => r.Capacity >= guests &&
                            !r.Bookings.Any(b =>
                                (start < b.EndDate && end > b.StartDate)))
                .ToListAsync();

            
            
            var lstRooms = rooms.ToList();
            
            // we need to do this to remove cyclic reference serialzation errors
            foreach (Room rm in lstRooms)
            {
                rm.Hotel = null;

            }

            return lstRooms;

        }

        public async Task<Booking?> BookRoom(int hotelId, DateTime start, DateTime end, int guests)
        {
            var availableRooms = await GetAvailableRooms(start, end, guests);
            var room = availableRooms.FirstOrDefault(r => r.HotelId == hotelId);

            if (room == null) return null;

            var booking = new Booking
            {
                RoomId = room.Id,
                StartDate = start,
                EndDate = end,
                GuestCount = guests,
                Reference = Guid.NewGuid().ToString()
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            booking.Room.Hotel = null;
            booking.Room.Bookings = null;

            return booking;
        }

        public async Task<Booking?> GetBookingByReference(string reference)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Hotel)
                .FirstOrDefaultAsync(b => b.Reference == reference);

            // we need to do this to remove cyclic reference serialzation errors
            if (bookings == null) throw new Exception("No Data found");

            

            // we need to do this to remove cyclic reference serialzation errors
            bookings.Room.Hotel = null;
            bookings.Room.Bookings = null;

            return bookings;
            
        }

        public async Task<Hotel> FindHotelByName(string name)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Name.ToLower() == name.ToLower());

            if (hotel == null) throw new Exception("No Data found");


           
            
            // we need to do this to remove cyclic reference serialzation errors
            foreach (Room rm in hotel.Rooms)
            {
                rm.Hotel = null;

            }

            return hotel;
        }
    }

    */
}
