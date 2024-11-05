using HRSystem.Application.DTOs.SalaryTiers;
using HRSystem.Application.Services.IServices;

namespace HRSystem.Application.UseCases.SalaryTiers
{
    public class GetSalaryTiersQuery
    {
        private readonly ISalaryTiersService _salaryTiersService;

        public GetSalaryTiersQuery(ISalaryTiersService salaryTiersService)
        {
            _salaryTiersService = salaryTiersService;
        }

        // Method to get a single salary tier by ID
        public Task<SalaryTiersDto> GetSalaryTierByIdAsync(Guid salaryTierId)
        {
            return _salaryTiersService.GetSalaryTierByIdAsync(salaryTierId);
        }

        // Method to get a paginated list of salary tiers
        public Task<IEnumerable<SalaryTiersDto>> GetSalaryTiersAsync(int pageNumber, int pageSize)
        {
            return _salaryTiersService.GetAllSalaryTiersAsync(pageNumber, pageSize);
        }
    }
}
