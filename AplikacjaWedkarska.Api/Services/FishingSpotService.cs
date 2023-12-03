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
    public class FishingSpotService : IFishingSpotService
    {
        private readonly DataContext _context;

        public FishingSpotService( DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetFishingSpots()
        {
            var spots = await _context.FishingSpots.ToListAsync();
            return new OkObjectResult(spots);
        }
    }
}