namespace HRSystem.WebAPI.Controllers
{
    using HRSystem.Application.DTOs.SalaryTiers;
    using HRSystem.Application.UseCases.Employee;
    using HRSystem.Application.UseCases.SalaryTiers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="SalaryTiersController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class SalaryTiersController : ControllerBase
    {
        /// <summary>
        /// Defines the _addSalaryTiersCommand
        /// </summary>
        private readonly AddSalaryTiersCommand _addSalaryTiersCommand;

        /// <summary>
        /// Defines the _getSalaryTiersQuery
        /// </summary>
        private readonly GetSalaryTiersQuery _getSalaryTiersQuery;

        /// <summary>
        /// Defines the _deleteSalaryTiersCommand
        /// </summary>
        private readonly DeleteSalaryTiersCommand _deleteSalaryTiersCommand;

        /// <summary>
        /// Defines the _updateSalaryTiersCommand
        /// </summary>
        private readonly UpdateSalaryTiersCommand _updateSalaryTiersCommand;
        private readonly GetReportAboutSalary getReportAboutSalary;

        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<SalaryTiersController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalaryTiersController"/> class.
        /// </summary>
        /// <param name="addSalaryTiersCommand">The addSalaryTiersCommand<see cref="AddSalaryTiersCommand"/></param>
        /// <param name="getSalaryTiersQuery">The getSalaryTiersQuery<see cref="GetSalaryTiersQuery"/></param>
        /// <param name="deleteSalaryTiersCommand">The deleteSalaryTiersCommand<see cref="DeleteSalaryTiersCommand"/></param>
        /// <param name="updateSalaryTiersCommand">The updateSalaryTiersCommand<see cref="UpdateSalaryTiersCommand"/></param>
        /// <param name="logger">The logger<see cref="ILogger{SalaryTiersController}"/></param>
        public SalaryTiersController(
            AddSalaryTiersCommand addSalaryTiersCommand,
            GetSalaryTiersQuery getSalaryTiersQuery,
            DeleteSalaryTiersCommand deleteSalaryTiersCommand,
            UpdateSalaryTiersCommand updateSalaryTiersCommand,
            GetReportAboutSalary getReportAboutSalary,
            ILogger<SalaryTiersController> logger
        )
        {
            _addSalaryTiersCommand = addSalaryTiersCommand;
            _getSalaryTiersQuery = getSalaryTiersQuery;
            _deleteSalaryTiersCommand = deleteSalaryTiersCommand;
            _updateSalaryTiersCommand = updateSalaryTiersCommand;
            this.getReportAboutSalary = getReportAboutSalary;
            _logger = logger;
        }

        /// <summary>
        /// The CreateSalaryTier
        /// </summary>
        /// <param name="salaryTierDto">The salaryTierDto<see cref="SalaryTiersCreateDto"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPost]
        public async Task<ActionResult> CreateSalaryTier([FromBody] SalaryTiersCreateDto salaryTierDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _addSalaryTiersCommand.Execute(salaryTierDto);
                    return result ? Ok() : BadRequest("Failed to create salary tier.");
                }
                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateSalaryTier");
                return BadRequest();
            }
        }

        /// <summary>
        /// The GetSalaryTier
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpGet("report")]
        public async Task<ActionResult> GetReportSalaryTier()
        {
            try
            {
                var salaryTierReport = await getReportAboutSalary.Execute();
                return salaryTierReport != null ? Ok(salaryTierReport) : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSalaryTier");
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSalaryTier(Guid id)
        {
            try
            {
                var salaryTier = await _getSalaryTiersQuery.GetSalaryTierByIdAsync(id);
                return salaryTier != null ? Ok(salaryTier) : NotFound("Salary tier not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSalaryTier");
                return BadRequest();
            }
        }
        /// <summary>
        /// The GetSalaryTiers
        /// </summary>
        /// <param name="pageNumber">The pageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpGet]
        public async Task<ActionResult> GetSalaryTiers([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var salaryTiers = await _getSalaryTiersQuery.GetSalaryTiersAsync(pageNumber, pageSize);
                return salaryTiers.Any() ? Ok(salaryTiers) : NotFound("No salary tiers found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSalaryTiers");
                return BadRequest();
            }
        }

        /// <summary>
        /// The DeleteSalaryTier
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSalaryTier(Guid id)
        {
            try
            {
                var result = await _deleteSalaryTiersCommand.Execute(id);
                return result ? NoContent() : NotFound("Salary tier not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteSalaryTier");
                return BadRequest();
            }
        }

        /// <summary>
        /// The DeleteSalaryTierSoft
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPut("delete/{id}")]
        public async Task<ActionResult> DeleteSalaryTierSoft(Guid id)
        {
            try
            {
                var result = await _deleteSalaryTiersCommand.ExecuteSoftDelete(id);
                return result ? NoContent() : NotFound("Salary tier not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteSalaryTierSoft");
                return BadRequest();
            }
        }

        /// <summary>
        /// The UpdateSalaryTier
        /// </summary>
        /// <param name="salaryTierUpdateDto">The salaryTierUpdateDto<see cref="SalaryTiersUpdateDto"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPatch("update")]
        public async Task<ActionResult> UpdateSalaryTier([FromBody] SalaryTiersUpdateDto salaryTierUpdateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _updateSalaryTiersCommand.Execute(salaryTierUpdateDto);
                    return result ? NoContent() : NotFound("Salary tier not found.");
                }
                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateSalaryTier");
                return BadRequest();
            }
        }
    }
}
