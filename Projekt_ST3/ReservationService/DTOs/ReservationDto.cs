namespace ReservationService.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TrainerId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
