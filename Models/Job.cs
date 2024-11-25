﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public JobCategory Category { get; set; }
        public ICollection<JobAssignment> JobAssignments { get; set; }
    }
}
