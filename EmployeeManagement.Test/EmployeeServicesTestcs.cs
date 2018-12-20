using Moq;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure;
using System;
using Xunit;
using EmployeeManagement.Core.Services;

namespace EmployeeManagement.Test
{
    /*
     Example Unit Tests
    */

    public class EmployeeServicesTestcs
    {
        private Mock<IEmployeesRepository> _employeeRepo;
        private EmployeeService _employeeService;

        public EmployeeServicesTestcs()
        {
            _employeeRepo = new Mock<IEmployeesRepository>();
            _employeeService = new EmployeeService(_employeeRepo.Object);
        }

        [Fact]
        public void GetEmployeeById_ReturnSuccess_Employee()
        {
            //Arrange
            Employees employee1 = new Employees() { Id = 1, Email = "makiep@gmail.com", Password = "password" };

            _employeeRepo.Setup(repo => repo.GetEmployeeById(employee1.Id))
                .Returns(new Employees(){ Id = 1, Email = "makiep@gmail.com", Password = "password" });

            //Act
            var result = _employeeService.GetEmployeeById(employee1.Id) as Employees;

            // Assert
            Assert.IsType<Employees>(result);
        }

        [Fact]
        public void GetEmployeeById_ReturnFailure_Employee()
        {
            //Arrange
            Employees employee1 = new Employees() { Id = 1, Email = "makiep@gmail.com", Password = "password" };

            _employeeRepo.Setup(repo => repo.GetEmployeeById(employee1.Id));

            //Act
            var result = _employeeService.GetEmployeeById(employee1.Id) as Employees;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetEmployeeByEmail_ReturnSuccess_Employee()
        {
            //Arrange
            Employees employee1 = new Employees() { Id = 1, Email = "makiep@gmail.com", Password = "password" };

            _employeeRepo.Setup(repo => repo.GetEmployeeByEmail(employee1.Email))
                .Returns(new Employees() { Id = 1, Email = "makiep@gmail.com", Password = "password" });

            //Act
            var result = _employeeService.GetEmployeeByEmail(employee1.Email) as Employees;

            // Assert
            Assert.IsType<Employees>(result);
        }

        [Fact]
        public void GetEmployeeByEmail_ReturnFailure_Employee()
        {
            //Arrange
            Employees employee1 = new Employees() { Id = 1, Email = "makiep@gmail.com", Password = "password" };

            _employeeRepo.Setup(repo => repo.GetEmployeeByEmail(employee1.Email));

            //Act
            var result = _employeeService.GetEmployeeByEmail(employee1.Email) as Employees;

            // Assert
            Assert.Null(result);
        }

    }


}
