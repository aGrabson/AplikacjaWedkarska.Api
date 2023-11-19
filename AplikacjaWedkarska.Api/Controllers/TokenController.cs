using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaWedkarska.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public IActionResult RefreshBearer([FromBody] TokenInfoDto tokenData)
        {
            var result = _tokenService.RefreshBearerToken(tokenData);
            if (result == null)
                return Unauthorized();
            else
                return Ok(result);
        }
    }
}
