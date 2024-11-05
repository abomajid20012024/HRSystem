using HRSystem.Application.DTOs.Department;
using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Services.IServices
{
    public interface IDepartmentService
    {
        Task<bool> AddDepartmentAsync(DepartmentCreateDto department);
        Task<DepartmentDto> GetDepartmentByIdAsync(Guid id);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(int pageNumber, int pageSize);
        Task<bool> UpdateDepartment(DepartmentUpdateDto department);
        Task<bool> DeleteDepartmentSoft(Guid idDepartment);
        Task<bool> DeleteDepartment(Guid idDepartment);
    }
}
