using AplikacjaWedkarska.Api.Data;
using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Entities;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace QuickTickets.Api.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;

        public ReservationService(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetUserReservations(Guid accountId)
        {

            var reservations = await _context.Reservations.Include(x => x.Account).Include(x => x.FishingSpot).Where(x => x.AccountId == accountId).ToListAsync();
            return new OkObjectResult(reservations);
        }
        public async Task<IActionResult> GetReservationDetails(Guid id)
        {

            var reservation = await _context.Reservations.Include(x => x.FishingSpot).Where(x => x.Id == id).FirstOrDefaultAsync();
            return new OkObjectResult(reservation);
        }
    }
}