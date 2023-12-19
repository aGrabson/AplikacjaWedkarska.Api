namespace AplikacjaWedkarska.Api.Entities
{
    public class ReservationEntity : BaseDbItem
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime ReservationStart { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsCancelled { get; set; } = false;

        public Guid AccountId { get; set; }
        public virtual AccountEntity? Account { get; set; }

        public Guid FishingSpotId { get; set; }
        public virtual FishingSpotEntity? FishingSpot { get; set; }
    }
}
