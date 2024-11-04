using HRSystem.Application.DTOs.ShowDto;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Department
{
    public class DeleteDepartmentCommand
    {
        private readonly IDepartmentService _departmentService;

        public DeleteDepartmentCommand
            (
            IDepartmentService departmentService
            )
        {
            this._departmentService = departmentService;
        }
        public Task<bool> Excute(Guid id)
        {
            return _departmentService.DeleteDepartment(id);
        }
        public Task<bool> ExcuteSoftDelete(Guid id)
        {
            return _departmentService.DeleteDepartmentSoft(id);
        }
    }
}
