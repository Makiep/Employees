using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Core.Services.Response
{
    public class EmployeesDetails
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }

        public int ManagerName { get; set; }
    }
}
