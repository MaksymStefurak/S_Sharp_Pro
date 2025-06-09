using FitnessTestApp.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Service.Interface
{
    public interface IReservationService
    {
        Task<ReservationDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<IEnumerable<ReservationDto>> GetByCustomerIdAsync(Guid customerId);
        Task CreateAsync(ReservationDto dto);
        Task UpdateAsync(ReservationDto dto);
        Task DeleteAsync(Guid id);
    }
}
