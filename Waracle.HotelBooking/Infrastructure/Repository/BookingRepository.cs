using Waracle.HotelBooking.Domain.Interfaces;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Helpers;
public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
       // SeedData.Reset(_context);
        //SeedData.Seed(_context);
    }

    public async Task<List<Room>> GetAvailableRooms(DateTime start, DateTime end, int guests)
    {
        return await _context.Rooms
            .Include(r => r.Bookings)
            .Where(r => r.Capacity >= guests &&
                        !r.Bookings.Any(b =>
                            (start < b.EndDate && end > b.StartDate)))
            .ToListAsync();
    }

    public async Task<Booking?> CreateBooking(int hotelId, DateTime start, DateTime end, int guests)
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
        return booking;
    }

    public async Task<Booking?> GetBookingByReference(string reference)
    {
        return await _context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .FirstOrDefaultAsync(b => b.Reference == reference);
    }

    public async Task<Hotel?> FindHotelByName(string name)
    {
        var hotel = await _context.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Name.ToLower() == name.ToLower());

        if (hotel != null)
        {
            // we need to do this to remove cyclic reference serialzation errors
            foreach (Room rm in hotel.Rooms)
            {
                rm.Hotel = null;
                rm.Bookings = null;

            }
        }

        return hotel;
    }
}

