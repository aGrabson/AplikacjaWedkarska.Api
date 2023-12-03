using AplikacjaWedkarska.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IAccountService
    {
        public Task<IActionResult> LoginUser(LoginUserDto loginData);
        public Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto);
        public string HashPassword(string password);
        public Task<IActionResult> GetInfoAboutUser(Guid accountId);
        public Task<IActionResult> UpdateInfoAboutUser(UpdateUserInfoDto updateUserInfoDto, Guid accountId);
    }
}
