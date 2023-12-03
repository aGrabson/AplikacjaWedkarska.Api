namespace AplikacjaWedkarska.Api.Entities
{
    public class InspectionEntity : BaseDbItem
    {
        public DateTime DateOfInspection { get; set; } = DateTime.Now;
        public string? Comment { get; set; }
        public Guid ControlledUserId { get; set; }
        public virtual AccountEntity? ControlledUser { get; set; }
        public Guid ControllerId { get; set; }
        public virtual AccountEntity? Controller { get; set; }
        public Guid FishingSpotId { get; set; }
        public virtual FishingSpotEntity? FishingSpot { get; set; }
        public string? Base64Image { get; set; }
        
    }
}
