using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int SizeId { get; set; }
        public CompanySize Size { get; set; }
        public int EmploymentTypeId { get; set; }
        public EmploymentTypes EmploymentType { get; set; }
        public ICollection<JobAssignment> JobAssignments { get; set; }
    }
}
