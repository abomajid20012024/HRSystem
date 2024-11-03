using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Domain.Entities
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessage = "Please input name department")]
        public required string DepartmetnName { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        public int CountEmployee => Employees?.Count ?? 0;
        public Department()
        {
            DepartmentId = Guid.NewGuid();
        }
    }
}
