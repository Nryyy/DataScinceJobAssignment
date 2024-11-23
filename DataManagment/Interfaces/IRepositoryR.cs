using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagment.Interfaces
{
    public interface IRepositoryR<T> : IDisposable
    {
        IEnumerable<T> Get();
        T Get(int id);
    }
}
