using CustomerTrainerService.DTOs;

namespace CustomerTrainerService.Services.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CustomerDto dto);
        Task<bool> UpdateAsync(CustomerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
