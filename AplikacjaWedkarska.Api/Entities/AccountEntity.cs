using System.ComponentModel.DataAnnotations;

namespace AplikacjaWedkarska.Api.Entities
{
    public class AccountEntity : BaseDbItem
    {
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Surname { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Password { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateOfBirth { get; set; }

        public bool IsDeleted { get; set; }

        public int RoleID { get; set; }
        public virtual RoleEntity? Role { get; set; }

        public string CardID { get; set; }
        public virtual CardEntity? Card { get; set; }
    }
}
