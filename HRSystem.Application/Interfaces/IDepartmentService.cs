using HRSystem.Application.DTOs;
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
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsByIdAsync(int id);
        Task<bool> UpdateDepartment(Department department);
        Task<bool> DeleteDepartmentSoft(Guid idDepartment);
        Task<bool> DeleteDepartment(Guid idDepartment);
    }
}
