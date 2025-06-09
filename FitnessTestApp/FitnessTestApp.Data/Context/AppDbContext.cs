using FitnessTestApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Data.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Membership> Memberships { get; set; } 
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
           .HasOne(c => c.Membership)
           .WithOne(m => m.Customer)
           .HasForeignKey<Membership>(m => m.CustomerId)
           .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Trainer)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TrainerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
