using Microsoft.EntityFrameworkCore;
using ReservationService.Entities;
using System.Collections.Generic;

namespace ReservationService.Context
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options) { }

        public DbSet<Reservation> Reservations => Set<Reservation>();
    }
}


