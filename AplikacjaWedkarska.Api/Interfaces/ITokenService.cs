using Microsoft.AspNetCore.Mvc;
using AplikacjaWedkarska.Api.Dto;

namespace AplikacjaWedkarska.Api.Services
{
    public interface ITokenService
    {
        TokenInfoDto RefreshBearerToken(TokenInfoDto oldTokens);
        public string GenerateBearerToken(string id, string role);
        public string GenerateRefreshToken(string id, string role);
    }
}