using HRSystem.Application.IRepository;
using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Data;
using HRSystem.Infrastructure.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Repositories
{
    public class SalaryTiersRepository : ISalaryTiersRepository
    {
        private readonly HRSystemDBContext _context;
        private readonly ILogger<SalaryTiersRepository> _logger;

        public SalaryTiersRepository(HRSystemDBContext context, ILogger<SalaryTiersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddSalaryTierAsync(SalaryTiers salaryTier)
        {
            try
            {
                await _context.SalaryTiers.AddAsync(salaryTier);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in AddSalaryTierAsync method");
                return false;
            }
        }

        public async Task<bool> UpdateSalaryTierAsync(SalaryTiers salaryTier)
        {
            try
            {
                _context.SalaryTiers.Update(salaryTier);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in UpdateSalaryTierAsync method");
                return false;
            }
        }

        public async Task<IEnumerable<SalaryTiers>> GetAllSalaryTiersAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.SalaryTiers
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .OrderBy(s => s.BaseSalary)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in GetAllSalaryTiersAsync method");
                return Enumerable.Empty<SalaryTiers>();
            }
        }

        public async Task<SalaryTiers> GetSalaryTierByIdAsync(Guid id)
        {
            try
            {
                var item = await _context.SalaryTiers
                    .FirstOrDefaultAsync(s => s.SalaryTiersId == id);
                return item ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in GetSalaryTierByIdAsync method");
                return null;
            }
        }

        public async Task<IEnumerable<SalaryTiersReport>> GetReportSalaryTierAsync()
        {
            try
            {
                //edit this report/*
                //unit test
                // 
                //*/
                var salaryTiers = await _context.SalaryTiers
                    .Include(st => st.Employees)
                        .ThenInclude(e => e.Department)
                    .ToListAsync();

                var salaryTiersReports = salaryTiers.Select(st => new SalaryTiersReport
                {
                    TierName = st.TierName,
                    BaseSalary = st.BaseSalary,
                    Bonus = st.Bonus,
                    EmployeeCount = st.Employees.Count,
                    TotalSalary = st.Employees.Sum(e => st.BaseSalary + st.Bonus),

                    DepartmentTotalSalaries = st.Employees
                        .GroupBy(e => e.Department.DepartmentId)
                        .Select(g => new DepartmentSalaryInfo
                        {
                            DepartmentName = g.First().Department.DepartmetnName,
                            TotalDepartmentSalary = g.Sum(e => st.BaseSalary + st.Bonus)
                        })
                        .ToList()
                }).ToList();

                return salaryTiersReports;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in GetReportSalaryTierAsync method");
                return Enumerable.Empty<SalaryTiersReport>();
            }
        }
        public async Task<bool> DeleteSalaryTierAsync(Guid id)
        {
            try
            {
                var salaryTier = await GetSalaryTierByIdAsync(id);
                if (salaryTier == null) return false;

                _context.SalaryTiers.Remove(salaryTier);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in DeleteSalaryTierAsync method");
                return false;
            }
        }

        public async Task<bool> DeleteSalaryTierSoftAsync(Guid id)
        {
            try
            {
                var salaryTier = await GetSalaryTierByIdAsync(id);
                if (salaryTier == null) return false;

                salaryTier.IsActive = false;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in SalaryTiersRepository in DeleteSalaryTierSoftAsync method");
                return false;
            }
        }
    }

}

