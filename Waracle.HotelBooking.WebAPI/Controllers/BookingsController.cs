using Microsoft.AspNetCore.Mvc;
using Waracle.HotelBooking.Domain.Interfaces;
using Waracle.HotelBooking.DTO;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Get available rooms between two dates for a number of guests.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{hotelId}/available-rooms/{start}/{end}/{guests}")]
    public async Task<IActionResult> GetAvailableRooms(int hotelId, DateTime start, DateTime end, int guests)
    {

        try 
        {
            var rooms = await _bookingService.GetAvailableRooms(hotelId, start, end, guests);

            return Ok(rooms);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
       
    }


    /// <summary>
    /// Book a room for a hotel.
    /// </summary>
    [HttpPost("book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> BookRoom([FromBody] BookingRequestDto request)
    {
        try
        {
            var booking = await _bookingService.BookRoom(request.HotelId, request.StartDate, request.EndDate, request.GuestCount);
            return booking == null ? BadRequest("No available room found.") : Ok(booking);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>
    /// Get booking details by reference.
    /// </summary>
    [HttpGet("{reference}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookingByReference(string reference)
    {
        var booking = await _bookingService.GetBookingByReference(reference);
        return booking == null ? NotFound() : Ok(booking);
    }
}
