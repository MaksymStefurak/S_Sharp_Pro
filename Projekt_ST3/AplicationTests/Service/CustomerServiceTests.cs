using CustomerTrainerService.DTOs;
using CustomerTrainerService.Entities;
using CustomerTrainerService.Interfaces;
using CustomerTrainerService.Repositories.Interface;
using CustomerTrainerService.Services.CustomerTrainerService.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace AplicationTests.Service
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _repoMock = new();
        private readonly Mock<IReservationClient> _reservationClientMock = new();

        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _service = new CustomerService(_repoMock.Object, _reservationClientMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCustomerWithReservations()
        {
            // Arrange
            int customerId = 1;
            var customer = new Customer { Id = customerId, FirstName = "Test", SurName = "User", Email = "test@example.com", Phone = "123456789" };
            var reservations = new List<ReservationDto> { new() { Id = 1, CustomerId = 1, TrainerId = 1, Date = DateTime.Now, Notes = "Test" } };

            _repoMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);
            _reservationClientMock.Setup(c => c.GetReservationsByCustomerIdAsync(customerId)).ReturnsAsync(reservations);

            // Act
            var result = await _service.GetByIdAsync(customerId);

            // Assert
            result.Should().NotBeNull();
            result!.Reservations.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenCustomerNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Customer?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new() { Id = 1, FirstName = "John", SurName = "Doe", Email = "john@example.com", Phone = "123" },
                new() { Id = 2, FirstName = "Jane", SurName = "Smith", Email = "jane@example.com", Phone = "456" }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task CreateAsync_CallsRepositoryAndReturnsId()
        {
            // Arrange
            var dto = new CustomerDto
            {
                FirstName = "Test",
                SurName = "User",
                Email = "test@example.com",
                Phone = "123"
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Customer>()))
                     .Callback<Customer>(c => c.Id = 42) 
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            result.Should().Be(42);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesCustomerAndReturnsTrue()
        {
            // Arrange
            var dto = new CustomerDto { Id = 1, FirstName = "Updated", SurName = "User", Email = "u@example.com", Phone = "999" };
            var customer = new Customer { Id = 1, FirstName = "Old", SurName = "User", Email = "old@example.com", Phone = "000" };

            _repoMock.Setup(r => r.GetByIdAsync(dto.Id)).ReturnsAsync(customer);
            _repoMock.Setup(r => r.UpdateAsync(customer)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            result.Should().BeTrue();
            _repoMock.Verify(r => r.UpdateAsync(customer), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenCustomerNotFound()
        {
            // Arrange
            var dto = new CustomerDto { Id = 1 };
            _repoMock.Setup(r => r.GetByIdAsync(dto.Id)).ReturnsAsync((Customer?)null);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_DeletesCustomerAndReturnsTrue()
        {
            // Arrange
            int id = 1;
            var customer = new Customer { Id = id };

            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);
            _repoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();
            _repoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenCustomerNotFound()
        {
            // Arrange
            int id = 1;
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Customer?)null);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            result.Should().BeFalse();
        }
    }
}
