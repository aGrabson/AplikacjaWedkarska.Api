using AplikacjaWedkarska.Api.Data;
using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Entities;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using XAct;
using XAct.Domain.Repositories;

namespace QuickTickets.Api.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;

        public ReservationService(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetUserReservations(Guid accountId, int pageNumber , int pageSize )
        {
            await CheckIfReservationsActive(accountId);

            var totalItems = await _context.Reservations.Where(x => x.AccountId == accountId && x.IsCancelled == false).CountAsync(x => x.AccountId == accountId);
            var reservations = await _context.Reservations
                .Include(x => x.Account)
                .Include(x => x.FishingSpot)
                .OrderByDescending(x => x.IsActive)
                .ThenByDescending(x => x.ReservationStart)
                .Where(x => x.AccountId == accountId && x.IsCancelled == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                TotalItems = totalItems,
                Reservations = reservations
            };

            return new OkObjectResult(result);
        }

        public async Task CheckIfReservationsActive(Guid accountId)
        {
            var now = DateTime.Now;
            var activeReservations = await _context.Reservations.Where(x => x.AccountId == accountId && x.IsCancelled == false).ToListAsync();

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

        public async Task<IActionResult> GetReservationDetails(Guid id, Guid accountId)
        {
            await CheckIfReservationsActive(accountId);
            var reservation = await _context.Reservations.Include(x => x.FishingSpot).Where(x => x.Id == id && x.IsCancelled == false).FirstOrDefaultAsync();
            return new OkObjectResult(reservation);
        }
        public async Task<IActionResult> AddFishToReservation(AddCaughtFishDto addCaughtFishDto)
        {
            var reservation = await _context.Reservations.FindAsync(addCaughtFishDto.ReservationId);
            if (reservation == null)
            {
                return new NotFoundResult();
            }
            if (reservation.IsActive == false)
            {
                return new BadRequestResult();
            }
            var fish = await _context.Fishes.FindAsync(addCaughtFishDto.FishId);
            if (fish == null)
            {
                return new NotFoundResult();
            }
            if (fish.ProtectionPeriodStart.HasValue && fish.ProtectionPeriodEnd.HasValue)
            {
                var protectionStartDate = new DateTime(1, fish.ProtectionPeriodStart.Value.Month, fish.ProtectionPeriodStart.Value.Day);
                
                var protectionEndDate = new DateTime(1, fish.ProtectionPeriodEnd.Value.Month, fish.ProtectionPeriodEnd.Value.Day);

                if(protectionEndDate.Month < protectionStartDate.Month)
                {
                   protectionEndDate = protectionEndDate.AddYears(1);
                }
                var catchDate = new DateTime(1, addCaughtFishDto.CatchDateTime.Month, addCaughtFishDto.CatchDateTime.Day);
                if (catchDate >= protectionStartDate && catchDate <= protectionEndDate)
                {
                    return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas dodawania ryby", errorText = $"Ryba {fish.Species} objęta okresem ochronnym!" });
                }
            }

            var fishLimits = await _context.FishingSpotLimits.FirstOrDefaultAsync(limit => limit.FishId == addCaughtFishDto.FishId && limit.FishingSpotId == reservation.FishingSpotId);
            var countFishes = await _context.CaughtFishes.Where(x => x.ReservationId == addCaughtFishDto.ReservationId && x.FishId == addCaughtFishDto.FishId && x.Status == CaughtFishStatusEnum.Taken.ToString()).CountAsync();
            
            if (fish.MinimumSize > addCaughtFishDto.Size)
            {
                return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas dodawania ryby", errorText = $"Ryba {fish.Species} niewymiarowa!" });
            }
            else if (fish.MaximumSize < addCaughtFishDto.Size)
            {
                return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas dodawania ryby", errorText = $"Ryba {fish.Species} ponadwymiarowa!" });
            }

            if (fishLimits != null)
            {
                if (fishLimits.DailyLimit <= countFishes)
                {
                    return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas dodawania ryby", errorText = $"Limit ryby {fish.Species} przekroczony!" });
                }
            }
            else if(fish.DailyLimit != 0)
            {
                if(fish.DailyLimit < 0)
                {
                    var CaughtFishesBySpecies = await _context.CaughtFishes.Where(x => x.ReservationId == addCaughtFishDto.ReservationId && x.FishId == addCaughtFishDto.FishId && x.Status == CaughtFishStatusEnum.Taken.ToString()).ToListAsync();
                    double? weight = 0;
                    foreach( var fishToBeWeight in CaughtFishesBySpecies )
                    {
                        weight += fishToBeWeight.Weight;
                    }
                    if ((weight + addCaughtFishDto.Weight) > (fish.DailyLimit * (-1)))
                    {
                        return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas dodawania ryby", errorText = $"Limit wagowy ryby {fish.Species} przekroczony!" });
                    }
                }
                else if (fish.DailyLimit <= countFishes)
                {
                    return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas dodawania ryby", errorText = $"Limit ryby {fish.Species} przekroczony!" });
                }
            }
            else 
            {
                return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas dodawania ryby", errorText = $"Ryba {fish.Species} niemożliwa do zabrania!" });
            }
            

            var caughtFish = new CaughtFishEntity
            {
                FishId = addCaughtFishDto.FishId,
                Size = addCaughtFishDto.Size,
                Weight = addCaughtFishDto.Weight,
                ReservationId = addCaughtFishDto.ReservationId,
            };

            _context.CaughtFishes.Add(caughtFish);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public CaughtFishInfoDto GetCaughtFishInfo(CaughtFishEntity caughtFish)
        {
            return new CaughtFishInfoDto
            {
                Id = caughtFish.Id,
                FishId = caughtFish.FishId,
                Size = caughtFish.Size,
                Weight = caughtFish.Weight,
                CatchDateTime = caughtFish.CatchDateTime,
                Status = caughtFish.Status,
                ReservationId = caughtFish.ReservationId,
            };

        }

        public async Task<IActionResult> GetUserFishes(Guid reservationId)
        {
            var fishes = await _context.CaughtFishes.Include(x => x.Fish).Where(x => x.ReservationId == reservationId && x.Status == CaughtFishStatusEnum.Taken.ToString()).ToListAsync();

            List<CaughtFishesBySpeciesDto> listOfFishesBySpecies = new();
            foreach (var fish in fishes) 
            {
                var tmp = GetCaughtFishInfo(fish);

                if (listOfFishesBySpecies.Any(x => x.Species == fish.Fish.Species))
                {
                    listOfFishesBySpecies.First(x => x.Species == fish.Fish.Species).FishList.Add(tmp);
                }
                else
                {
                    CaughtFishesBySpeciesDto caughtFishesBySpeciesDto = new()
                    {
                        Species = fish.Fish.Species
                    };
                    caughtFishesBySpeciesDto.FishList.Add(tmp);
                    listOfFishesBySpecies.Add(caughtFishesBySpeciesDto);
                }
            }

            return new OkObjectResult(listOfFishesBySpecies);
        }
        public async Task<IActionResult> Reserve(ReservationDto reservationDto, Guid accountId)
        {
            var account = await _context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();

            if (account == null)
            {
                return new NotFoundResult();
            }
            var fishingSpot = await _context.FishingSpots.Where(x => x.Id == reservationDto.FishingSpotId).FirstOrDefaultAsync();
            if (fishingSpot == null)
            {
                return new NotFoundResult();
            };
            var existingReservation = await _context.Reservations
                .Where(r => r.FishingSpotId == reservationDto.FishingSpotId && r.AccountId == accountId && r.IsCancelled == false && r.ReservationStart <= reservationDto.ReservationStart && r.ReservationStart >= reservationDto.ReservationStart.AddHours(-24))
                .FirstOrDefaultAsync();
            var existingReservationBefore = await _context.Reservations
            .Where(r => r.FishingSpotId == reservationDto.FishingSpotId && r.AccountId == accountId && r.IsCancelled == false && r.ReservationStart >= reservationDto.ReservationStart && r.ReservationStart <= reservationDto.ReservationStart.AddHours(24))
            .FirstOrDefaultAsync();
            if (existingReservationBefore != null)
            {
                return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas rezerwacji", errorText = $"Rezerwacja na dzień: {reservationDto.ReservationStart.ToString("dd.MM.yyyy HH:mm")} jest niemożliwa. W ciągu 24 godzin po proponowanym początku rezerwacji istnieje aktywna rezerwacja!" });
            }
            if (existingReservation != null)
            {
                return new BadRequestObjectResult(new { error = "Wystąpił błąd podczas rezerwacji", errorText = $"Rezerwacja na dzień: {reservationDto.ReservationStart.ToString("dd.MM.yyyy")} jest niemożliwa. Już jest aktywna w tym dniu inna rezerwacja!" });
            }
            var reservation = new ReservationEntity
            {
                ReservationStart = reservationDto.ReservationStart,
                AccountId = account.Id,
                FishingSpotId = fishingSpot.Id
            };
            if (reservationDto.ReservationStart <= DateTime.Now.AddHours(24))
            {
                reservation.IsActive = true;
            }

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public async Task<IActionResult> GetFishList()
        {
            var fishes = await _context.Fishes.OrderBy(x => x.Species).ToListAsync();

            return new OkObjectResult(fishes);
        }
        public async Task<IActionResult> ReleaseFish(ReleaseFishDto releaseFishDto, Guid accountId)
        {

            var caughtFishes = await _context.CaughtFishes.Where(x => x.Id == releaseFishDto.CaughtFishId && x.ReservationId == releaseFishDto.ReservationId && x.Status == CaughtFishStatusEnum.Taken.ToString()).FirstOrDefaultAsync();
            if(caughtFishes == null)
            {
                return new NotFoundResult();
            }
            caughtFishes.Status = CaughtFishStatusEnum.Released.ToString();

            _context.CaughtFishes.Update(caughtFishes);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        public async Task<IActionResult> CancelReservation(CancelReservationDto cancelReservationDto, Guid accountId)
        {
            var reservation = await _context.Reservations.Where(x => x.Id == cancelReservationDto.ReservationId && x.AccountId == accountId && x.IsCancelled == false).FirstOrDefaultAsync();

            if (reservation == null)
            {
                return new NotFoundResult();
            }
            await CheckIfReservationsActive(accountId);
            if (reservation.IsActive || reservation.ReservationStart <= DateTime.Now)
            {
                return new BadRequestResult();
            }

            reservation.IsActive = false;
            reservation.IsCancelled = true;
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
