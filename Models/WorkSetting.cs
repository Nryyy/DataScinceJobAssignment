using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WorkSetting
    {
        public int Id { get; set; }
        public string Setting { get; set; }
        public ICollection<JobAssignment> JobAssignments { get; set; }
    }
}
