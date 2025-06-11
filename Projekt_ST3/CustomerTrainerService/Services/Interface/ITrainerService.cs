using CustomerTrainerService.DTOs;

namespace CustomerTrainerService.Services.Interface
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDto>> GetAllAsync();
        Task<TrainerDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(TrainerDto dto);
        Task<bool> UpdateAsync(TrainerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
