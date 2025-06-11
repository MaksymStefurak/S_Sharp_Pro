using Microsoft.EntityFrameworkCore;
using ReservationService.Context;
using ReservationService.Entities;
using ReservationService.Repositories.Interface;

namespace ReservationService.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDbContext _context;

        public ReservationRepository(ReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync() =>
            await _context.Reservations.ToListAsync();

        public async Task<Reservation?> GetByIdAsync(int id) =>
            await _context.Reservations.FindAsync(id);

        public async Task<IEnumerable<Reservation>> GetByCustomerIdAsync(int customerId) =>
            await _context.Reservations.Where(r => r.CustomerId == customerId).ToListAsync();

        public async Task AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Reservation reservation)
        {
            var existing = await _context.Reservations.FindAsync(reservation.Id);
            if (existing == null) return false;

            existing.Date = reservation.Date;
            existing.Notes = reservation.Notes;
            existing.CustomerId = reservation.CustomerId;
            existing.TrainerId = reservation.TrainerId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
