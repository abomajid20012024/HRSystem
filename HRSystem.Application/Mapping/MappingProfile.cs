using AutoMapper;
using HRSystem.Application.DTOs.Department;
using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.DTOs.SalaryTiers;
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
            CreateMap<DepartmentCreateDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            //mapping employee
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            // mapping SalaryTiers
            CreateMap<SalaryTiers, SalaryTiersDto>().ReverseMap();
            CreateMap<SalaryTiers, SalaryTiersCreateDto>().ReverseMap();
            CreateMap<SalaryTiers, SalaryTiersUpdateDto>().ReverseMap();


        }
    }
}
