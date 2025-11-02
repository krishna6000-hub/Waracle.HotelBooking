using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Domain.Interfaces;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Infrastructure.DbContext;
public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
        // SeedData.Reset(_context);
        //SeedData.Seed(_context);
    }

    public async Task<List<Room>> GetAvailableRooms(int hotelId, DateTime start, DateTime end, int guests)
    {
        return await _context.Rooms
            .Include(r => r.Bookings)
            .Where(r => r.Capacity >= guests && r.HotelId == hotelId &&
                        !r.Bookings.Any(b =>
                            (start < b.EndDate && end > b.StartDate)))
            .ToListAsync();

    }

    public async Task<Booking?> CreateBooking(int hotelId, DateTime start, DateTime end, int guests)
    {
        var availableRooms = await GetAvailableRooms(hotelId, start, end, guests);
        var room = availableRooms.FirstOrDefault(r => r.HotelId == hotelId);

        if (room == null) return null;

        var booking = new Booking
        {
            RoomId = room.Id,
            Room = room,
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
        return await _context.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Name.ToLower() == name.ToLower());
    }
}

