using HRSystem.Application.DTOs;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using HRSystem.Domain.Interfaces;
using HRSystem.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentService
    {
        private readonly HRSystemDBContext _context;
        private readonly ILogger<DepartmentRepository> logger;

        public DepartmentRepository
            (
            HRSystemDBContext context,
            ILogger<DepartmentRepository> logger

            )
        {
            this._context = context;
            this.logger = logger;
        }
        public async Task<bool> AddDepartmentAsync(Department department)
        {
            try
            {
                if (department is not null)
                {
                    await _context.Departments.AddAsync(department);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "error ocurs in department repository in function AddDepartmentAsync");
                return false;
            }
        }

        public Task<bool> DeleteDepartment(Guid idDepartment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDepartmentSoft(Guid idDepartment)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DepartmentDto>> GetAllDepartmentsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDepartment(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
