﻿using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Repositories.IRepository
{
    public interface ISalaryTiersRepository
    {
        Task<IEnumerable<SalaryTiers>> GetAllSalaryTiersAsync(int pageNumber, int pageSize);
        Task<SalaryTiers> GetSalaryTierByIdAsync(Guid id);
        Task<IEnumerable<SalaryTiersReport>> GetReportSalaryTierAsync();
        Task<bool> AddSalaryTierAsync(SalaryTiers salaryTier);
        Task<bool> UpdateSalaryTierAsync(SalaryTiers salaryTier);
        Task<bool> DeleteSalaryTierAsync(Guid id);
        Task<bool> DeleteSalaryTierSoftAsync(Guid id); // Soft delete where applicable
    }
}
