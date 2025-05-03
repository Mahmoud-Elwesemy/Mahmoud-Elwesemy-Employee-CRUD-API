using Employee.Controllers;
using Employee.Core.Application.Abstraction;
using Employee.Core.Application.Abstraction.Employee.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Emp.Task.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _mockEmployeeService = new Mock<IEmployeeService>();

            // Set up the mock service manager to return the mock employee service
            _mockServiceManager.Setup(sm => sm.EmployeeService).Returns(_mockEmployeeService.Object);

            // Initialize the controller with the mocked service manager
            _controller = new EmployeeController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GetAllEmployee_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<EmployeeDTOWithId>
            {
                new EmployeeDTOWithId { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Position = "Developer" },
                new EmployeeDTOWithId { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Position = "Manager" }
            };
            _mockEmployeeService.Setup(es => es.GetAllAsync(It.IsAny<Pramter>())).ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAllEmployee(new Pramter());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsType<List<EmployeeDTOWithId>>(okResult.Value);
            Assert.Equal(2, returnedEmployees.Count);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            _mockEmployeeService.Setup(es => es.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((EmployeeDTOWithId)null);

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsBadRequest_WhenEmployeeIsNull()
        {
            // Act
            var result = await _controller.CreateEmployee(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Employee data", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteEmployee_ReturnsNoContent_WhenEmployeeIsDeleted()
        {
            // Arrange
            _mockEmployeeService.Setup(es => es.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteEmployee(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
