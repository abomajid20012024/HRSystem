using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.SalaryTiers
{
    public class SalaryTiersDto
    {
        public Guid SalaryTiersId { get; set; }
        public required string TierName { get; set; }
        public float BaseSalary { get; set; }
        public float Bonus { get; set; }
        public Guid DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public SalaryTiersDto()
        {

        }
    }
}
