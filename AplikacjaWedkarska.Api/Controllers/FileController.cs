using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaWedkarska.Api.Controllers
{
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
        _fileService = fileService;
        }

        [HttpGet("getRulesPdf")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRulesPdf()
        {
            return await _fileService.GetRulesPdf();
        }
    }
}