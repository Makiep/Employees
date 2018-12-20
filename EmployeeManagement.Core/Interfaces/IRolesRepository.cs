using EmployeeManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IRolesRepository
    {
        EmployeesRoles AddRole(EmployeesRoles empRole);

        EmployeesRoles GetRoleById(int roleId);

        IEnumerable<EmployeesRoles> GetAllRoles();

        EmployeesRoles EditRole(EmployeesRoles empRole);

        void DeleteRole(EmployeesRoles empRole);
    }
}
