using System.ComponentModel.DataAnnotations;

namespace AplikacjaWedkarska.Api.Entities
{
    public class CardEntity : BaseDbItem
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }
        public bool Mountain1Active { get; set; } = false;
        public bool Mountain2Active { get; set; } = false;
        public bool Lowland1Active { get; set; } = false;
    }
}
