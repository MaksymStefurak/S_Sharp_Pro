using CustomerTrainerService.DTOs;
using CustomerTrainerService.Entities;
using CustomerTrainerService.Repositories.Interface;
using CustomerTrainerService.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationTests.Service
{
    public class TrainerServiceTests
    {
        private readonly Mock<ITrainerRepository> _repoMock = new();
        private readonly TrainerService _service;

        public TrainerServiceTests()
        {
            _service = new TrainerService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTrainers()
        {
            // Arrange
            var trainers = new List<Trainer>
            {
                new() { Id = 1, FirstName = "Alice", SurName = "Smith", Email = "alice@example.com", Phone = "111", Specialty = "Yoga" },
                new() { Id = 2, FirstName = "Bob", SurName = "Brown", Email = "bob@example.com", Phone = "222", Specialty = "Strength" }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(trainers);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().FirstName.Should().Be("Alice");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTrainer_WhenExists()
        {
            // Arrange
            var trainer = new Trainer
            {
                Id = 1,
                FirstName = "Alice",
                SurName = "Smith",
                Email = "alice@example.com",
                Phone = "111",
                Specialty = "Yoga"
            };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trainer);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.FirstName.Should().Be("Alice");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Trainer?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_AddsTrainerAndReturnsId()
        {
            // Arrange
            var dto = new TrainerDto
            {
                FirstName = "Charlie",
                SurName = "Green",
                Email = "charlie@example.com",
                Phone = "333",
                Specialty = "Pilates"
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Trainer>()))
                     .Callback<Trainer>(t => t.Id = 10)
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            result.Should().Be(10);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Trainer>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTrainer_WhenExists()
        {
            // Arrange
            var existingTrainer = new Trainer
            {
                Id = 1,
                FirstName = "Old",
                SurName = "Name",
                Email = "old@example.com",
                Phone = "000",
                Specialty = "None"
            };

            var dto = new TrainerDto
            {
                Id = 1,
                FirstName = "Updated",
                SurName = "Trainer",
                Email = "new@example.com",
                Phone = "999",
                Specialty = "CrossFit"
            };

            _repoMock.Setup(r => r.GetByIdAsync(dto.Id)).ReturnsAsync(existingTrainer);
            _repoMock.Setup(r => r.UpdateAsync(existingTrainer)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            result.Should().BeTrue();
            existingTrainer.FirstName.Should().Be("Updated");
            _repoMock.Verify(r => r.UpdateAsync(existingTrainer), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenTrainerNotFound()
        {
            // Arrange
            var dto = new TrainerDto { Id = 99 };
            _repoMock.Setup(r => r.GetByIdAsync(dto.Id)).ReturnsAsync((Trainer?)null);

            // Act
            var result = await _service.UpdateAsync(dto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_DeletesTrainer_WhenExists()
        {
            // Arrange
            var trainer = new Trainer { Id = 1 };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trainer);
            _repoMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            result.Should().BeTrue();
            _repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenTrainerNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Trainer?)null);

            // Act
            var result = await _service.DeleteAsync(99);

            // Assert
            result.Should().BeFalse();
        }
    }
}
