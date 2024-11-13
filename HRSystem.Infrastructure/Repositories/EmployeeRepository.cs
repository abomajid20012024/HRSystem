using HRSystem.Application.IRepository;
using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Data;
using HRSystem.Shard;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRSystemDBContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(HRSystemDBContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(IEnumerable<Employee?>, PaginationMetaData?)> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var countItem = await _context.Employees.CountAsync(e => e.IsActive);
                var paginationMetDate = new PaginationMetaData(countItem, pageSize, pageNumber);
                return (await _context.Employees
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .Where(e => e.IsActive)
                    .OrderBy(e => e.LastName)
                    .ToListAsync(), paginationMetDate);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in EmployeeRepository in GetAllEmployeesAsync method");
                return (Enumerable.Empty<Employee>(), null);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                var item = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeId == id);
                if (item is not null)
                {
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in EmployeeRepository in GetEmployeeByIdAsync method");
                return null;
            }
        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in EmployeeRepository in AddEmployeeAsync method");
                return false;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in EmployeeRepository in UpdateEmployeeAsync method");
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(id);
                if (employee == null) return false;

                _context.Employees.Remove(employee);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in EmployeeRepository in DeleteEmployeeAsync method");
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeSoftAsync(Guid id)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(id);
                if (employee == null) return false;

                employee.IsActive = false;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred in EmployeeRepository in DeleteEmployeeSoftAsync method");
                return false;
            }
        }

        public async Task<List<Employee>> GetEmployeeByNameAsync(string name)
        {
            try
            {
                var employies = await _context.Employees
                                             .Where(e => (e.FirstName + " " + e.LastName).Contains(name))
                                             .ToListAsync();
                return employies;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "error ocurs in getEMployeeByName");
                return new List<Employee>() { };
            }
        }
    }
}
