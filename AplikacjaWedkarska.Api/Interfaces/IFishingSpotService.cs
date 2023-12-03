using AplikacjaWedkarska.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IFishingSpotService
    {
        public Task<IActionResult> GetFishingSpots();
    }
}
