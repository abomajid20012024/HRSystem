namespace HRSystem.WebAPI.Controllers
{
    using AutoMapper;
    using HRSystem.Application.DTOs.Department;
    using HRSystem.Application.UseCases.Department;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="DepartmentController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        /// Defines the AddDepartmentCommand
        /// </summary>
        private readonly AddDepartmentCommand AddDepartmentCommand;

        /// <summary>
        /// Defines the getDepartmentQuery
        /// </summary>
        private readonly GetDepartmentQuery getDepartmentQuery;

        /// <summary>
        /// Defines the deleteDepartmentCommand
        /// </summary>
        private readonly DeleteDepartmentCommand deleteDepartmentCommand;

        /// <summary>
        /// Defines the updateDepartmentCommand
        /// </summary>
        private readonly UpdateDepartmentCommand updateDepartmentCommand;

        /// <summary>
        /// Defines the logger
        /// </summary>
        private readonly ILogger<DepartmentController> logger;

        /// <summary>
        /// Defines the mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentController"/> class.
        /// </summary>
        /// <param name="departmentCommand">The departmentCommand<see cref="AddDepartmentCommand"/></param>
        /// <param name="getDepartmentQuery">The getDepartmentQuery<see cref="GetDepartmentQuery"/></param>
        /// <param name="deleteDepartmentCommand">The deleteDepartmentCommand<see cref="DeleteDepartmentCommand"/></param>
        /// <param name="updateDepartmentCommand">The updateDepartmentCommand<see cref="UpdateDepartmentCommand"/></param>
        /// <param name="logger">The logger<see cref="ILogger{DepartmentController}"/></param>
        /// <param name="mapper">The mapper<see cref="IMapper"/></param>
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

        /// <summary>
        /// The Create
        /// </summary>
        /// <param name="departmentDto">The departmentDto<see cref="DepartmentCreateDto"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DepartmentCreateDto departmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentCheck = await AddDepartmentCommand.Excute(departmentDto);
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

        /// <summary>
        /// The GetDepartment
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
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

        /// <summary>
        /// The GetDepartments
        /// </summary>
        /// <param name="pageNumber">The pageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
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

        /// <summary>
        /// The DeleteDepartment
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
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

        /// <summary>
        /// The DeleteDepartmentSoftDelete where put activate is false
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPut("delete/{id}")]
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

        /// <summary>
        /// The UpdateDepartment
        /// </summary>
        /// <param name="departmentUpdateDto">The departmentUpdateDto<see cref="DepartmentUpdateDto"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
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
