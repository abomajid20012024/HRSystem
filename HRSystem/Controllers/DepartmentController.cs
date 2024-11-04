using AutoMapper;
using HRSystem.Application.DTOs.CreateDto;
using HRSystem.Application.DTOs.ShowDto;
using HRSystem.Application.DTOs.UpdateDto;
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
        private readonly AddDepartmentCommand AddDepartmentCommand;
        private readonly GetDepartmentQuery getDepartmentQuery;
        private readonly DeleteDepartmentCommand deleteDepartmentCommand;
        private readonly UpdateDepartmentCommand updateDepartmentCommand;
        private readonly ILogger<DepartmentController> logger;
        private readonly IMapper mapper;

        public DepartmentController
            (
                AddDepartmentCommand departmentCommand,
                GetDepartmentQuery getDepartmentQuery,
                DeleteDepartmentCommand deleteDepartmentCommand,
                UpdateDepartmentCommand updateDepartmentCommand,
                ILogger<DepartmentController> logger,
                IMapper mapper
            )
        {
            this.AddDepartmentCommand = departmentCommand;
            this.getDepartmentQuery = getDepartmentQuery;
            this.deleteDepartmentCommand = deleteDepartmentCommand;
            this.updateDepartmentCommand = updateDepartmentCommand;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DepartmentCreateDto departmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentCheck = await AddDepartmentCommand.Excute(mapper.Map<Department>(departmentDto));
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
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(Guid id)
        {
            try
            {


                var departmentCheck = await getDepartmentQuery.GetDepartmentByIdAsync(id);
                if (departmentCheck is not null)
                {
                    return Ok(departmentCheck);
                }
                else
                {
                    return NotFound("Not found this department please check if correct id");
                }


            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Controller in action GetDepartment");
                return BadRequest();
            }
        }
        [HttpGet()]
        public async Task<ActionResult> GetDepartments([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var departmentCheck = await getDepartmentQuery.GetAllDepartmentsAsync(pageNumber, pageSize);
                if (departmentCheck.Any())
                {
                    return Ok(departmentCheck);
                }
                else
                {
                    return NotFound("Not found any department");
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Controller in action Get");
                return BadRequest();
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var departmentCheck = await deleteDepartmentCommand.Excute(id);
                if (departmentCheck)
                {
                    return StatusCode(204);
                }
                else
                {
                    return NotFound("Not found any department");
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Controller in action Delete");
                return BadRequest();
            }
        }
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> DeleteDepartmentSoftDelete(Guid id)
        {
            try
            {
                var departmentCheck = await deleteDepartmentCommand.ExcuteSoftDelete(id);
                if (departmentCheck)
                {
                    return StatusCode(204);
                }
                else
                {
                    return NotFound("Not found any department");
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Controller in action DeleteSoft");
                return BadRequest();
            }
        }
        [HttpPatch("update")]
        public async Task<ActionResult> UpdateDepartment(DepartmentUpdateDto departmentUpdateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentCheck = await updateDepartmentCommand.Excute(departmentUpdateDto);
                    if (departmentCheck)
                    {
                        return StatusCode(204);
                    }
                    else
                    {
                        return NotFound("Not found any department");
                    }
                }
                else
                {
                    return BadRequest("Please input correct data");
                }

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error ocurs in Department Controller in action Update");
                return BadRequest();
            }
        }
    }

}



