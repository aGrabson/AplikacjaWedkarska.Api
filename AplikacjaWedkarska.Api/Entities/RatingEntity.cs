namespace AplikacjaWedkarska.Api.Entities
{
    public class RatingEntity : BaseDbItem
    {

        public Guid AccountId { get; set; }
        public virtual AccountEntity? Account { get; set; }

        public Guid FishingSpotId { get; set; }
        public virtual FishingSpotEntity? FishingSpot { get; set; }
        public int Rating { get; set; }
    }
}