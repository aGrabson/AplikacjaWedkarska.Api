using System.ComponentModel.DataAnnotations;

namespace AplikacjaWedkarska.Api.Entities
{
    public class FishingSpotEntity : BaseDbItem
    {
        public FishingSpotEntity()
        {
            FishingSpotLimits = new List<FishingSpotLimitEntity>();
        }

        public string? Title { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Type { get; set; }

        public bool CatchAndRelease { get; set; }
        public double Size { get; set; }

        public virtual ICollection<FishingSpotLimitEntity> FishingSpotLimits { get; set; }
    }
}
