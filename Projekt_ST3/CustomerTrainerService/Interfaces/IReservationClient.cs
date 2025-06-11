using CustomerTrainerService.DTOs;

namespace CustomerTrainerService.Interfaces
{
    public interface IReservationClient
    {
        Task<IEnumerable<ReservationDto>> GetReservationsByCustomerIdAsync(int customerId);
    }
}
