using CustomerTrainerService.DTOs;
using FluentAssertions;
using Moq;
using ReservationService.Entities;
using ReservationService.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationTests.Service
{
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _repoMock = new();
        private readonly ReservationService.Service.ReservationService _service;

        public ReservationServiceTests()
        {
            _service = new ReservationService.Service.ReservationService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllReservations()
        {
            // Arrange
            var reservations = new List<Reservation>
            {
                new() { Id = 1, CustomerId = 1, TrainerId = 2, Date = new DateTime(2025, 1, 1), Notes = "Test 1" },
                new() { Id = 2, CustomerId = 2, TrainerId = 3, Date = new DateTime(2025, 1, 2), Notes = "Test 2" }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(reservations);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().CustomerId.Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReservation_WhenExists()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = 1,
                CustomerId = 1,
                TrainerId = 2,
                Date = new DateTime(2025, 1, 1),
                Notes = "Test"
            };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(reservation);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Reservation?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByCustomerIdAsync_ReturnsReservations()
        {
            // Arrange
            int customerId = 1;
            var reservations = new List<Reservation>
            {
                new() { Id = 1, CustomerId = customerId, TrainerId = 2, Date = new DateTime(2025, 1, 1), Notes = "Note 1" }
            };

            _repoMock.Setup(r => r.GetByCustomerIdAsync(customerId)).ReturnsAsync(reservations);

            // Act
            var result = await _service.GetByCustomerIdAsync(customerId);

            // Assert
            result.Should().HaveCount(1);
            result.First().CustomerId.Should().Be(customerId);
        }

        [Fact]
        public async Task CreateAsync_CallsAddAsyncWithCorrectEntity()
        {
            // Arrange
            var date = new DateTime(2025, 1, 1);
            var dto = new ReservationService.DTOs.ReservationDto
            {
                Id = 1,
                CustomerId = 1,
                TrainerId = 2,
                Date = DateTime.Now,
                Notes = "Test"
            };

            // Act
            await _service.CreateAsync(dto);

            // Assert
            _repoMock.Verify(r => r.AddAsync(It.Is<Reservation>(
                r => r.Id == dto.Id &&
                     r.CustomerId == dto.CustomerId &&
                     r.TrainerId == dto.TrainerId &&
                     r.Date == dto.Date &&
                     r.Notes == dto.Notes)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            var dto = new ReservationService.DTOs.ReservationDto
            {
                Id = 1,
                CustomerId = 1,
                TrainerId = 2,
                Date = new DateTime(2025, 1, 1),
                Notes = "Updated"
            };

            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Reservation>())).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            result.Should().BeTrue();
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Reservation>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenRepositoryFails()
        {
            // Arrange
            var dto = new ReservationService.DTOs.ReservationDto
            {
                Id = 1,
                CustomerId = 1,
                TrainerId = 1,
                Date = new DateTime(2025, 1, 1),
                Notes = "Fail"
            };

            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Reservation>())).ReturnsAsync(false);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenDeleted()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            result.Should().BeTrue();
            _repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(99);

            // Assert
            result.Should().BeFalse();
        }
    }
}
