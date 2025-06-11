using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using CustomerTrainerService.Context;

namespace CustomerTrainerService.Services
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomerTrainerDbContext>
    {
        public CustomerTrainerDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CustomerTrainerDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new CustomerTrainerDbContext(optionsBuilder.Options);
        }
    }
}
