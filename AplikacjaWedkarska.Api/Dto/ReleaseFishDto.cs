using AplikacjaWedkarska.Api.Entities;

namespace AplikacjaWedkarska.Api.Dto
{
    public class ReleaseFishDto
    {
        public Guid CaughtFishId { get; set; }
        public Guid ReservationId { get; set; }
        public string Status { get; set; } = CaughtFishStatusEnum.Released.ToString();
    }
}
