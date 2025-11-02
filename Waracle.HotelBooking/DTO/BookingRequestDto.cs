namespace Waracle.HotelBooking.DTO
{
    public class BookingRequestDto
    {
        public int HotelId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }
    }
}
