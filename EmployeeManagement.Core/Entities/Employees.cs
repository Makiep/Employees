namespace EmployeeManagement.Core.Entities
{
    public class Employees
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId{ get; set; }
        public string Password { get; set; }

        public int ManagerId { get; set; }
    }
}
