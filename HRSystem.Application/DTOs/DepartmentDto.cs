using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs
{
    public class DepartmentDto
    {
        [Required(ErrorMessage = "Please input name department")]
        public required string DepartmetnName { get; set; }

    }
}
