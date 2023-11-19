using System.ComponentModel.DataAnnotations;

namespace AplikacjaWedkarska.Api.Entities
{
    public abstract class BaseDbItem
    {
        [Key]
        public Guid Id { get; set; }
    }
}
