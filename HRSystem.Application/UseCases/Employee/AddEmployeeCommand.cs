using HRSystem.Application.DTOs;
using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.Services.IServices;
using System;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Employee
{
    public class AddEmployeeCommand
    {
        private readonly IEmployeeService _employeeService;

        public AddEmployeeCommand(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public Task<bool> Execute(EmployeeCreateDto employee)
        {
            return _employeeService.AddEmployeeAsync(employee);
        }
    }
}
