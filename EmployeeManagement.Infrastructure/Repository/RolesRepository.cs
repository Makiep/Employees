
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;

namespace EmployeeManagement.Infrastructure.Repository
{
    public class RolesRepository: IRolesRepository
    {
        private IRepository<EmployeesRoles> _rolesRepo;

        public RolesRepository(IRepository<EmployeesRoles> employeesRolesRepo)
        {
            _rolesRepo = employeesRolesRepo;
        }

        public EmployeesRoles AddRole(EmployeesRoles empRole)
        {
            _rolesRepo.Insert(empRole);

            return empRole;
        }

        public void DeleteRole(EmployeesRoles empRole)
        {
            _rolesRepo.Delete(empRole);
        }

        public EmployeesRoles EditRole(EmployeesRoles empRole)
        {
            var roleToEdit = _rolesRepo.GetAll().FirstOrDefault(x => x.Id == empRole.Id);

            roleToEdit.RoleName = empRole.RoleName;

            _rolesRepo.Update(roleToEdit);

            return roleToEdit;
        }

        public IEnumerable<EmployeesRoles> GetAllRoles()
        {
            return _rolesRepo.GetAll();
        }

        public EmployeesRoles GetRoleById(int roleId)
        {
            return _rolesRepo.GetAll().FirstOrDefault(x => x.Id == roleId);
        }
    }
}
