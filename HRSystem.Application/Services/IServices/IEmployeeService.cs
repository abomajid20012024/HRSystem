using HRSystem.Application.DTOs.Department;
using HRSystem.Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Services.IServices
{
    public interface IEmployeeService
    {
        Task<bool> AddEmployeeAsync(EmployeeCreateDto employee);
        Task<EmployeeDto> GetEmployeeByIdAsync(Guid id);
        Task<IEnumerable<EmployeeDto>> GetAllEmployiesAsync(int pageNumber, int pageSize);
        Task<bool> UpdateEmployeeAsync(EmployeeUpdateDto employeeUpdate);
        Task<bool> DeleteEmployeeSoftAsync(Guid idEmployee);
        Task<bool> DeleteEmployeeAsync(Guid idEmployee);
    }
}
