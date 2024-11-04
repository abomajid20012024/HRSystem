using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Domain.Entities
{
    public class Employee
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "Please input first name for employee")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Please input last name for employee")]
        public required string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please input valid email for this property")]
        public required string Email { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
        public Guid SalaryTiersId { get; set; }
        public SalaryTiers? SalaryTiers { get; set; }
        public Employee()
        {
            EmployeeId = Guid.NewGuid();
        }
    }
}
