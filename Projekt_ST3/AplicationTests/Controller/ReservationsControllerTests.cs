using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Controllers;
using ReservationService.DTOs;
using ReservationService.Service.Interface;

namespace AplicationTests.Controller
{
    public class ReservationsControllerTests
    {
        private readonly Mock<IReservationService> _serviceMock = new();
        private readonly ReservationsController _controller;

        public ReservationsControllerTests()
        {
            _controller = new ReservationsController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfReservations()
        {
            // Arrange
            var reservations = new List<ReservationDto>
            {
                new ReservationDto { Id = 1, CustomerId = 1, TrainerId = 2, Date = DateTime.Now, Notes = "Test 1" }
            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(reservations);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(reservations);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenFound()
        {
            var dto = new ReservationDto { Id = 1, CustomerId = 1, TrainerId = 2, Date = DateTime.Now, Notes = "Test" };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

            var result = await _controller.GetById(1);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be(dto);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((ReservationDto?)null);

            var result = await _controller.GetById(999);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetByCustomerId_ReturnsOk_WithReservations()
        {
            var list = new List<ReservationDto> { new() { Id = 1, CustomerId = 1, TrainerId = 2, Date = DateTime.Now } };
            _serviceMock.Setup(s => s.GetByCustomerIdAsync(1)).ReturnsAsync(list);

            var result = await _controller.GetByCustomerId(1);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var dto = new ReservationDto { Id = 1, CustomerId = 1, TrainerId = 2, Date = DateTime.Now };
            _serviceMock.Setup(s => s.CreateAsync(dto)).Returns(Task.CompletedTask);

            var result = await _controller.Create(dto);

            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.ActionName.Should().Be(nameof(_controller.GetByCustomerId));
            createdResult.Value.Should().Be(dto);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            var dto = new ReservationDto { Id = 1, CustomerId = 1, TrainerId = 2, Date = DateTime.Now };
            _serviceMock.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(true);

            var result = await _controller.Update(1, dto);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenFailed()
        {
            var dto = new ReservationDto { Id = 1, CustomerId = 1, TrainerId = 2, Date = DateTime.Now };
            _serviceMock.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(false);

            var result = await _controller.Update(1, dto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            _serviceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenNotFound()
        {
            _serviceMock.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

            var result = await _controller.Delete(99);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
