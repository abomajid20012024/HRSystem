using AutoMapper;
using HRSystem.Application.DTOs.SalaryTiers;
using HRSystem.Application.IRepository;
using HRSystem.Application.Services.IServices;
using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Repositories.IRepository;
using Microsoft.Extensions.Logging;

namespace HRSystem.Application.Services
{
    public class SalaryTiersService : ISalaryTiersService
    {
        private readonly ISalaryTiersRepository _salaryTiersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SalaryTiersService> _logger;

        public SalaryTiersService(
            ISalaryTiersRepository salaryTiersRepository,
            IMapper mapper,
            ILogger<SalaryTiersService> logger
        )
        {
            _salaryTiersRepository = salaryTiersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddSalaryTierAsync(SalaryTiersCreateDto salaryTierDto)
        {
            if (salaryTierDto == null)
            {
                _logger.LogWarning("SalaryTier DTO is null");
                return false;
            }

            var salaryTier = _mapper.Map<SalaryTiers>(salaryTierDto);

            if (string.IsNullOrEmpty(salaryTier.TierName))
            {
                _logger.LogWarning("Invalid SalaryTier data: missing name or invalid level");
                return false;
            }

            return await _salaryTiersRepository.AddSalaryTierAsync(salaryTier);
        }

        public async Task<SalaryTiersDto> GetSalaryTierByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid SalaryTier ID");
                return null;
            }

            var salaryTier = await _salaryTiersRepository.GetSalaryTierByIdAsync(id);
            return salaryTier != null ? _mapper.Map<SalaryTiersDto>(salaryTier) : null;
        }

        public async Task<IEnumerable<SalaryTiersDto>> GetAllSalaryTiersAsync(
            int pageNumber,
            int pageSize
        )
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogWarning("Invalid pagination parameters for SalaryTiers");
                return Enumerable.Empty<SalaryTiersDto>();
            }

            var salaryTiers = await _salaryTiersRepository.GetAllSalaryTiersAsync(
                pageNumber,
                pageSize
            );
            return _mapper.Map<IEnumerable<SalaryTiersDto>>(salaryTiers);
        }

        public async Task<bool> UpdateSalaryTierAsync(SalaryTiersUpdateDto salaryTierUpdateDto)
        {
            if (salaryTierUpdateDto == null || salaryTierUpdateDto.SalaryTierId == Guid.Empty)
            {
                _logger.LogWarning("Invalid SalaryTier data for update");
                return false;
            }

            var salaryTier = await _salaryTiersRepository.GetSalaryTierByIdAsync(
                salaryTierUpdateDto.SalaryTierId
            );
            if (salaryTier == null)
            {
                _logger.LogWarning(
                    $"SalaryTier with ID {salaryTierUpdateDto.SalaryTierId} not found"
                );
                return false;
            }

            _mapper.Map(salaryTierUpdateDto, salaryTier);
            return await _salaryTiersRepository.UpdateSalaryTierAsync(salaryTier);
        }

        public async Task<bool> DeleteSalaryTierAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid SalaryTier ID for deletion");
                return false;
            }

            return await _salaryTiersRepository.DeleteSalaryTierAsync(id);
        }

        public async Task<bool> DeleteSalaryTierSoftAsync(Guid idSalaryTier)
        {
            if (idSalaryTier == Guid.Empty)
            {
                _logger.LogWarning("Invalid SalaryTier ID for soft deletion");
                return false;
            }

            return await _salaryTiersRepository.DeleteSalaryTierSoftAsync(idSalaryTier);
        }

        public async Task<IEnumerable<SalaryTiersReportResponse>> GetRportSalaryTierAsync()
        {
            try
            {
                var item = _mapper.Map<IEnumerable<SalaryTiersReportResponse>>(await _salaryTiersRepository.GetReportSalaryTierAsync());
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<SalaryTiersReportResponse> GetRportSalaryTierAsync(Guid idEmployee)
        {
            throw new NotImplementedException();
        }


    }
}
