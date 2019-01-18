using System;
using System.Collections.Generic;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;

namespace EmployeeManagement.Core.Services
{
    public class EmployeeService : IEmployeesService
    {
        private IEmployeesRepository _employeeRepo;

        public EmployeeService(IEmployeesRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public IEnumerable<Employees> EmployeeDetails()
        {
           return _employeeRepo.GetAllEmployees();
        }

        public IEnumerable<Employees> GetAllEmployees()
        {
            return _employeeRepo.GetAllEmployees();
        }

        public Employees GetEmployeeById(int empId)
        {
            return _employeeRepo.GetEmployeeById(empId);
        }

        public Employees GetEmployeeByEmail(string email)
        {
            return _employeeRepo.GetEmployeeByEmail(email);
        }

        public EmployeesRoles GetRoleByEmployeeId(int empId)
        {
            return _employeeRepo.GetRoleByEmployeeId(empId);
        }

        public void SaveEmployees(Employees employee)
        {
            _employeeRepo.SaveEmployees(employee);
        }

        public bool DoesEmployeeExist(Employees employee)
        {
            return _employeeRepo.DoesEmployeeExist(employee);
        }

        public IEnumerable<Employees> GetEmployeeWithRelationships(int empId)
        {
            return _employeeRepo.GetEmployeeWithRelationships(empId);
        }

        public IEnumerable<Employees> GetManagerOptions(int empId)
        {
            return _employeeRepo.GetManagerOptions(empId);
        }

        public void DeleteEmployee(int Id)
        {
            _employeeRepo.DeleteEmployee(Id);
        }

        public EmployeeManager GetEmployeeManagerByEmployeeId(int employeeId)
        {
            return _employeeRepo.GetEmployeeManagerByEmployeeId(employeeId);
        }
    }
}
