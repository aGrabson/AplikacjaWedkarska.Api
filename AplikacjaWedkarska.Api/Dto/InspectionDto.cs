namespace AplikacjaWedkarska.Api.Dto
{
    public class InspectionDto
    {
        public List<string> Base64Photos { get; set; }
        public Guid ReservationId { get; set; }
        public string? Comment { get; set; }
    }
}
