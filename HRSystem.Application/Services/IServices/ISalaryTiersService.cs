using HRSystem.Application.DTOs.SalaryTiers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Application.Services.IServices
{
    public interface ISalaryTiersService
    {
        Task<bool> AddSalaryTierAsync(SalaryTiersCreateDto salaryTier);
        Task<SalaryTiersDto> GetSalaryTierByIdAsync(Guid id);
        Task<IEnumerable<SalaryTiersDto>> GetAllSalaryTiersAsync(int pageNumber, int pageSize);
        Task<bool> UpdateSalaryTierAsync(SalaryTiersUpdateDto salaryTierUpdate);
        Task<bool> DeleteSalaryTierSoftAsync(Guid idSalaryTier);
        Task<bool> DeleteSalaryTierAsync(Guid idSalaryTier);
        Task<IEnumerable<SalaryTiersReportResponse>> GetRportSalaryTierAsync();
        Task<SalaryTiersReportResponse> GetRportSalaryTierAsync(Guid idEmployee);
    }
}
