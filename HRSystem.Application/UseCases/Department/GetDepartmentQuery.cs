using HRSystem.Application.DTOs.ShowDto;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Department
{
    public class GetDepartmentQuery
    {
        private readonly IDepartmentService _departmentService;

        public GetDepartmentQuery
            (
             IDepartmentService departmentService
            )
        {
            this._departmentService = departmentService;
        }
        public Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
        {
            return _departmentService.GetAllDepartmentsAsync(pageNumber, pageSize);
        }

        // Optional: Method to get a single department by ID
        public Task<Domain.Entities.Department> GetDepartmentByIdAsync(Guid departmentId)
        {
            return _departmentService.GetDepartmentByIdAsync(departmentId);
        }
    }
}
