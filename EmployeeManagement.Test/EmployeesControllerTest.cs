using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Moq;
using EmployeeManagement.API;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Test
{
    /*
     Example Unit Tests
    */

    public class EmployeesControllerTest
    {
        private Mock<IRepository<Employees>> _empRepo;
        private EmployeesController controller;

        private Mock<IEmployeesService> _employeesService;
        private Mock<IRolesService> _employeesRolesService;
        private Mock<IConfiguration> _config;

        public EmployeesControllerTest()
        {
            _employeesService = new Mock<IEmployeesService>();
            _employeesRolesService = new Mock<IRolesService>();
            _config = new Mock<IConfiguration>();

            controller = new EmployeesController(_employeesService.Object, _employeesRolesService.Object, _config.Object);
        }


        [Fact]
        public void UserLogin_ReturnsSuccess_CorrectCredentials()
        {
            // Arrange
            var username = "makiep@gmail.com";
            var password = "password";

            _employeesService.Setup(service => service.GetEmployeeByEmail(username))
                .Returns(new Employees { Id = 1, Email = username, Password = password });
            
            _config.SetupGet(m => m["Tokens:Key"]).Returns("0123456789ABCDEF");
            _config.SetupGet(m => m["Tokens:Issuer"]).Returns("http://mycodecamp.io");

            // Act
            var result = controller.Login(new LoginModel()
            {
                Email = username,
                Password = password
            }
            ) as IActionResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UserLogin_ReturnsFailure_IncorrectCrentials()
        {
            // Arrange
            var username = "user@gmail.com";
            var password = "password";

            _employeesService.Setup(service => service.GetEmployeeByEmail(username))
                .Returns(new Employees { Id = 1, Email = "user@gmail.com", Password = "password1" });

            _config.SetupGet(m => m["Tokens:Key"]).Returns("0123456789ABCDEF");
            _config.SetupGet(m => m["Tokens:Issuer"]).Returns("http://mycodecamp.io");

            // Act
            var result = controller.Login(new LoginModel()
            {
                Email = username,
                Password = password
            }
           ) as IActionResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Signup_ReturnsSuccess_UserSignedUp()
        {
            // Arrange
            SignUpModel model = new SignUpModel();
            model.Email = "max@gmail.com";
            model.Password = "password";
            model.ConfirmPassword = "password";
            model.FirstName = "FirstName";
            model.LastName = "LastName";

            _employeesService.Setup(service => service.GetEmployeeByEmail(model.Email));
            
            var employee = new Employees()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                RoleId = model.RoleId,
                ManagerId = model.Manager
            };

            _employeesService.Setup(service => service.SaveEmployees(employee));
            
            // Act
            var result = controller.SignUp(model) as IActionResult;

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Signup_ReturnsFailure_UserExists()
        {
            // Arrange
            SignUpModel model = new SignUpModel();
            model.Email = "makiep@gmail.com";
            model.Password = "password";
            model.ConfirmPassword = "password";
            model.FirstName = "FirstName";
            model.LastName = "LastName";

            _employeesService.Setup(service => service.GetEmployeeByEmail(model.Email))
                .Returns(new Employees { Id = 1, Email = "makiep@gmail.com", Password = "password" });

            // Act
            var result = controller.SignUp(model) as IActionResult;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void EmployeeDetails_ReturnsSuccess_Employees()
        {
            //Arrange
            Employees employee1 = new Employees() { Id = 1, Email = "makiep@gmail.com", Password = "password" };
            Employees employee2 = new Employees() { Id = 2, Email = "max@gmail.com", Password = "password" };

            _employeesService.Setup(service => service.GetAllEmployees()).Returns(new[] {
                employee1,
                employee2
            });

            //Act
            var result = controller.EmployeeDetails() as IActionResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void EmployeesManagersHierarchy_ReturnsSuccess_Employees()
        {
            Employees employee1 = new Employees() { Id = 1, Email = "employee@gmail.com", Password = "password" };
            Employees manager1 = new Employees() { Id = 2, Email = "manager1@gmail.com", Password = "password" };
            Employees manager2 = new Employees() { Id = 3, Email = "manager2@gmail.com", Password = "password" };

            //Arrange
            _employeesService.Setup(service => service.GetAllEmployees()).Returns(new[]{
                employee1,
                manager1,
                manager2
                    });

            //Act
            var result = controller.EmployeesManagersHierarchy(employee1.Id) as IActionResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        //Other tests such as GenerateToken could not be generated due to time 
    }
}