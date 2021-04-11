using System.Collections.Generic;

namespace RepairCompanyManagement.DataAccess.Interfaces
{
    public interface IRepository<T> 
        where T : class
    {
        int Create(T item);
        
        T GetById(int id);
        
        IEnumerable<T> GetAll();
        
        void Update(T item);
        
        void Delete(int id);
    }
}