using Microsoft.EntityFrameworkCore;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Helpers;

namespace Waracle.HotelBooking.Infrastructure.DbContext
{
    /// <summary>
    /// EF Core DB Context
    /// </summary>


    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
          
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set-up referential integrety constaints between defined EF entities

            modelBuilder.Entity<Hotel>().HasMany(h => h.Rooms).WithOne(r => r.Hotel).HasForeignKey(r => r.HotelId);
            modelBuilder.Entity<Room>().HasMany(r => r.Bookings).WithOne(b => b.Room).HasForeignKey(b => b.RoomId);
            modelBuilder.Entity<Booking>().HasIndex(b => b.Reference).IsUnique();
        }
    }


}
