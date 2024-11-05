using HRSystem.Application.DTOs.SalaryTiers;
using HRSystem.Application.Services.IServices;
using System;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.SalaryTiers
{
    public class UpdateSalaryTiersCommand
    {
        private readonly ISalaryTiersService _salaryTiersService;

        public UpdateSalaryTiersCommand(ISalaryTiersService salaryTiersService)
        {
            _salaryTiersService = salaryTiersService;
        }

        // Execute method to update a salary tier
        public Task<bool> Execute(SalaryTiersUpdateDto salaryTier)
        {
            return _salaryTiersService.UpdateSalaryTierAsync(salaryTier);
        }
    }
}
