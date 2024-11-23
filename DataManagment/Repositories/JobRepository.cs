using DataManagment.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManagment.Repositories
{
    public class JobRepository : IRepositoryCRUD<Job>
    {
        private readonly AppContext _context;

        public JobRepository(AppContext context)
        {
            _context = context;
        }

        public void Create(Job entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Jobs.Add(entity);
            SaveChanges();
        }

        public IEnumerable<Job> Get()
        {
            return _context.Jobs.ToList();
        }

        public Job Get(int id)
        {
            return _context.Jobs.FirstOrDefault(job => job.Id == id);
        }

        public void Update(Job entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var job = Get(entity.Id);
            if (job != null)
            {
                job.Title = entity.Title;
                job.CategoryId = entity.CategoryId;
                job.Category = entity.Category;
                job.JobAssignments = entity.JobAssignments;
                SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var job = Get(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
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
