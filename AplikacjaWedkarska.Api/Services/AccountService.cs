using AplikacjaWedkarska.Api.Data;
using AplikacjaWedkarska.Api.Dto;
using AplikacjaWedkarska.Api.Services;
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

        public TokenInfoDto LoginUser(LoginUserDto loginData)
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            else
            {
                var accountEntity = _context.Accounts.Where(x => x.Email == loginData.Email && x.IsDeleted == false).FirstOrDefault();
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

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }


    }
}