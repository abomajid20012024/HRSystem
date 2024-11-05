using System;
using System.Collections.Generic;

namespace HRSystem.Application.DTOs.SalaryTiers
{
    public class SalaryTiersReportResponse
    {
        // اسم الفئة
        public string TierName { get; set; }

        // الراتب الأساسي للفئة
        public float BaseSalary { get; set; }

        // المكافأة للفئة
        public float Bonus { get; set; }

        // عدد الموظفين في الفئة
        public int EmployeeCount { get; set; }

        // إجمالي الراتب (الراتب الأساسي + المكافأة) لكل الموظفين في هذه الفئة
        public float TotalSalary { get; set; }

        // قائمة الأقسام مع مجموع الرواتب في كل قسم
        public List<DepartmentSalaryInfo> DepartmentTotalSalaries { get; set; } = new List<DepartmentSalaryInfo>();
    }

    // كائن فرعي يمثل مجموع الرواتب في القسم
    public class DepartmentSalaryInfo
    {
        // اسم القسم
        public string DepartmentName { get; set; }

        // مجموع الرواتب لكل الموظفين في هذا القسم
        public float TotalDepartmentSalary { get; set; }
    }
}
