using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.DTOs.SalaryTiers;
using HRSystem.Application.Services;
using HRSystem.Application.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.UseCases.Employee
{
    public class GetReportAboutSalary
    {
        private readonly ISalaryTiersService salaryTiersService;

        public GetReportAboutSalary(ISalaryTiersService salaryTiersService)
        {
            this.salaryTiersService = salaryTiersService;
        }
        public Task<IEnumerable<SalaryTiersReportResponse>> Execute()
        {
            return salaryTiersService.GetRportSalaryTierAsync();
        }
    }
}
