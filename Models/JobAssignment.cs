using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JobAssignment
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int WorkSettingId { get; set; }
        public WorkSetting WorkSetting { get; set; }
        public int WorkYear { get; set; }
        public string SalaryCurrency { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryInUSD { get; set; }
    }
}
