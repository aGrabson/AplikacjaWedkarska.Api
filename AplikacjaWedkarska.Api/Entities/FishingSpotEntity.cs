using System.ComponentModel.DataAnnotations;

namespace AplikacjaWedkarska.Api.Entities
{
    public class FishingSpotEntity
    {
        public FishingSpotEntity()
        {
            FishInSpot = new List<FishEntity>();
            FishingSpotLimits = new List<FishingSpotLimitEntity>();
        }

        [StringLength(50)]
        public string? Title { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Type { get; set; }

        public bool CatchAndRelease { get; set; }

        public virtual ICollection<FishEntity> FishInSpot { get; set; }
        public virtual ICollection<FishingSpotLimitEntity> FishingSpotLimits { get; set; }
    }
}
