using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.CreateDto
{
    public class DepartmentCreateDto
    {
        [Required(ErrorMessage = "Please input Department name for create a new department")]
        public required string DepartmetnName { get; set; }
    }
}
