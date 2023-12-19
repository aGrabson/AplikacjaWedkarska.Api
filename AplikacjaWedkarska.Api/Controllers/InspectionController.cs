
using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Services;
using System.Security.Claims;

namespace AplikacjaWedkarska.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionService _inspectionService;

        public InspectionController(IInspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        [HttpPost("validateUserCard")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateUserCard([FromBody] ValidateCardDto validateCardDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");
            return await _inspectionService.ValidateUserCard(validateCardDto, userId);
        }
        [HttpPut("releaseFishAsInspector")]
        [AllowAnonymous]
        public async Task<IActionResult> ReleaseFishAsInspector(ReleaseFishDto releaseFishDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");

            return await _inspectionService.ReleaseFishAsInspector(releaseFishDto, userId);
        }
        [HttpPost("postInspection")]
        [AllowAnonymous]
        public async Task<IActionResult> PostInspection([FromBody] InspectionDto inspectionDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");
            return await _inspectionService.PostInspection(inspectionDto, userId);
        }
    }
}