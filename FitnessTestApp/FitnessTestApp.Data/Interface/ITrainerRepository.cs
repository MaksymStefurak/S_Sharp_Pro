using FitnessTestApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Data.Interface
{
    public interface ITrainerRepository
    {
        Task<Trainer?> GetByIdAsync(Guid id);
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task AddAsync(Trainer trainer);
        Task UpdateAsync(Trainer trainer);
        Task DeleteAsync(Guid id);
    }
}
