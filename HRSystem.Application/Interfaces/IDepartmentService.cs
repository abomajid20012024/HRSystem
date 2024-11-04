using HRSystem.Application.DTOs.ShowDto;
using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> AddDepartmentAsync(Department department);
        Task<Department> GetDepartmentByIdAsync(Guid id);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(int pageNumber, int pageSize);
        Task<bool> UpdateDepartment(Department department);
        Task<bool> DeleteDepartmentSoft(Guid idDepartment);
        Task<bool> DeleteDepartment(Guid idDepartment);
    }
}
