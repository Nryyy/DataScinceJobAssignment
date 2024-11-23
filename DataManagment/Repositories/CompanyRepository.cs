using DataManagment.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManagment.Repositories
{
    public class CompanyRepository : IRepositoryR<Company>, IDisposable
    {
        private readonly AppContext _context;

        public CompanyRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<Company> Get()
        {
            return _context.Companies
                .Include(c => c.Size)
                .Include(c => c.EmploymentType)
                .Include(c => c.JobAssignments)
                .ToList();
        }

        public Company Get(int id)
        {
            return _context.Companies
                .Include(c => c.Size)
                .Include(c => c.EmploymentType)
                .Include(c => c.JobAssignments)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
