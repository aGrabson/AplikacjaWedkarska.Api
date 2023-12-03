using AplikacjaWedkarska.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IReservationService
    {
        public Task<IActionResult> GetUserReservations(Guid accountId);
        public Task<IActionResult> GetReservationDetails(Guid id);

    }
}
