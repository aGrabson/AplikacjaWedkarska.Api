using AplikacjaWedkarska.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AplikacjaWedkarska.Api.Services
{
    public class FileService : IFileService
    {
        private readonly DataContext _context;

        public FileService(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetRulesPdf()
        {
            int currentYear = DateTime.Now.Year;
            var pdfFile = await _context.PdfFiles.Where(x => x.ExpirationYear == currentYear).FirstOrDefaultAsync();
            if (pdfFile == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(new { PdfContentBase64 = pdfFile.Content });
        }
    }
}
