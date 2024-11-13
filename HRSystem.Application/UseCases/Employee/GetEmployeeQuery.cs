using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.Services.IServices;
using HRSystem.Shard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Employee
{
    public class GetEmployeeQuery
    {
        private readonly IEmployeeService _employeeService;

        public GetEmployeeQuery(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Method to get a single employee by ID
        public Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId)
        {
            return _employeeService.GetEmployeeByIdAsync(employeeId);
        }

        // Method to get a paginated list of employees
        public Task<(IEnumerable<EmployeeDto>, PaginationMetaData)> GetEmployeesAsync(int pageNumber, int pageSize)
        {
            return _employeeService.GetAllEmployiesAsync(pageNumber, pageSize);
        }
    }
}
