using AutoMapper;
using HRSystem.Application.DTOs.ShowDto;
using HRSystem.Application.Interfaces;
using HRSystem.Domain.Entities;
using HRSystem.Domain.Interfaces;
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentRepository> logger;

        public DepartmentRepository
            (
            HRSystemDBContext context,
            IMapper mapper,
            ILogger<DepartmentRepository> logger

            )
        {
            this._context = context;
            this.mapper = mapper;
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

        public async Task<bool> DeleteDepartment(Guid idDepartment)
        {
            try
            {
                var item = await GetDepartmentByIdAsync(idDepartment);
                if (item is not null)
                {
                    _context.Departments.Remove(item);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in DepartmentRepository in function DeleteDepartment");
                return false;
            }
        }

        public async Task<bool> DeleteDepartmentSoft(Guid idDepartment)
        {
            try
            {
                var item = await GetDepartmentByIdAsync(idDepartment);
                if (item is not null)
                {
                    item.IsActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in DepartmentRepository in function DeleteDepartmentSoftDelete");
                return false;
            }
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalItemsCount = await _context.Departments.CountAsync();
                if (totalItemsCount > 0)
                {
                    var departments = await _context.Departments
                                                    //.Where(g => g.Active == true)
                                                    .Skip(pageSize * (pageNumber - 1))
                                                    .Take(pageSize)
                                                    .OrderBy(g => g.DepartmetnName)
                                                    .Select(d => new DepartmentDto
                                                    {
                                                        DepartmentId = d.DepartmentId,
                                                        DepartmetnName = d.DepartmetnName
                                                    })
                                                    .ToListAsync();
                    if (departments.Count > 0)
                    {
                        return mapper.Map<IEnumerable<DepartmentDto>>(departments);
                    }
                    else
                    {
                        return Enumerable.Empty<DepartmentDto>();
                    }

                }
                else
                {
                    return Enumerable.Empty<DepartmentDto>();
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in DepartmentRepository in Function GetDepartments");
                return Enumerable.Empty<DepartmentDto>();

            }
        }

        public async Task<Department> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                var item = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
                if (item is not null)
                {
                    return item;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in DepartmentRepository in Function GetDepartmentByIdAsync");
                return null;


            }
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            try
            {
                if (department is not null)
                {
                    var item = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == department.DepartmentId && d.IsActive);
                    if (item is not null)
                    {
                        item.DepartmetnName = department.DepartmetnName;
                        await _context.SaveChangesAsync();
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
                logger.LogCritical(ex, "error ocurs in departmentRepository and in method UpdateDepartment");
                return false;
            }
        }
    }
}
