using Microsoft.AspNetCore.Mvc;

using Waracle.HotelBooking.Domain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Waracle.HotelBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public HotelsController(IBookingService bookingService)
        {
            _bookingService = bookingService;

        }

        /// <summary>
        /// Find a hotel by its name.
        /// </summary>
        /// <param name="name">Hotel name</param>
        /// <returns>Hotel details</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("search")]
        public async Task<IActionResult> FindHotel(string name)
        {
            var hotel = await _bookingService.FindHotelByName(name);

            return hotel == null ? NotFound() : Ok(hotel);
        }

        /// <summary>
        /// Get available rooms between two dates for a number of guests.
        /// </summary>
        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAvailableRooms(int hotelId, DateTime start, DateTime end, int guests)
        {

            var available = await _bookingService.GetAvailableRooms(hotelId, start, end, guests);

            return Ok(available);
        }

    }
}
