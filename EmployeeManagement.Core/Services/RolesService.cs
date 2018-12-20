using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Core.Services
{
    public class RolesService : IRolesService
    {
        private IRolesRepository _rolesRepo;

        public RolesService(IRolesRepository rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }

        public EmployeesRoles AddRole(EmployeesRoles empRole)
        {
           return _rolesRepo.AddRole(empRole);
        }

        public void DeleteRole(EmployeesRoles empRole)
        {
            _rolesRepo.DeleteRole(empRole);
        }

        public EmployeesRoles EditRole(EmployeesRoles empRole)
        {
            return _rolesRepo.EditRole(empRole);
        }

        public IEnumerable<EmployeesRoles> GetAllRoles()
        {
            return _rolesRepo.GetAllRoles();
        }

        public EmployeesRoles GetRoleById(int roleId)
        {
            return _rolesRepo.GetRoleById(roleId);
        }
    }
}
