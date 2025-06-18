using CustomerTrainerService.Controllers;
using CustomerTrainerService.DTOs;
using CustomerTrainerService.Services.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationTests.Controller
{
    public class TrainerControllerTests
    {
        private readonly Mock<ITrainerService> _serviceMock = new();
        private readonly TrainerController _controller;

        public TrainerControllerTests()
        {
            _controller = new TrainerController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithTrainers()
        {
            // Arrange
            var trainers = new List<TrainerDto>
            {
                new() { Id = 1, FirstName = "Anna", SurName = "Nowak", Email = "anna@example.com", Phone = "111" },
                new() { Id = 2, FirstName = "Jan", SurName = "Kowalski", Email = "jan@example.com", Phone = "222" }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(trainers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(trainers);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenTrainerExists()
        {
            // Arrange
            var trainer = new TrainerDto { Id = 1, FirstName = "Anna" };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(trainer);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be(trainer);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenTrainerNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((TrainerDto?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new TrainerDto { Id = 1, FirstName = "Anna", SurName = "Nowak", Email = "anna@example.com", Phone = "123456789" };
            _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(1);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var created = result as CreatedAtActionResult;
            created.Should().NotBeNull();
            created!.ActionName.Should().Be(nameof(_controller.GetById));
            created.Value.Should().Be(dto);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var dto = new TrainerDto { Id = 1, FirstName = "Updated" };
            _serviceMock.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, dto);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var dto = new TrainerDto { Id = 2, FirstName = "Mismatch" };

            // Act
            var result = await _controller.Update(1, dto);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.Value.Should().Be("ID mismatch.");
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenTrainerNotFound()
        {
            // Arrange
            var dto = new TrainerDto { Id = 1 };
            _serviceMock.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, dto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenTrainerNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(99);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}