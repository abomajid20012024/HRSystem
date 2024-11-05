using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRSystem.Application.DTOs.Department
{
    public class DepartmentDto
    {
        public Guid DepartmentId { get; set; }
        public required string DepartmetnName { get; set; }
    }
}
