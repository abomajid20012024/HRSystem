using HRSystem.Application.DTOs;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using HRSystem.Domain.Interfaces;
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
        public Task<bool> Excute(Domain.Entities.Department departmen)
        {
            return departmentService.AddDepartmentAsync(departmen);
        }
    }
}
