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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;

        public ReservationService(IReservationRepository repo)
        {
            _repo = repo;
        }

        public async Task<ReservationDto?> GetByIdAsync(Guid id)
        {
            var r = await _repo.GetByIdAsync(id);
            return r == null ? null : Map(r);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(Map);
        }

        public async Task<IEnumerable<ReservationDto>> GetByCustomerIdAsync(Guid customerId)
        {
            var list = await _repo.GetByCustomerIdAsync(customerId);
            return list.Select(Map);
        }

        public async Task CreateAsync(ReservationDto dto)
        {
            var r = new Reservation
            {
                Id = Guid.NewGuid(),
                Date = dto.Date,
                Activity = dto.Activity,
                CustomerId = dto.CustomerId,
                TrainerId = dto.TrainerId
            };
            await _repo.AddAsync(r);
        }

        public async Task UpdateAsync(ReservationDto dto)
        {
            var r = new Reservation
            {
                Id = dto.Id,
                Date = dto.Date,
                Activity = dto.Activity,
                CustomerId = dto.CustomerId,
                TrainerId = dto.TrainerId
            };
            await _repo.UpdateAsync(r);
        }

        public async Task DeleteAsync(Guid id) =>
            await _repo.DeleteAsync(id);

        private static ReservationDto Map(Reservation r) => new()
        {
            Id = r.Id,
            Date = r.Date,
            Activity = r.Activity,
            CustomerId = r.CustomerId,
            TrainerId = r.TrainerId
        };
    }
}
