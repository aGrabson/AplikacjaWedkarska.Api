using AplikacjaWedkarska.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Services;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IFishingSpotService
    {
        public Task<IActionResult> GetFishingSpots();
        public Task<IActionResult> GetFishingSpotsByQuery(string searchQuery);

        public Task<IActionResult> GetUsersForFishingSpot(Guid fishingSpotId);
        public Task CheckIfReservationsActive(Guid id);
        public Task<IActionResult> GetFishingSpot(Guid fishingSpotId);
        public Task<IActionResult> GetRatingsForFishingSpot(Guid fishingSpotId, Guid AccountId);
        public Task<IActionResult> PostRatingForFishingSpot(RatingForFishingSpotDto ratingForFishingSpotDto, Guid AccountId);
        public Task<IActionResult> UpdateRatingForFishingSpot(RatingForFishingSpotDto ratingForFishingSpotDto, Guid AccountId);
    }
}
