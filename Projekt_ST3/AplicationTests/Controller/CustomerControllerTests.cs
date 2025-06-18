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
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _serviceMock = new();
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _controller = new CustomerController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto>
            {
                new() { Id = 1, FirstName = "John", SurName = "Doe", Email = "john@example.com", Phone = "123" },
                new() { Id = 2, FirstName = "Jane", SurName = "Smith", Email = "jane@example.com", Phone = "456" }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(customers);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenCustomerExists()
        {
            // Arrange
            var customer = new CustomerDto { Id = 1, FirstName = "John", SurName = "Doe", Email = "john@example.com", Phone = "123" };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((CustomerDto?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            // Arrange
            var customer = new CustomerDto
            {
                Id = 1,
                FirstName = "New",
                SurName = "Customer",
                Email = "new@example.com",
                Phone = "999"
            };

            _serviceMock.Setup(s => s.CreateAsync(customer)).ReturnsAsync(customer.Id);

            // Act
            var result = await _controller.Create(customer);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.Value.Should().Be(customer);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var customer = new CustomerDto { Id = 1, FirstName = "Updated" };
            _serviceMock.Setup(s => s.UpdateAsync(customer)).ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, customer);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var customer = new CustomerDto { Id = 2 };

            // Act
            var result = await _controller.Update(1, customer);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customer = new CustomerDto { Id = 1 };
            _serviceMock.Setup(s => s.UpdateAsync(customer)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, customer);

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
        public async Task Delete_ReturnsNotFound_WhenCustomerDoesNotExist()
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
