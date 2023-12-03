namespace AplikacjaWedkarska.Api.Entities
{
    public class FishingSpotLimitEntity : BaseDbItem
    {
        public Guid FishId { get; set; }
        public virtual FishEntity Fish { get; set; }
        public int? DailyLimit { get; set; }
        public Guid FishingSpotId { get; set; }
        public virtual FishingSpotEntity? FishingSpot { get; set; }
    }
}
