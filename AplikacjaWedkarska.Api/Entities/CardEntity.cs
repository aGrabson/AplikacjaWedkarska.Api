using System.ComponentModel.DataAnnotations;

namespace AplikacjaWedkarska.Api.Entities
{
    public class CardEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }
        public bool Mountain1Active { get; set; } = false;
        public bool Mountain2Active { get; set; } = false;
        public bool Lowland1Active { get; set; } = false;
        public string? OwnerName { get; set; }
        public string? OwnerSurname { get; set; }
        public string? Email { get; set; }
        public bool IsRegistered { get; set; } = false;
    }
}
