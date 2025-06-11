using CustomerTrainerService.DTOs;
using CustomerTrainerService.Interfaces;

namespace CustomerTrainerService.Services
{
    public class ReservationClient : IReservationClient
    {
        private readonly HttpClient _httpClient;

        public ReservationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByCustomerIdAsync(int customerId)
        {
            var reservations = await _httpClient.GetFromJsonAsync<IEnumerable<ReservationDto>>($"api/reservations/customer/{customerId}");
            return reservations ?? Enumerable.Empty<ReservationDto>();
        }
    }
}
