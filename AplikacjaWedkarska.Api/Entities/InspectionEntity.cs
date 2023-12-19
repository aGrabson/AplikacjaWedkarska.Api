namespace AplikacjaWedkarska.Api.Entities
{
    public class InspectionEntity : BaseDbItem
    {
        public DateTime DateOfInspection { get; set; } = DateTime.Now;
        public string? Comment { get; set; }
        public Guid ControllerId { get; set; }
        public virtual AccountEntity? Controller { get; set; }
        public Guid ReservationId { get; set; }
        public virtual ReservationEntity? Reservation { get; set; }
        
    }
}
