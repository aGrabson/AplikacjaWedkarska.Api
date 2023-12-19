using AplikacjaWedkarska.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IReservationService
    {
        public Task<IActionResult> GetUserReservations(Guid accountId, int pageNumber, int pageSize);
        public Task<IActionResult> GetReservationDetails(Guid id, Guid accountId);
        public Task<IActionResult> AddFishToReservation(AddCaughtFishDto addCaughtFishDto);
        public Task<IActionResult> GetUserFishes(Guid reservationId);
        public Task<IActionResult> Reserve(ReservationDto reservationDto, Guid accountId);
        public Task CheckIfReservationsActive(Guid accountId);
        public Task<IActionResult> GetFishList();
        public Task<IActionResult> ReleaseFish(ReleaseFishDto releaseFishDto, Guid accountId);
        public Task<IActionResult> CancelReservation(CancelReservationDto cancelReservationDto, Guid accountId);

        
    }
}
