namespace AplikacjaWedkarska.Api.Entities
{
    public class FishEntity : BaseDbItem
    {
        public string? Species { get; set; }
        public int? MinimumSize { get; set; }
        public int? MaximumSize { get; set; }
        public DateTime? ProtectionPeriodStart { get; set; }
        public DateTime? ProtectionPeriodEnd { get; set; }
        public int? DailyLimit { get; set; }
        public bool UnableToTake { get; set; } = false;
    }
}
