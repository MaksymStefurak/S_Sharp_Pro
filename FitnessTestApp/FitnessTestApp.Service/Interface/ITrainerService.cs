using FitnessTestApp.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Service.Interface
{
    public interface ITrainerService
    {
        Task<TrainerDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TrainerDto>> GetAllAsync();
        Task AddAsync(TrainerDto dto);
        Task UpdateAsync(TrainerDto dto);
        Task DeleteAsync(Guid id);
    }
}
