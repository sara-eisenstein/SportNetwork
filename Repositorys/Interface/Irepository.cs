using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Interface
{
    public interface Irepository<T>
    {
        List<T> GetAll();
        T Get(int id);
        void Delete(int id);
        T Update(T entity);
       T Add(T entity);
    }
}
