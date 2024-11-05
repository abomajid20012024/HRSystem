namespace HRSystem.WebAPI.Controllers
{
    using HRSystem.Application.DTOs.Employee;
    using HRSystem.Application.UseCases.Employee;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="EmployeeController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Defines the _addEmployeeCommand
        /// </summary>
        private readonly AddEmployeeCommand _addEmployeeCommand;

        /// <summary>
        /// Defines the _getEmployeeQuery
        /// </summary>
        private readonly GetEmployeeQuery _getEmployeeQuery;

        /// <summary>
        /// Defines the _deleteEmployeeCommand
        /// </summary>
        private readonly DeleteEmployeeCommand _deleteEmployeeCommand;

        /// <summary>
        /// Defines the _updateEmployeeCommand
        /// </summary>
        private readonly UpdateEmployeeCommand _updateEmployeeCommand;

        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<EmployeeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="addEmployeeCommand">The addEmployeeCommand<see cref="AddEmployeeCommand"/></param>
        /// <param name="getEmployeeQuery">The getEmployeeQuery<see cref="GetEmployeeQuery"/></param>
        /// <param name="deleteEmployeeCommand">The deleteEmployeeCommand<see cref="DeleteEmployeeCommand"/></param>
        /// <param name="updateEmployeeCommand">The updateEmployeeCommand<see cref="UpdateEmployeeCommand"/></param>
        /// <param name="logger">The logger<see cref="ILogger{EmployeeController}"/></param>
        public EmployeeController(
            AddEmployeeCommand addEmployeeCommand,
            GetEmployeeQuery getEmployeeQuery,
            DeleteEmployeeCommand deleteEmployeeCommand,
            UpdateEmployeeCommand updateEmployeeCommand,
            ILogger<EmployeeController> logger
        )
        {
            _addEmployeeCommand = addEmployeeCommand;
            _getEmployeeQuery = getEmployeeQuery;
            _deleteEmployeeCommand = deleteEmployeeCommand;
            _updateEmployeeCommand = updateEmployeeCommand;
            _logger = logger;
        }

        /// <summary>
        /// The CreateEmployee
        /// </summary>
        /// <param name="employeeDto">The employeeDto<see cref="EmployeeCreateDto"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeCreateDto employeeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _addEmployeeCommand.Execute(employeeDto);
                    return result ? Ok() : BadRequest("Failed to create employee.");
                }
                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateEmployee");
                return BadRequest();
            }
        }
        /// <summary>
        /// The GetEmployee
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(Guid id)
        {
            try
            {
                var employee = await _getEmployeeQuery.GetEmployeeByIdAsync(id);
                return employee != null ? Ok(employee) : NotFound("Employee not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployee");
                return BadRequest();
            }
        }

        /// <summary>
        /// The GetEmployees
        /// </summary>
        /// <param name="pageNumber">The pageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpGet]
        public async Task<ActionResult> GetEmployees([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var employees = await _getEmployeeQuery.GetEmployeesAsync(pageNumber, pageSize);
                return employees.Any() ? Ok(employees) : NotFound("No employees found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployees");
                return BadRequest();
            }
        }

        /// <summary>
        /// The DeleteEmployee
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await _deleteEmployeeCommand.Execute(id);
                return result ? NoContent() : NotFound("Employee not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteEmployee");
                return BadRequest();
            }
        }

        /// <summary>
        /// The DeleteEmployeeSoft
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPut("delete/{id}")]
        public async Task<ActionResult> DeleteEmployeeSoft(Guid id)
        {
            try
            {
                var result = await _deleteEmployeeCommand.ExecuteSoftDelete(id);
                return result ? NoContent() : NotFound("Employee not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteEmployeeSoft");
                return BadRequest();
            }
        }

        /// <summary>
        /// The UpdateEmployee
        /// </summary>
        /// <param name="employeeUpdateDto">The employeeUpdateDto<see cref="EmployeeUpdateDto"/></param>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [HttpPatch("update")]
        public async Task<ActionResult> UpdateEmployee([FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _updateEmployeeCommand.Execute(employeeUpdateDto);
                    return result ? NoContent() : NotFound("Employee not found.");
                }
                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateEmployee");
                return BadRequest();
            }
        }
    }
}
