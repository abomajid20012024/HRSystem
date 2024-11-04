using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.UpdateDto
{
    public class DepartmentUpdateDto
    {
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessage = "Please input new name department for update")]
        public required string DepartmetnName { get; set; }
    }
}
