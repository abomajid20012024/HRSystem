using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Repositories.IRepository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(int pageNumber, int pageSize);

        // Retrieve a single department by ID
        Task<Department> GetDepartmentByIdAsync(Guid id);

        // Add a new department
        Task<bool> AddDepartmentAsync(Department department);

        // Update an existing department
        Task<bool> UpdateDepartmentAsync(Department department);

        // Delete a department by ID
        Task<bool> DeleteDepartmentAsync(Guid id);
        Task<bool> DeleteDepartmentSoftAsync(Guid id);
    }
}
