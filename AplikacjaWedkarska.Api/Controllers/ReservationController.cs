using Microsoft.AspNetCore.Mvc;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using QuickTickets.Api.Services;
using System.Security.Claims;

namespace AplikacjaWedkarska.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("getUserReservations")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserReservations()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");
            return await _reservationService.GetUserReservations(userId);
        }

        [HttpGet("getReservationDetails/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReservationDetails(Guid id)
        {
            return await _reservationService.GetReservationDetails(id);
        }
    }
}

