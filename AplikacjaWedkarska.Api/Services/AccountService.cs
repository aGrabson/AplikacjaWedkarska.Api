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
    public class AccountService : IAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;

        public AccountService(ITokenService tokenService, DataContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<IActionResult> LoginUser(LoginUserDto loginData)
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            else
            {
                var accountEntity = await _context.Accounts.Where(x => x.Email == loginData.Email && x.IsDeleted == false).FirstOrDefaultAsync();
                if (accountEntity != null)
                {
                    SHA256 sha256 = SHA256Managed.Create();
                    byte[] bytes = Encoding.UTF8.GetBytes(loginData.Password);
                    byte[] hash = sha256.ComputeHash(bytes);
                    string password = Convert.ToBase64String(hash);
                    if (accountEntity.Password == password)
                    {
                        var result = new TokenInfoDto();
                        result.AccessToken = _tokenService.GenerateBearerToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());
                        result.RefreshToken = _tokenService.GenerateRefreshToken(accountEntity.Id.ToString(), accountEntity.RoleID.ToString());

                        return new OkObjectResult(result);
                    }
                    else
                    {
                        return new BadRequestResult();
                    }
                }
                else
                {
                    return new NotFoundResult();
                }
            }
        }
        public string HashPassword(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (_context.Accounts == null || _context.Cards == null)
            {
                return new NotFoundResult();
            }
            var cardEntity = await _context.Cards.FirstOrDefaultAsync(
                c => c.Id == registerUserDto.CardNumber &&
                     c.OwnerName == registerUserDto.Name &&
                     c.OwnerSurname == registerUserDto.Surname &&
                     c.Email == registerUserDto.Email
            );

            if (cardEntity == null)
            {
                return new NotFoundResult();
            }else if (cardEntity.IsRegistered == true)
            {
                return new BadRequestResult();
            }

            AccountEntity accountEntity = new AccountEntity
            {
                Id = Guid.NewGuid(),
                Name = registerUserDto.Name,
                Surname = registerUserDto.Surname,
                Email = registerUserDto.Email,
                Password = HashPassword(registerUserDto.Password),
                DateOfBirth = registerUserDto.DateOfBirth,
                RoleID = 2,
                CardID = cardEntity.Id.ToString(),
            };

            _context.Accounts.Add(accountEntity);
            cardEntity.IsRegistered = true;
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<IActionResult> GetInfoAboutUser(Guid accountId)
        {
            if (_context.Accounts == null )
            {
                return new NotFoundResult();
            }

            var accountEntity = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (accountEntity == null)
            {
                return new NotFoundResult();
            }

            var result = GetUserInfoDto(accountEntity);

            return new OkObjectResult(result);
        }
        public UserInfoDto GetUserInfoDto(AccountEntity accountEntity)
        {
            return new UserInfoDto
            {
                Name = accountEntity.Name,
                Surname = accountEntity.Surname,
                Email = accountEntity.Email,
                CardNumber = accountEntity.CardID,
            };

        }
        public async Task<IActionResult> UpdateInfoAboutUser(UpdateUserInfoDto updateUserInfoDto, Guid accountId)
        {
            if (_context.Accounts == null)
            {
                return new NotFoundResult();
            }

            var accountEntity = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
            if (EmailExists(updateUserInfoDto.Email) && (updateUserInfoDto.Email != accountEntity.Email))
            {
                return new BadRequestResult();
            }
            var cardEntity = await _context.Cards.FirstOrDefaultAsync(a => a.Id == accountEntity.CardID);
            if (cardEntity == null) 
            {
                return new NotFoundResult();
            }
            if (EmailExists(updateUserInfoDto.Email) && (updateUserInfoDto.Email != accountEntity.Email))
            {
                return new BadRequestResult();
            }
            if (accountEntity == null)
            {
                return new NotFoundResult();
            }
            accountEntity.Name = updateUserInfoDto.Name;
            accountEntity.Surname = updateUserInfoDto.Surname;
            accountEntity.Email = updateUserInfoDto.Email;
            cardEntity.Email = updateUserInfoDto.Email;
            cardEntity.OwnerName = updateUserInfoDto.Name;
            cardEntity.OwnerSurname = updateUserInfoDto.Surname;

            _context.Accounts.Update(accountEntity);
            _context.Cards.Update(cardEntity);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        public bool EmailExists(string email)
        {
            return (_context.Accounts?.Any(e => e.Email == email)).GetValueOrDefault();
        }
    }
}