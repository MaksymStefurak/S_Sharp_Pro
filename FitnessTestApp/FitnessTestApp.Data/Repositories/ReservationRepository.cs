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
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation?> GetByIdAsync(Guid id) =>
            await _context.Reservations.FindAsync(id);

        public async Task<IEnumerable<Reservation>> GetByCustomerIdAsync(Guid customerId) =>
            await _context.Reservations
                          .Where(r => r.CustomerId == customerId)
                          .ToListAsync();

        public async Task<IEnumerable<Reservation>> GetAllAsync() =>
            await _context.Reservations.ToListAsync();

        public async Task AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var res = await _context.Reservations.FindAsync(id);
            if (res != null)
            {
                _context.Reservations.Remove(res);
                await _context.SaveChangesAsync();
            }
        }
    }
}