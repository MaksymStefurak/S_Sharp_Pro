using ReservationService.DTOs;

namespace ReservationService.Service.Interface
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<IEnumerable<ReservationDto>> GetByCustomerIdAsync(int customerId);
        Task CreateAsync(ReservationDto dto);
        Task<bool> UpdateAsync(ReservationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
