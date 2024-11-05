using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.Services.IServices;
using System;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Employee
{
    public class UpdateEmployeeCommand
    {
        private readonly IEmployeeService _employeeService;

        public UpdateEmployeeCommand(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Execute method to update an employee
        public Task<bool> Execute(EmployeeUpdateDto employee)
        {
            return _employeeService.UpdateEmployeeAsync(employee);
        }
    }
}
