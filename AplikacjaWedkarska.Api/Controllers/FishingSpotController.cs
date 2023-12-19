using Microsoft.AspNetCore.Mvc;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using AplikacjaWedkarska.Api.Dto;
using System.Security.Claims;
using QuickTickets.Api.Services;

namespace AplikacjaWedkarska.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishingSpotController : ControllerBase
    {
        private readonly IFishingSpotService _fishingSpotService;

        public FishingSpotController(IFishingSpotService fishingSpotService)
        {
            _fishingSpotService = fishingSpotService;
        }

        [HttpGet("getFishingSpots")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFishingSpots()
        {
            return await _fishingSpotService.GetFishingSpots();
        }
        [HttpGet("getFishingSpotsByQuery/{searchQuery}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFishingSpotsByQuery(string searchQuery)
        {
            return await _fishingSpotService.GetFishingSpotsByQuery(searchQuery);
        }
        [HttpGet("getUsersForFishingSpot/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersForFishingSpot(Guid id)
        {
            return await _fishingSpotService.GetUsersForFishingSpot(id);
        }
        [HttpGet("getFishingSpot/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFishingSpot(Guid id)
        {
            return await _fishingSpotService.GetFishingSpot(id);
        }
        [HttpGet("getRatingsForFishingSpot/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRatingsForFishingSpot(Guid id)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("22BBB16C-7C2C-13A4-557D-7D1AA32D4A23");

            return await _fishingSpotService.GetRatingsForFishingSpot(id, userId);
        }
        [HttpPost("postRatingForFishingSpot/")]
        [AllowAnonymous]
        public async Task<IActionResult> PostRatingForFishingSpot(RatingForFishingSpotDto ratingForFishingSpotDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("22BBB16C-7C2C-13A4-557D-7D1AA32D4A23");

            return await _fishingSpotService.PostRatingForFishingSpot(ratingForFishingSpotDto, userId);
        }
        [HttpPut("updateRatingForFishingSpot/")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateRatingForFishingSpot(RatingForFishingSpotDto ratingForFishingSpotDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("22BBB16C-7C2C-13A4-557D-7D1AA32D4A23");
            return await _fishingSpotService.UpdateRatingForFishingSpot(ratingForFishingSpotDto, userId);
        }
    }
}

