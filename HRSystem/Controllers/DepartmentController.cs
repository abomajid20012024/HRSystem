using AutoMapper;
using HRSystem.Application.DTOs;
using HRSystem.Application.UseCases.Department;
using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AddDepartmentCommand departmentCommand;
        private readonly ILogger<DepartmentController> logger;
        private readonly IMapper mapper;

        public DepartmentController
            (
                AddDepartmentCommand departmentCommand,
                ILogger<DepartmentController> logger,
                IMapper mapper
            )
        {
            this.departmentCommand = departmentCommand;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentCheck = await departmentCommand.Excute(mapper.Map<Department>(departmentDto));
                    if (departmentCheck)
                    {
                        return Ok();
                    }
                    else
                    {
                        logger.LogCritical("Error ocurs in Department Controller in action Craete and thie modelStatae is valid okey");

                        return BadRequest();

                    }
                }

                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Controller in action Craete");
                return BadRequest();
            }
        }
    }
}
