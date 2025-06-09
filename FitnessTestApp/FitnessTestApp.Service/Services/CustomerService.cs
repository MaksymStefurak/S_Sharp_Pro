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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email
            });
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        public async Task<Guid> CreateAsync(CustomerDto dto)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email
            };
            await _repo.AddAsync(customer);
            return customer.Id;
        }

        public async Task<bool> UpdateAsync(CustomerDto dto)
        {
            var customer = await _repo.GetByIdAsync(dto.Id);
            if (customer == null) return false;

            customer.Name = dto.Name;
            customer.Email = dto.Email;

            await _repo.UpdateAsync(customer);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return false;

            await _repo.DeleteAsync(customer.Id);
            return true;
        }
    }
}
