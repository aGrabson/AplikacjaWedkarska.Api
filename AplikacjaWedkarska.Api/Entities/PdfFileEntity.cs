using System.Buffers.Text;

namespace AplikacjaWedkarska.Api.Entities
{
    public class PdfFileEntity : BaseDbItem
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public int ExpirationYear { get; set; }
    }
}
