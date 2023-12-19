using AplikacjaWedkarska.Api.Data;
using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Entities;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        public async Task<IActionResult> GetFishingSpotsByQuery(string searchQuery)
        {
            IQueryable<FishingSpotEntity> query = _context.FishingSpots;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var searchTerms = searchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in searchTerms)
                {
                    query = query.Where(s => s.Title.Contains(term));
                }
            }
            

            var spots = await query.ToListAsync();
            return new OkObjectResult(spots);
        }
        public async Task<IActionResult> GetUsersForFishingSpot(Guid fishingSpotId)
        {
            await CheckIfReservationsActive(fishingSpotId);
            var usersWithReservations = await _context.Reservations
                .Where(r => r.FishingSpotId == fishingSpotId && r.IsActive && r.IsCancelled == false)
                .Select(r => new { r.Account, Reservation = r })
                .ToListAsync();
            
            if (usersWithReservations == null)
            {
                return new NotFoundResult();
            }
            var usersList = new List<UserInfoDto>();
            foreach (var user in usersWithReservations)
            {
                var userDto = new UserInfoDto
                {
                    Name = user.Account.Name,
                    Surname = user.Account.Surname,
                    Email = user.Account.Email,
                    CardNumber = user.Account.CardID,
                    ReservationId = user.Reservation.Id
                };

                usersList.Add(userDto);
            }

            return new OkObjectResult(usersList);

        }

        public async Task CheckIfReservationsActive(Guid id)
        {
            var now = DateTime.Now;
            var activeReservations = await _context.Reservations.Where(x => x.FishingSpotId == id && x.IsCancelled == false).ToListAsync();

            foreach (var reservation in activeReservations)
            {
                var reservationEnd = reservation.ReservationStart.AddHours(24);

                if (reservationEnd >= now && reservation.ReservationStart <= now)
                {
                    reservation.IsActive = true;
                }
                else if (reservation.ReservationStart.AddHours(24) < now && reservation.IsActive)
                {
                    reservation.IsActive = false;
                }
                else
                {
                    reservation.IsActive = false;
                }

                _context.Reservations.Update(reservation);
            }

            await _context.SaveChangesAsync();

        }

        public async Task<IActionResult> GetFishingSpot(Guid fishingSpotId)
        {
            var fishingSpot = await _context.FishingSpots.Where(x => x.Id == fishingSpotId).FirstOrDefaultAsync();
            return new OkObjectResult(fishingSpot);
        }

        public async Task<IActionResult> GetRatingsForFishingSpot(Guid fishingSpotId, Guid accountId)
        {
            var user = await _context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if(user == null)
            {
                return new NotFoundResult();
            }
            var fishingSpot = await _context.FishingSpots.Where(x => x.Id == fishingSpotId).FirstOrDefaultAsync();
            if (fishingSpot == null)
            {
                return new NotFoundResult();
            }
            var ratings = await _context.Ratings
            .Where(x => x.FishingSpotId == fishingSpotId)
            .ToListAsync();
            
            var averageRating = ratings.Any() ? ratings.Average(x => x.Rating) : 0;
            var userRating = await _context.Ratings.Where(x => x.FishingSpotId == fishingSpotId).Where(y => y.AccountId == accountId).FirstOrDefaultAsync();
            
            return new OkObjectResult(new {Ratings = averageRating, UserRating = userRating?.Rating, Total = ratings.Count});
        }

        public async Task<IActionResult> PostRatingForFishingSpot(RatingForFishingSpotDto ratingForFishingSpotDto, Guid accountId)
        {
            var user = await _context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if (user == null)
            {
                return new NotFoundResult();
            }
            var fishingSpot = await _context.FishingSpots.Where(x => x.Id == ratingForFishingSpotDto.FishingSpotId).FirstOrDefaultAsync();
            if (fishingSpot == null)
            {
                return new NotFoundResult();
            }
            var rating = new RatingEntity
            {
                AccountId = accountId,
                FishingSpotId = ratingForFishingSpotDto.FishingSpotId,
                Rating = ratingForFishingSpotDto.Rating,
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> UpdateRatingForFishingSpot(RatingForFishingSpotDto ratingForFishingSpotDto, Guid accountId)
        {
            var rating = await _context.Ratings.Where(x => x.FishingSpotId == ratingForFishingSpotDto.FishingSpotId && x.AccountId == accountId).FirstOrDefaultAsync();
            if (rating == null) 
            {
                return new NotFoundResult();
            }

            rating.Rating = ratingForFishingSpotDto.Rating;

            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}