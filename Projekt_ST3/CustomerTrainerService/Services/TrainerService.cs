using CustomerTrainerService.DTOs;
using CustomerTrainerService.Entities;
using CustomerTrainerService.Repositories.Interface;
using CustomerTrainerService.Services.Interface;

namespace CustomerTrainerService.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _repo;

        public TrainerService(ITrainerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TrainerDto>> GetAllAsync()
        {
            var trainers = await _repo.GetAllAsync();
            return trainers.Select(t => new TrainerDto
            {
                Id = t.Id,
                FirstName = t.FirstName,
                SurName = t.SurName,
                Email = t.Email,
                Phone = t.Phone,
                Specialty = t.Specialty
            });
        }

        public async Task<TrainerDto?> GetByIdAsync(int id)
        {
            var trainer = await _repo.GetByIdAsync(id);
            if (trainer == null) return null;

            return new TrainerDto
            {
                Id = trainer.Id,
                FirstName = trainer.FirstName,
                SurName = trainer.SurName,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialty = trainer.Specialty
            };
        }

        public async Task<int> CreateAsync(TrainerDto dto)
        {
            var trainer = new Trainer
            {
                FirstName = dto.FirstName,
                SurName = dto.SurName,
                Email = dto.Email,
                Phone = dto.Phone,
                Specialty = dto.Specialty
            };

            await _repo.AddAsync(trainer);
            return trainer.Id;
        }

        public async Task<bool> UpdateAsync(TrainerDto dto)
        {
            var trainer = await _repo.GetByIdAsync(dto.Id);
            if (trainer == null) return false;

            trainer.FirstName = dto.FirstName;
            trainer.SurName = dto.SurName;
            trainer.Email = dto.Email;
            trainer.Phone = dto.Phone;
            trainer.Specialty = dto.Specialty;

            await _repo.UpdateAsync(trainer);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trainer = await _repo.GetByIdAsync(id);
            if (trainer == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
