using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "Please input first name for employee")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Please input last name for employee")]
        public required string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please input valid email for this property")]
        public required string Email { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid SalaryTiersId { get; set; }

    }
}
