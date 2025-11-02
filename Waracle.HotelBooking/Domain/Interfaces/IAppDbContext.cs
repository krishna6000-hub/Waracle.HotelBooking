
using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Domain.Models;

namespace Waracle.HotelBooking.Domain.Interfaces;

public interface IAppDbContext : IDbContext
{
    DbSet<Hotel> Hotels { get; set; }

    DbSet<Room> Rooms { get; set; }

    DbSet<Booking> Bookings { get; set; }
}