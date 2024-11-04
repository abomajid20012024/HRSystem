using AutoMapper;
using HRSystem.Application.DTOs.CreateDto;
using HRSystem.Application.DTOs.ShowDto;
using HRSystem.Application.DTOs.UpdateDto;
using HRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<DepartmentCreateDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<SalaryTiers, SalaryTiersDto>().ReverseMap();
        }
    }
}
