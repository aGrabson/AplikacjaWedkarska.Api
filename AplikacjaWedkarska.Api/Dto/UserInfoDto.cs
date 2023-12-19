namespace AplikacjaWedkarska.Api.Dto
{
    public class UserInfoDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public Guid ReservationId { get; set; }
    }
}
