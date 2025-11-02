using Microsoft.AspNetCore.Mvc;
using Waracle.HotelBooking.Infrastructure.DbContext;
using Waracle.HotelBooking.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Waracle.HotelBooking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Seed the database with test data.
        /// </summary>
        [HttpPost("seed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Seed()
        {
           
            SeedData.Seed(_context);
            return Ok("Database seeded.");
        }

        /// <summary>
        /// Reset the database to empty state.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            SeedData.Reset(_context);
            return Ok("Database reset.");
        }
    }

}
