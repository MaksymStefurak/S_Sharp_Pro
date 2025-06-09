using FitnessTestApp.Data.Interface;
using FitnessTestApp.Domain.Entities;
using FitnessTestApp.Service.DTOs;
using FitnessTestApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Service.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _repo;

        public TrainerService(ITrainerRepository repo)
        {
            _repo = repo;
        }

        public async Task<TrainerDto?> GetByIdAsync(Guid id)
        {
            var t = await _repo.GetByIdAsync(id);
            return t == null ? null : Map(t);
        }

        public async Task<IEnumerable<TrainerDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(Map);
        }

        public async Task AddAsync(TrainerDto dto)
        {
            var t = new Trainer
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Specialty = dto.Specialty
            };
            await _repo.AddAsync(t);
        }

        public async Task UpdateAsync(TrainerDto dto)
        {
            var t = new Trainer
            {
                Id = dto.Id,
                FullName = dto.FullName,
                Specialty = dto.Specialty
            };
            await _repo.UpdateAsync(t);
        }

        public async Task DeleteAsync(Guid id) =>
            await _repo.DeleteAsync(id);

        private static TrainerDto Map(Trainer t) => new()
        {
            Id = t.Id,
            FullName = t.FullName,
            Specialty = t.Specialty
        };
    }
}
