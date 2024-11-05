using HRSystem.Application.DTOs;
using HRSystem.Application.DTOs.Department;
using HRSystem.Application.Services.IServices;
using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Department
{
    public class AddDepartmentCommand
    {
        private readonly IDepartmentService departmentService;

        public AddDepartmentCommand
            (
        IDepartmentService departmentService
            )
        {
            this.departmentService = departmentService;
        }
        public Task<bool> Excute(DepartmentCreateDto departmen)
        {
            return departmentService.AddDepartmentAsync(departmen);
        }
    }
}
