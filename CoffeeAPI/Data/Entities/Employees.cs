using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Employees
    {
        public int EmployeeID { get; set; }
        [Required]
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool EmploymentType { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public int PositionID { get; set; }
        public DateOnly StartDate { get; set; }
        public decimal SalaryBase { get; set; }
        public bool Status { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public Positions Positions { get; set; }
        public List<EmployeeSchedules> EmployeeSchedules { get; set; }
        public List<Orders> Orders { get; set; }
        public List<Salaries> Salaries { get; set; }
        public List<User> Users { get; set; }
        public List<Timekeeping> Timekeepings { get; set; }
    }
}
