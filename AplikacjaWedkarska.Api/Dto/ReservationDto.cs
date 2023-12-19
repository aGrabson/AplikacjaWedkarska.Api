using AplikacjaWedkarska.Api.Entities;

namespace AplikacjaWedkarska.Api.Dto
{
    public class ReservationDto
    {
        public DateTime ReservationStart { get; set; }
        public Guid FishingSpotId { get; set; }
    }
}
