using DataManagment.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManagment.Repositories
{
    public class JobAssignmentRepository : IRepositoryCRUD<JobAssignment>
    {
        private readonly AppContext _context;

        public JobAssignmentRepository(AppContext context)
        {
            _context = context;
        }

        public void Create(JobAssignment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.JobAssignments.Add(entity);
            SaveChanges();
        }

        public IEnumerable<JobAssignment> Get()
        {
            return _context.JobAssignments.ToList();
        }

        public JobAssignment Get(int id)
        {
            return _context.JobAssignments.FirstOrDefault(jobAssignment => jobAssignment.Id == id);
        }

        public void Update(JobAssignment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var jobAssignment = Get(entity.Id);
            if (jobAssignment != null)
            {
                jobAssignment.JobId = entity.JobId;
                jobAssignment.Job = entity.Job;
                jobAssignment.EmployeeId = entity.EmployeeId;
                jobAssignment.Employee = entity.Employee;
                jobAssignment.CompanyId = entity.CompanyId;
                jobAssignment.Company = entity.Company;
                jobAssignment.WorkSettingId = entity.WorkSettingId;
                jobAssignment.WorkSetting = entity.WorkSetting;
                jobAssignment.WorkYear = entity.WorkYear;
                jobAssignment.SalaryCurrency = entity.SalaryCurrency;
                jobAssignment.Salary = entity.Salary;
                jobAssignment.SalaryInUSD = entity.SalaryInUSD;
                SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var jobAssignment = Get(id);
            if (jobAssignment != null)
            {
                _context.JobAssignments.Remove(jobAssignment);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}


