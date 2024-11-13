using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Employee
{
    public class GetEmployeeByNameCommand
    {
        private readonly IEmployeeService employeeService;

        public GetEmployeeByNameCommand
            (
            IEmployeeService employeeService
            )
        {
            this.employeeService = employeeService;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string name)
        {
            try
            {
                return await employeeService.GetAllEmployiesByNameAsync(name);
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<EmployeeDto>();
            }
        }
    }
}