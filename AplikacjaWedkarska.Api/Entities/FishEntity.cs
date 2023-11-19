using System.ComponentModel.DataAnnotations;

namespace AplikacjaWedkarska.Api.Entities
{
    public class FishEntity
    {
        [Required]
        public string? Species { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public DateTime CaughtDate { get; set; } = DateTime.Now;
        public DateTime? StartProtection { get; set; }
        public DateTime? EndProtection { get; set; }
    }
}
