using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCompanyManagement.DataAccessMongoDB.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        void Create(T item);

        T GetById(int id);

        List<T> GetAll();

        void Delete(int id);
    }
}
