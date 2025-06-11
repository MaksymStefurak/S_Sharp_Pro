using CustomerTrainerService.Context;
using CustomerTrainerService.Entities;
using CustomerTrainerService.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerTrainerService.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly CustomerTrainerDbContext _context;

        public TrainerRepository(CustomerTrainerDbContext context)
        {
            _context = context;
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.ToListAsync();
        }

        public async Task AddAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trainer = await GetByIdAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
