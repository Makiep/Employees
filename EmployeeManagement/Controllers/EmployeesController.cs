using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.API.Models;
using EmployeeManagement.Core.Interfaces;

namespace EmployeeManagement.API
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private IEmployeesService _employeesService;
        private IRolesService _employeesRolesService;
        private IConfiguration _config;

        public EmployeesController(IEmployeesService employeesService,
                                    IRolesService employeesRolesService,
                                    IConfiguration config)
        {
            _employeesService = employeesService;
            _employeesRolesService = employeesRolesService;
            _config = config;
        }

        //[HttpGet("")]
        //public IActionResult Employees()
        //{
        //    var employees = _employeesService.GetAllEmployees();
        //    return Ok(employees);
        //}

        [HttpGet("EmployeeDetails")]
        public IActionResult EmployeeDetails()
        {
            var employees = _employeesService.GetAllEmployees().ToList();

            var newEmployess = from empl in _employeesService.GetAllEmployees().ToList()
                               select new EmployeeModel
                               {
                                   Id = empl.Id,
                                   FirstName = empl.FirstName,
                                   LastName = empl.LastName,
                                   Email = empl.Email,
                                   Password = empl.Password,
                                   RoleId = empl.RoleId,
                                   ManagerId = empl.ManagerId,
                                   RoleName = _employeesService.GetRoleByEmployeeId(empl.RoleId).RoleName,
                                   ManagerName = empl.ManagerId > 0 ? _employeesService.GetEmployeeById(empl.ManagerId).FirstName : "None"
                               };

            return Ok(newEmployess);
        }

        [HttpGet("employeesmanagershierarchy")]
        private IActionResult EmployeesManagersHierarchy(int id)
        {
            var employees = _employeesService.GetEmployeeWithRelationships(id);
            return Ok(employees);
        }

        [HttpGet("getmanageroptions")]
        public IActionResult GetManagerOptions(int id)
        {
            if (id <= 0)
                return Ok();

            var employees = _employeesService.GetManagerOptions(id);
            return Ok(employees);
        }

        [HttpGet("Roles")]
        public IActionResult Roles()
        {
            var roles = _employeesRolesService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("employee")]
        public IActionResult GetEmployee(string email)
        {
            var employee = _employeesService.GetEmployeeByEmail(email);
            return Ok(employee);
        }

        [HttpGet("employeebyid")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeesService.GetEmployeeById(id);
            return Ok(employee);
        }

        [HttpPost("SaveEmployees")]
        public IActionResult SaveEmployees([FromBody] EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employees()
            {
                Email = employeeModel.Email,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Password = employeeModel.Password,
                RoleId = employeeModel.RoleId,
                ManagerId = employeeModel.ManagerId
            };

            _employeesService.SaveEmployees(employee);

            return Ok();
        }

        [HttpPost("deleteemployeee")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeee = _employeesService;
            _employeesService.DeleteEmployee(id);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _employeesService.GetEmployeeByEmail(model.Email);

            if(employee == null)
                return BadRequest("Email address does not exist");

            if (employee.Password != model.Password)
                return BadRequest("Wrong password");

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Email, employee.Email),
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
              _config["Tokens:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [AllowAnonymous]
        [Route("SignUp")]
        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors)
                    .Select(modelError => modelError.ErrorMessage).ToList());

            var existingEmployee = _employeesService.GetEmployeeByEmail(model.Email);

            if (existingEmployee != null)
            {
                ModelState.AddModelError("", "Employee already exists!");
                return BadRequest();
            }

            var employee = new Employees()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                RoleId = model.RoleId,
                ManagerId = model.Manager
            };

            _employeesService.SaveEmployees(employee);
      
            return Ok();
        }

        private string GenerateToken(string employeename, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(_config["Tokens:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                        new Claim(ClaimTypes.Name, employeename)
                    }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
