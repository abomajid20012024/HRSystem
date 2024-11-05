using AutoMapper;
using HRSystem.Application.DTOs.Department;
using HRSystem.Application.Services.IServices;
using HRSystem.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Department
{
    public class UpdateDepartmentCommand
    {
        private readonly IDepartmentService departmentService;
        private readonly ILogger<UpdateDepartmentCommand> logger;
        private readonly IMapper mapper;

        public UpdateDepartmentCommand
            (
            IDepartmentService departmentService,
            ILogger<UpdateDepartmentCommand> logger,
            IMapper mapper
            )
        {
            this.departmentService = departmentService;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<bool> Excute(DepartmentUpdateDto departmentUpdateDto)
        {
            try
            {
                if (departmentUpdateDto == null)
                {
                    return await Task.FromResult(false);

                }
                return await departmentService.UpdateDepartment(departmentUpdateDto);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "error ocurs in UpdateDepartmentCommand ");
                return false;
            }
        }
    }
}
