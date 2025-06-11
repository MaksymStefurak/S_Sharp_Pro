using CustomerTrainerService.Entities;

namespace CustomerTrainerService.Repositories.Interface
{
    public interface ITrainerRepository
    {
        Task<Trainer?> GetByIdAsync(int id);
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task AddAsync(Trainer trainer);
        Task UpdateAsync(Trainer trainer);
        Task DeleteAsync(int id);
    }
}
