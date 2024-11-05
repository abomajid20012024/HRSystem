using AutoMapper;
using HRSystem.Application.DTOs.Department;
using HRSystem.Application.Services.IServices;
using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Repositories.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILogger<DepartmentService> logger;
        private readonly IMapper mapper;

        public DepartmentService
            (
            IDepartmentRepository departmentRepository,
            ILogger<DepartmentService> logger,
           IMapper mapper
            )
        {
            this.departmentRepository = departmentRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<bool> AddDepartmentAsync(DepartmentCreateDto department)
        {
            try
            {
                if (department is not null)
                {
                    var item = mapper.Map<Department>(department);
                    if (await departmentRepository.AddDepartmentAsync(item))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Service in method AddDepartmentAsync");
                return false;

            }
        }

        public async Task<bool> DeleteDepartment(Guid idDepartment)
        {
            try
            {
                if (idDepartment != Guid.Empty)
                {
                    if (await departmentRepository.DeleteDepartmentAsync(idDepartment))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Service in method DeleteDepartment");
                return false;

            }
        }

        public async Task<bool> DeleteDepartmentSoft(Guid idDepartment)
        {
            try
            {
                if (idDepartment != Guid.Empty)
                {
                    if (await departmentRepository.DeleteDepartmentSoftAsync(idDepartment))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Service in method DeleteDepartmentSoft");
                return false;

            }
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(int pageNumber = 1, int pageSize = 1)
        {
            try
            {
                var items = await departmentRepository.GetAllDepartmentsAsync(pageNumber, pageSize);

                if (items.Any())
                {
                    return mapper.Map<IEnumerable<DepartmentDto>>(items);
                }
                else
                {
                    return Enumerable.Empty<DepartmentDto>();
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error occurred in Department Service in method GetAllDepartmentsAsync");
                return Enumerable.Empty<DepartmentDto>();
            }
        }


        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                var item = await departmentRepository.GetDepartmentByIdAsync(id);

                if (item is not null)
                {
                    return mapper.Map<DepartmentDto>(item);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error occurred in Department Service in method GetDepartmentByIdAsync");
                return null;

            }
        }

        public async Task<bool> UpdateDepartment(DepartmentUpdateDto department)
        {
            try
            {
                if (department is not null)
                {

                    var item = await departmentRepository.UpdateDepartmentAsync(mapper.Map<Department>(department));

                    if (item)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error occurred in Department Service in method UpdateDepartment");
                return false;

            }
        }
    }
}
