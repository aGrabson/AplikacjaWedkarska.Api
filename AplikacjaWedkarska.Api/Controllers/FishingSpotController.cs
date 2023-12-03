using Microsoft.AspNetCore.Mvc;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using AplikacjaWedkarska.Api.Dto;
using System.Security.Claims;

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
    }
}

