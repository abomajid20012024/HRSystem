using HRSystem.Application.IRepository;
using HRSystem.Application.Services.IServices;
using System;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Employee
{
    public class DeleteEmployeeCommand
    {
        private readonly IEmployeeService employeeService;

        public DeleteEmployeeCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // Hard delete (permanent deletion)
        public async Task<bool> Execute(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }
            await employeeService.DeleteEmployeeAsync(id);
            return true;
        }

        // Soft delete (set a flag to indicate deletion)
        public async Task<bool> ExecuteSoftDelete(Guid id)
        {
            var employee = await employeeService.DeleteEmployeeSoftAsync(id);
            if (!employee)
            {
                return false;
            }
            return true;
        }
    }
}
