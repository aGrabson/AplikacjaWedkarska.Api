using AplikacjaWedkarska.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace AplikacjaWedkarska.Api.Services
{
    public interface IFileService
    {
        public Task<IActionResult> GetRulesPdf();
    }
}
