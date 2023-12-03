namespace AplikacjaWedkarska.Api.Dto
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}