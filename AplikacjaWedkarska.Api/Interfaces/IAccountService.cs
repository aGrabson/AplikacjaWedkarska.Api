using AplikacjaWedkarska.Api.Dto;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IAccountService
    {
        public TokenInfoDto LoginUser(LoginUserDto loginData);
    }
}
