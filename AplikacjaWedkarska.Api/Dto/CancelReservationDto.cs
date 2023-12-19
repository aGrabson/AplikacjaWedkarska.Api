namespace AplikacjaWedkarska.Api.Dto
{
    public class CancelReservationDto
    {
        public bool IsCancelled { get; set; }
        public Guid ReservationId { get; set; }
    }
}
