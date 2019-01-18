using EmployeeManagement.Core.Entities;
using System.Collections.Generic;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IEmployeesService
    {
        void SaveEmployees(Employees employee);

        void DeleteEmployee(int Id);

        Employees GetEmployeeById(int empId);

        EmployeesRoles GetRoleByEmployeeId(int empId);

        Employees GetEmployeeByEmail(string email);

        EmployeeManager GetEmployeeManagerByEmployeeId(int employeeId);

        bool DoesEmployeeExist(Employees employee);

        IEnumerable<Employees> GetAllEmployees();

        IEnumerable<Employees> GetManagerOptions(int empId);

        IEnumerable<Employees> GetEmployeeWithRelationships(int empId);

    }
}
