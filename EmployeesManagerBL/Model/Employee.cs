using System;

namespace EmployeesManagerBL.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressOfResidence { get; set; }
        public string Department { get; set; }
        public string AboutMe { get; set; }
    }
}