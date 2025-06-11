using CustomerTrainerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerTrainerService.Context
{
    public class CustomerTrainerDbContext : DbContext
    {
        public CustomerTrainerDbContext(DbContextOptions<CustomerTrainerDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Trainer> Trainers => Set<Trainer>();   
    }
}