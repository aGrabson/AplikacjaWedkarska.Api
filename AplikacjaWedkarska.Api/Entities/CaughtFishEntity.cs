namespace AplikacjaWedkarska.Api.Entities
{
    public class CaughtFishEntity : BaseDbItem
    {
        public Guid FishId { get; set; }
        public virtual FishEntity? Fish { get; set; }
        public double? Size { get; set; }
        public double? Weight { get; set; }
        public DateTime CatchDateTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = CaughtFishStatusEnum.Taken.ToString();

    }
}
