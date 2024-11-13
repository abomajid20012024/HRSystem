using HRSystem.Domain.Entities;
using HRSystem.Shard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Application.IRepository
{
    public interface IEmployeeRepository
    {
        Task<(IEnumerable<Employee?>, PaginationMetaData?)> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<bool> AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(Guid id);
        Task<bool> DeleteEmployeeSoftAsync(Guid id);
        Task<List<Employee>> GetEmployeeByNameAsync(string name);
    }
}
