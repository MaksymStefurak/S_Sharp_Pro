using FitnessTestApp.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Service.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CustomerDto dto);
        Task<bool> UpdateAsync(CustomerDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
