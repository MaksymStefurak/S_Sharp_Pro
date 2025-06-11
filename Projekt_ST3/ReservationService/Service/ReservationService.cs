using ReservationService.DTOs;
using ReservationService.Entities;
using ReservationService.Repositories.Interface;
using ReservationService.Service.Interface;

namespace ReservationService.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;

        public ReservationService(IReservationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(ToDto);

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            var res = await _repo.GetByIdAsync(id);
            return res == null ? null : ToDto(res);
        }

        public async Task<IEnumerable<ReservationDto>> GetByCustomerIdAsync(int customerId) =>
            (await _repo.GetByCustomerIdAsync(customerId)).Select(ToDto);

        public async Task CreateAsync(ReservationDto dto)
        {
            var entity = new Reservation
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                TrainerId = dto.TrainerId,
                Date = dto.Date,
                Notes = dto.Notes
            };
            await _repo.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(ReservationDto dto)
        {
            var entity = new Reservation
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                TrainerId = dto.TrainerId,
                Date = dto.Date,
                Notes = dto.Notes
            };
            return await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);

        private static ReservationDto ToDto(Reservation res) => new()
        {
            Id = res.Id,
            CustomerId = res.CustomerId,
            TrainerId = res.TrainerId,
            Date = res.Date,
            Notes = res.Notes
        };
    }
}
