using AutoMapper;
using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.IRepository;
using HRSystem.Application.Services.IServices;
using HRSystem.Domain.Entities;
using HRSystem.Shard;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(IEnumerable<EmployeeDto?>, PaginationMetaData?)> GetAllEmployiesAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogWarning("Invalid pagination parameters");
                return (Enumerable.Empty<EmployeeDto>(), null);
            }
            var (items, pagination) = await _employeeRepository.GetAllEmployeesAsync(pageNumber, pageSize);
            var itemsMapper = _mapper.Map<List<EmployeeDto>>(items);
            if (items is not null)
            {
                return (itemsMapper, pagination);
            }
            else { return (Enumerable.Empty<EmployeeDto>(), null); }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid employee ID");
                return null;
            }
            var item = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (item is not null)
            {
                return _mapper.Map<EmployeeDto>(item);

            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployiesByNameAsync(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var employees = await _employeeRepository.GetEmployeeByNameAsync(name);

                    var items = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                    return items;
                }
                return Enumerable.Empty<EmployeeDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<EmployeeDto>();
            }
        }


        public async Task<bool> AddEmployeeAsync(EmployeeCreateDto employeeCreate)
        {
            if (employeeCreate == null)
            {
                _logger.LogWarning("Employee DTO is null");
                return false;
            }

            // Map EmployeeDto to Employee entity
            var employee = _mapper.Map<Employee>(employeeCreate);

            // Additional validation if needed (e.g., required fields, value checks)
            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName))
            {
                _logger.LogWarning("Employee's first name or last name is missing");
                return false;
            }

            return await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeUpdateDto employeeUpdate)
        {
            if (employeeUpdate == null || employeeUpdate.EmployeeId == Guid.Empty)
            {
                _logger.LogWarning("Invalid employee data for update");
                return false;
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeUpdate.EmployeeId);
            if (employee == null)
            {
                _logger.LogWarning($"Employee with ID {employeeUpdate.EmployeeId} not found");
                return false;
            }

            // Map updated properties from DTO to entity
            _mapper.Map(employeeUpdate, employee);

            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid employee ID for deletion");
                return false;
            }

            return await _employeeRepository.DeleteEmployeeAsync(id);
        }

        public async Task<bool> DeleteEmployeeSoftAsync(Guid idEmployee)
        {
            if (idEmployee == Guid.Empty)
            {
                _logger.LogWarning("Invalid employee ID for soft deletion");
                return false;
            }

            return await _employeeRepository.DeleteEmployeeSoftAsync(idEmployee);
        }
    }
}
