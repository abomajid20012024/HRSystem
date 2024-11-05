using HRSystem.Application.DTOs.SalaryTiers;
using HRSystem.Application.Services.IServices;
using System;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.SalaryTiers
{
    public class AddSalaryTiersCommand
    {
        private readonly ISalaryTiersService _salaryTiersService;

        public AddSalaryTiersCommand(ISalaryTiersService salaryTiersService)
        {
            _salaryTiersService = salaryTiersService;
        }

        public Task<bool> Execute(SalaryTiersCreateDto salaryTier)
        {
            return _salaryTiersService.AddSalaryTierAsync(salaryTier);
        }
    }
}
