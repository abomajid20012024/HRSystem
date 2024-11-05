using HRSystem.Application.Services.IServices;
using System;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.SalaryTiers
{
    public class DeleteSalaryTiersCommand
    {
        private readonly ISalaryTiersService _salaryTiersService;

        public DeleteSalaryTiersCommand(ISalaryTiersService salaryTiersService)
        {
            _salaryTiersService = salaryTiersService;
        }

        // Hard delete (permanent deletion)
        public async Task<bool> Execute(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false; // Invalid ID
            }
            return await _salaryTiersService.DeleteSalaryTierAsync(id);
        }

        // Soft delete (set a flag to indicate deletion)
        public async Task<bool> ExecuteSoftDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false; // Invalid ID
            }
            return await _salaryTiersService.DeleteSalaryTierSoftAsync(id);
        }
    }
}
