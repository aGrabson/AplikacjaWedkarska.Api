using AplikacjaWedkarska.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IInspectionService
    {
        public Task<IActionResult> ValidateUserCard(ValidateCardDto validateCardDto, Guid accountId);
        public Task<IActionResult> ReleaseFishAsInspector(ReleaseFishDto releaseFishDto, Guid accountId);
        public Task<IActionResult> PostInspection(InspectionDto inspectionDto, Guid accountId);

    }
}
