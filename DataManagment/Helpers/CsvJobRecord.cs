using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagment.Helpers
{
    public class CsvJobRecord
    {
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string CompanyLocation { get; set; }
        public string EmployeeResidence { get; set; }
        public string ExperienceLevel { get; set; }
        public string EmploymentType { get; set; }
        public string WorkSetting { get; set; }
        public string CompanySize { get; set; }
        public string SalaryCurrency { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryInUSD { get; set; }
        public int WorkYear { get; set; }
    }

}
