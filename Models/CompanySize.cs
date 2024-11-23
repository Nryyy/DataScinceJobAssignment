using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CompanySize
    {
        public int Id { get; set; }
        public string SizeCode { get; set; }
        public string Description { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
