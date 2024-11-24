using DataManagment.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace DataManagment.Repositories
{
    public class WorkSettingRepository : IRepositoryR<WorkSetting>
    {
        private readonly AppContext _context;

        public WorkSettingRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<WorkSetting> Get()
        {
            return _context.WorkSettings
                .Include(ws => ws.JobAssignments)
                .ToList();
        }

        public WorkSetting Get(int id)
        {
            return _context.WorkSettings
                .Include(ws => ws.JobAssignments)
                .FirstOrDefault(ws => ws.Id == id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
