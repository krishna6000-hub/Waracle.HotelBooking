using Waracle.HotelBooking.Domain.Definitions;
using Waracle.HotelBooking.Domain.Models;
using Waracle.HotelBooking.Infrastructure.DbContext;

namespace Waracle.HotelBooking.Helpers
{
    public static class SeedData
    {
        public static void Seed(AppDbContext context)
        {

            if (context.Hotels.Any()) return;

            var hotelAlpha = new Hotel { Name = "Alpha" };
            var roomsAlpha = new List<Room>
            {
               new Room { Type = RoomType.Single, Capacity = 1 },
               new Room { Type = RoomType.Single, Capacity = 1 },
               new Room { Type = RoomType.Double, Capacity = 2 },
               new Room { Type = RoomType.Double, Capacity = 2 },
               new Room { Type = RoomType.Deluxe, Capacity = 4 },
               new Room { Type = RoomType.Deluxe, Capacity = 4 }
             };

            hotelAlpha.Rooms = roomsAlpha;
            context.Hotels.Add(hotelAlpha);

            var hotelBeta = new Hotel { Name = "Beta" };
            var roomsBeta = new List<Room>
            {
                new Room {  Type = RoomType.Double, Capacity = 2 },
                new Room {  Type = RoomType.Double, Capacity = 2 },
                new Room {  Type = RoomType.Double, Capacity = 2 },
                new Room {  Type = RoomType.Double, Capacity = 2 },
                new Room {  Type = RoomType.Deluxe, Capacity = 2 },
                new Room {  Type = RoomType.Deluxe, Capacity = 2 }
             };

            hotelBeta.Rooms = roomsBeta;
            context.Hotels.Add(hotelBeta);

            var hotelGamma = new Hotel { Name = "Gamma" };
            var roomsGamma = new List<Room>
            {
                new Room { Type = RoomType.Single, Capacity = 1 },
                new Room { Type = RoomType.Single, Capacity = 1 },
                new Room { Type = RoomType.Double, Capacity = 2 },
                new Room { Type = RoomType.Double, Capacity = 2 },
                new Room { Type = RoomType.Deluxe, Capacity = 4 },
                new Room { Type = RoomType.Deluxe, Capacity = 4 }
             };

            hotelGamma.Rooms = roomsGamma;
            context.Hotels.Add(hotelGamma);

            var hotelDelta = new Hotel { Name = "Delta" };
            var roomsDelta = new List<Room>
            {
                new Room { Type = RoomType.Deluxe, Capacity = 2 },
                new Room { Type = RoomType.Deluxe, Capacity = 2 },
                new Room { Type = RoomType.Deluxe, Capacity = 2 },
                new Room { Type = RoomType.Deluxe, Capacity = 4 },
                new Room { Type = RoomType.Deluxe, Capacity = 4 },
                new Room { Type = RoomType.Deluxe, Capacity = 4 }
             };

            hotelDelta.Rooms = roomsDelta;
            context.Hotels.Add(hotelDelta);

            var hotelEpsilon = new Hotel { Name = "Epsilon" };
            var roomsEpsilon = new List<Room>
            {
                new Room { Type = RoomType.Double, Capacity = 2 },
                new Room { Type = RoomType.Double, Capacity = 2 },
                new Room { Type = RoomType.Double, Capacity = 2 },
                new Room { Type = RoomType.Double, Capacity = 2 },
                new Room { Type = RoomType.Double, Capacity = 2 },
                new Room { Type = RoomType.Double, Capacity = 2 }
             };

            hotelEpsilon.Rooms = roomsEpsilon;
            context.Hotels.Add(hotelEpsilon);

            var hotelZeta = new Hotel { Name = "Zeta" };
            var roomsZeta = new List<Room>
            {
                new Room { Type = RoomType.Single, Capacity = 1 },
                new Room { Type = RoomType.Single, Capacity = 1 },
                new Room { Type = RoomType.Single, Capacity = 1 },
                new Room { Type = RoomType.Single, Capacity = 1 },
                new Room { Type = RoomType.Deluxe, Capacity = 2 },
                new Room { Type = RoomType.Deluxe, Capacity = 4 }
             };

            hotelZeta.Rooms = roomsZeta;
            context.Hotels.Add(hotelZeta);

            context.SaveChanges();


            /*
            var bookings = new List<Booking>
            {
                new Booking {  StartDate = DateTime.Parse("10-11-2025"), EndDate = DateTime.Parse("15-11-2025"), GuestCount = 1, Id = 1, Reference = "87132ff9-b354-443b-aba3-7f9d06bb94d7", RoomId =1},
                new Booking {  StartDate = DateTime.Parse("10-11-2025"), EndDate = DateTime.Parse("15-11-2025"), GuestCount = 1, Id = 2, Reference = "4acc8ceb-621d-41f1-ae74-de01badd49ee", RoomId =2},
                new Booking {  StartDate = DateTime.Parse("10-11-2025"), EndDate = DateTime.Parse("15-11-2025"), GuestCount = 1, Id = 3, Reference = "6efa9f02-9f39-4bdd-bd81-7150db1a4940", RoomId =3},
                new Booking {  StartDate = DateTime.Parse("10-11-2025"), EndDate = DateTime.Parse("15-11-2025"), GuestCount = 2, Id = 4, Reference = "82f75151-3788-49b0-b534-22ff6b0ffaa5", RoomId =4},
                new Booking {  StartDate = DateTime.Parse("10-11-2025"), EndDate = DateTime.Parse("15-11-2025"), GuestCount = 2, Id = 5, Reference = "18fde892-435d-4def-a149-aa7b489f3a2c", RoomId =5}
      
             };


            context.Bookings.AddRange(bookings);
            context.SaveChanges();
            */
        }

        public static void Reset(AppDbContext context)
        {

            context.Bookings.RemoveRange(context.Bookings);
            context.Rooms.RemoveRange(context.Rooms);
            context.Hotels.RemoveRange(context.Hotels);
            context.SaveChanges();
        }
    }
}
