namespace CustomerTrainerService.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public IEnumerable<ReservationDto>? Reservations { get; set; }
    }
}
