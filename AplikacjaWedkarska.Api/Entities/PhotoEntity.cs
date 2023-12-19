using System.ComponentModel.DataAnnotations.Schema;

namespace AplikacjaWedkarska.Api.Entities
{
    public class PhotoEntity :BaseDbItem
    {
        public Guid ReservationId { get; set; }
        public virtual ReservationEntity? Reservation { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }

    }
}
