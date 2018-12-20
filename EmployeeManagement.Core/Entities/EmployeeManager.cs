using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Core.Entities
{
    public class EmployeeManager
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
    }
}
