namespace AplikacjaWedkarska.Api.Entities
{
    public class FishingSpotLimitEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Guid FishingSpotId { get; set; }
        public virtual FishingSpotEntity? FishingSpot { get; set; }

        public string Species { get; set; }

        public int DailyLimit { get; set; }

    }
}
