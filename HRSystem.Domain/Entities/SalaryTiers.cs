using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.Domain.Entities
{
    public class SalaryTiers
    {
        public Guid SalaryTiersId { get; set; }
        public required string TierName { get; set; }
        public float BaseSalary { get; set; }
        public float Bonus { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public bool IsActive { get; set; } = true;

        public SalaryTiers()
        {
            SalaryTiersId = Guid.NewGuid();
        }
    }
}
