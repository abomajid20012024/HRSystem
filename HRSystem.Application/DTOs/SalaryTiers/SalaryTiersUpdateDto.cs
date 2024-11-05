using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Application.DTOs.SalaryTiers
{
    public class SalaryTiersUpdateDto
    {
        public Guid SalaryTierId { get; set; }
        public required string TierName { get; set; }
        public float BaseSalary { get; set; }
        public float Bonus { get; set; }
        public SalaryTiersUpdateDto()
        {

        }
    }
}
