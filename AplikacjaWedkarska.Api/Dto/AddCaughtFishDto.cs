using AplikacjaWedkarska.Api.Entities;

namespace AplikacjaWedkarska.Api.Dto
{
    public class AddCaughtFishDto
    {
        public Guid FishId { get; set; }
        public double? Size { get; set; }
        public double? Weight { get; set; }
        public DateTime CatchDateTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = CaughtFishStatusEnum.Taken.ToString();
        public Guid ReservationId { get; set; }
    }
}
