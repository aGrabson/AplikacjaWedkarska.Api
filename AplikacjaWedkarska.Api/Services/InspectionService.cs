using AplikacjaWedkarska.Api.Data;
using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Text;

namespace AplikacjaWedkarska.Api.Services
{
    public class InspectionService : IInspectionService
    {
        private readonly DataContext _context; 
        private readonly IWebHostEnvironment _hostEnvironment;


        public InspectionService(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> ValidateUserCard(ValidateCardDto validateCardDto, Guid accountId)
        {
            var card = await _context.Cards.Where(x => x.Id == validateCardDto.CardId).FirstOrDefaultAsync();
            if (card == null)
            {
                return new NotFoundResult();
            }
            var account = await _context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if(account == null)
            {
                return new NotFoundResult();
            }
            var fishingSpot = await _context.FishingSpots
                .Where(x => x.Id == validateCardDto.FishingSpotId)
                .Select(x => x.Type)
                .FirstOrDefaultAsync();

            if (fishingSpot == null)
            {
                return new NotFoundResult();
            }
            bool hasPermission = false;

            if (fishingSpot == "Nizinna" && card.Lowland1Active)
            {
                hasPermission = true;
            }
            else if (fishingSpot == "Gorska I" && card.Mountain1Active)
            {
                hasPermission = true;
            }
            else if (fishingSpot == "Gorska II" && card.Mountain2Active)
            {
                hasPermission = true;
            }


            if (!hasPermission)
            {
                return new OkObjectResult(new { result = "Nieopłacona" });
            }
            return new OkObjectResult(new { result = "Opłacona" });

        }
        public async Task<IActionResult> ReleaseFishAsInspector(ReleaseFishDto releaseFishDto, Guid accountId)
        {
            var account = _context.Accounts.Where(x => x.Id == accountId).FirstOrDefault();
            if (account == null)
            {
                return new NotFoundResult();
            }
            var caughtFishes = await _context.CaughtFishes.Where(x => x.Id == releaseFishDto.CaughtFishId && x.ReservationId == releaseFishDto.ReservationId && x.Status == CaughtFishStatusEnum.Taken.ToString()).FirstOrDefaultAsync();
            if (caughtFishes == null)
            {
                return new NotFoundResult();
            }
            
            caughtFishes.Status = CaughtFishStatusEnum.ReleasedByController.ToString();

            _context.CaughtFishes.Update(caughtFishes);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task SaveFile(InspectionDto inspectionDto)
        {
            foreach (var base64Photo in inspectionDto.Base64Photos)
            {
                byte[] imageBytes = Convert.FromBase64String(base64Photo);
                string uniqueFileName = $"{Guid.NewGuid()}_{inspectionDto.ReservationId}.jpg";
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                await File.WriteAllBytesAsync(filePath, imageBytes);
                var photoEntity = new PhotoEntity
                {
                    ReservationId = inspectionDto.ReservationId,
                    FilePath = filePath,
                    FileName = uniqueFileName,
                };
                _context.Photos.Add(photoEntity);
            }
            await _context.SaveChangesAsync();
        }


        public async Task<IActionResult> PostInspection(InspectionDto inspectionDto, Guid accountId)
        {
            var user = await _context.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if (user == null)
            {
                return new NotFoundResult();
            }
            var reservation = await _context.Reservations.Where(x => x.Id == inspectionDto.ReservationId).FirstOrDefaultAsync();
            if (reservation==null)
            {
                return new NotFoundResult();
            }
            var inspection = new InspectionEntity
            {
                ControllerId = accountId,
                ReservationId = inspectionDto.ReservationId,
                Comment = inspectionDto.Comment,
            };

            await SaveFile(inspectionDto);

            _context.Inspections.Add(inspection);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
 