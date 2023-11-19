namespace AplikacjaWedkarska.Api.Entities
{
    public class ReservationEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime ReservationStart { get; set; }

        public DateTime ReservationEnd { get; set; }

        public Guid AccountId { get; set; }
        public virtual AccountEntity? Account { get; set; }

        public Guid FishingSpotId { get; set; }
        public virtual FishingSpotEntity? FishingSpot { get; set; }

    }
}
