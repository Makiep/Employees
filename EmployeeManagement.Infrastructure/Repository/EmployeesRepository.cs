using EmployeeManagement.Core;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.Infrastructure.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private IRepository<Employees> _employeesRepo;
        private IRepository<EmployeeManager> _employeeManagerRepo;
        private IRolesRepository _employeesRolesRepo;

        public EmployeesRepository(IRepository<Employees> employeesRepo,
                                    IRepository<EmployeeManager> employeeManagerRepo,
                                    IRolesRepository employeesRolesRepo)
        {
            _employeesRepo = employeesRepo;
            _employeesRolesRepo = employeesRolesRepo;
            _employeeManagerRepo = employeeManagerRepo;
        }

        public IEnumerable<Employees> EmployeeDetails()
        {
            var employee = from emp in _employeesRepo.GetAll()
                           join empMan in _employeeManagerRepo.GetAll()
                            on emp.Id equals empMan.EmployeeId
                           select new Employees
                           {
                               Id = emp.Id,
                               FirstName = emp.FirstName,
                               LastName = emp.LastName,
                               RoleId = emp.RoleId,
                               ManagerId = emp.ManagerId
                           };

            return employee;
        }

        public IEnumerable<Employees> GetAllEmployees()
        {
            return _employeesRepo.GetAll().ToList();
        }

        public Employees GetEmployeeById(int empId)
        {
            if (empId > 0)
            {
                var employee = _employeesRepo.GetAll().FirstOrDefault(x => x.Id == empId);
                if (employee != null)
                {
                    employee.ManagerId = GetManagerId(employee.Id);
                    return employee;
                }
            }

            return null;
        }

        public Employees GetEmployeeByEmail(string emailAdd)
        {
            var employee = _employeesRepo.GetAll().FirstOrDefault(x => x.Email == emailAdd);
            employee.ManagerId = GetManagerId(employee.Id);

            return employee;
        }

        public void DeleteEmployee(int Id)
        {
            var employee = GetEmployeeById(Id);
            _employeesRepo.Delete(employee);

            //Delete Manager relationships for deleted employee
            EmployeeManager employeeManager = _employeeManagerRepo.GetAll().FirstOrDefault(x => x.EmployeeId == Id);
            _employeeManagerRepo.Delete(employeeManager);

            BulkResetEmployeesManager(_employeesRepo.GetAll().Where(x => x.ManagerId == Id).ToList());
        }

        public EmployeesRoles GetRoleByEmployeeId(int empId)
        {
            Employees employee = GetEmployeeById(empId);
            return _employeesRolesRepo.GetRoleById(employee.Id);
        }

        public void SaveEmployees(Employees employee)
        {
            var employeeToEdit = _employeesRepo.GetAll().FirstOrDefault(x => x.Email == employee.Email);
            if (employeeToEdit != null)
            {
                employeeToEdit.FirstName = employee.FirstName;
                employeeToEdit.LastName = employee.LastName;
                employeeToEdit.Email = employee.Email;
                employeeToEdit.RoleId = employee.RoleId;

                _employeesRepo.Update(employeeToEdit);

                //update employee-manager relationship table
                EmployeeManager employeeManager = _employeeManagerRepo.GetAll().FirstOrDefault(x => x.EmployeeId == employeeToEdit.Id);
                employeeManager.ManagerId = employee.ManagerId;
                _employeeManagerRepo.Update(employeeManager);
            }
            else
            {
                employee = _employeesRepo.InsertWithReturn(employee);

                //Add to employee-manager relationship table
                EmployeeManager employeeManager = new EmployeeManager { EmployeeId = employee.Id, ManagerId = employee.ManagerId };
                _employeeManagerRepo.Insert(employeeManager);
            }
        }

        public bool DoesEmployeeExist(Employees employee)
        {
            var employeeCopy = _employeesRepo.GetAll().FirstOrDefault(x => x.Email == employee.Email);

            return employeeCopy != null ? true : false;
        }

        public IEnumerable<Employees> GetManagerOptions(int empId)
        {
            List<Employees> employeeManagerOptions = new List<Employees>();
            var employee = GetEmployeeById(empId);
            var empManagers = _employeesRepo.GetAll().Where(x => x.RoleId > employee.RoleId).ToList();

            return empManagers;
        }

        public IEnumerable<Employees> GetEmployeeWithRelationships(int empId)
        {
            List<Employees> employeesHerachy = new List<Employees>();
            var employee = GetEmployeeById(empId);
            employeesHerachy.Add(employee);
            return GetEmployeeManager(employee, employeesHerachy);
        }

        private List<Employees> GetEmployeeManager(Employees employee, List<Employees> employeesList)
        {
            var empManager = _employeeManagerRepo.GetAll().FirstOrDefault(x => x.EmployeeId == employee.Id);

            if (empManager.ManagerId > 0)
            {
                var currentManager = GetEmployeeById(empManager.ManagerId);
                employeesList.Add(currentManager);
                employeesList = GetEmployeeManager(currentManager, employeesList);
            }

            return employeesList;
        }

        public EmployeeManager GetEmployeeManagerByEmployeeId(int employeeId)
        {
            var empMan = _employeeManagerRepo.GetAll().FirstOrDefault(x => x.EmployeeId == employeeId);
            return empMan;
        }

        private int GetManagerId(int employeeId)
        {
            var empMan = _employeeManagerRepo.GetAll().FirstOrDefault(x => x.EmployeeId == employeeId);
            return empMan != null ? empMan.ManagerId : 0;
        }

        private void BulkResetEmployeesManager(IEnumerable<Employees> employeesList)
        {
            foreach (Employees employee in employeesList)
            {
                employee.ManagerId = 0;
                _employeesRepo.Update(employee);
            }
        }
    }
}
