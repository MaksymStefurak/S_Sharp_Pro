using FitnessTestApp.Data.Context;
using FitnessTestApp.Data.Interface;
using FitnessTestApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Data.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly AppDbContext _context;

        public TrainerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Trainer?> GetByIdAsync(Guid id) =>
            await _context.Trainers.FindAsync(id);

        public async Task<IEnumerable<Trainer>> GetAllAsync() =>
            await _context.Trainers.ToListAsync();

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

        public async Task DeleteAsync(Guid id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
