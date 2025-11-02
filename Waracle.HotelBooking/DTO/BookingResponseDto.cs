namespace Waracle.HotelBooking.DTO
{
    public class BookingResponseDto
    {
        public string Reference { get; set; }
        public string RoomType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
