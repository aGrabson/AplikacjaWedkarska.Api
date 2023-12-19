using Microsoft.AspNetCore.Mvc;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using QuickTickets.Api.Services;
using System.Security.Claims;
using AplikacjaWedkarska.Api.Dto;

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
        public async Task<IActionResult> GetUserReservations(int pageNumber, int pageSize)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");
            return await _reservationService.GetUserReservations(userId, pageNumber, pageSize);
        }

        [HttpGet("getReservationDetails/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReservationDetails(Guid id)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");
            return await _reservationService.GetReservationDetails(id, userId);
        }

        [HttpPost("addFishToReservation")]
        [AllowAnonymous]
        public async Task<IActionResult> AddFishToReservation(AddCaughtFishDto addCaughtFishDto)
        {
            return await _reservationService.AddFishToReservation(addCaughtFishDto);
        }

        [HttpGet("getUserFishes/{reservationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserFishes(Guid reservationId)
        {
            return await _reservationService.GetUserFishes(reservationId);
        }
        [HttpPost("reserve")]
        [AllowAnonymous]
        public async Task<IActionResult> Reserve(ReservationDto reservationDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");

            return await _reservationService.Reserve(reservationDto, userId);
        }
        [HttpGet("getFishList")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFishList()
        {
            return await _reservationService.GetFishList();
        }
        [HttpPut("releaseFish")]
        [AllowAnonymous]
        public async Task<IActionResult> ReleaseFish(ReleaseFishDto releaseFishDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");

            return await _reservationService.ReleaseFish(releaseFishDto, userId);
        }
        [HttpPut("cancelReservation")]
        [AllowAnonymous]
        public async Task<IActionResult> CancelReservation(CancelReservationDto cancelReservationDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");

            return await _reservationService.CancelReservation(cancelReservationDto, userId);
        }
    }
}

