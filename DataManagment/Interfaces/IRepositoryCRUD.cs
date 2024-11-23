using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagment.Interfaces
{
    public interface IRepositoryCRUD<T> : IDisposable
    {
        void Create(T entity);
        IEnumerable<T> Get();
        T Get(int id);
        void Update(T entity);
        void Delete(int id);
        void SaveChanges();
    }
}
