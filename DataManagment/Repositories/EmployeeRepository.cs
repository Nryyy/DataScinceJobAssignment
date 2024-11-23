using DataManagment.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManagment.Repositories
{
    public class EmployeeRepository : IRepositoryCRUD<Employee>
    {
        private readonly AppContext _context;

        public EmployeeRepository(AppContext context)
        {
            _context = context;
        }

        public void Create(Employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Employees.Add(entity);
            SaveChanges();
        }

        public IEnumerable<Employee> Get()
        {
            return _context.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return _context.Employees.FirstOrDefault(employee => employee.Id == id);
        }

        public void Update(Employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var employee = Get(entity.Id);
            if (employee != null)
            {
                employee.Residence = entity.Residence;
                employee.ExperienceLevelId = entity.ExperienceLevelId;
                employee.ExperienceLevel = entity.ExperienceLevel;
                employee.JobAssignments = entity.JobAssignments;
                SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var employee = Get(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
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

