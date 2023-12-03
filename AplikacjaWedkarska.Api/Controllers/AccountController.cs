using Microsoft.AspNetCore.Mvc;
using AplikacjaWedkarska.Api.Services;
using Microsoft.AspNetCore.Authorization;
using AplikacjaWedkarska.Api.Dto;
using System.Security.Claims;

namespace AplikacjaWedkarska.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody]LoginUserDto loginData)
        {
            return await _accountService.LoginUser(loginData);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDto registerUserDto)
        {
            return await _accountService.RegisterUser(registerUserDto);
        }
        [HttpGet("getInfoAboutUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetInfoAboutUser()
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Guid userId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23");

            return await _accountService.GetInfoAboutUser(userId);
        }
        [HttpPut("updateInfoAboutUser")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateInfoAboutUser([FromBody] UpdateUserInfoDto updateUserInfoDto)
        {
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return await _accountService.UpdateInfoAboutUser(updateUserInfoDto, userId);
        }
    }
}

