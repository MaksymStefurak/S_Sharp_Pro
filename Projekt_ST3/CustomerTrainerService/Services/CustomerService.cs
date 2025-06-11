using CustomerTrainerService.DTOs;
using CustomerTrainerService.Interfaces;
using CustomerTrainerService.Repositories.Interface;
using CustomerTrainerService.Entities;
using CustomerTrainerService.Services.Interface;

namespace CustomerTrainerService.Services
{
    namespace CustomerTrainerService.Services
    {
        public class CustomerService : ICustomerService
        {
            private readonly ICustomerRepository _repo;
            private readonly IReservationClient _reservationClient;

            public CustomerService(ICustomerRepository repo, IReservationClient reservationClient)
            {
                _repo = repo;
                _reservationClient = reservationClient;
            }

            public async Task<IEnumerable<CustomerDto>> GetAllAsync()
            {
                var customers = await _repo.GetAllAsync();
                return customers.Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    SurName = c.SurName,
                    Email = c.Email,
                    Phone = c.Phone
                });
            }

            public async Task<CustomerDto?> GetByIdAsync(int id)
            {
                var customer = await _repo.GetByIdAsync(id);
                if (customer == null) return null;

                var reservations = await _reservationClient.GetReservationsByCustomerIdAsync(id);

                return new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    SurName = customer.SurName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Reservations = reservations
                };
            }

            public async Task<int> CreateAsync(CustomerDto dto)
            {
                var customer = new Customer
                {
                    FirstName = dto.FirstName,
                    SurName = dto.SurName,
                    Email = dto.Email,
                    Phone = dto.Phone
                };

                await _repo.AddAsync(customer);
                return customer.Id;
            }

            public async Task<bool> UpdateAsync(CustomerDto dto)
            {
                var customer = await _repo.GetByIdAsync(dto.Id);
                if (customer == null) return false;

                customer.FirstName = dto.FirstName;
                customer.SurName = dto.SurName;
                customer.Email = dto.Email;
                customer.Phone = dto.Phone;

                await _repo.UpdateAsync(customer);
                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var customer = await _repo.GetByIdAsync(id);
                if (customer == null) return false;

                await _repo.DeleteAsync(id);
                return true;
            }
        }
    }

}
