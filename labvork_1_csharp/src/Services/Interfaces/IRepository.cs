using labvork_1_csharp.Models;

namespace labvork_1_csharp.Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T? GetByID(int ID);
        IEnumerable<T> GetAll();
        void Add(T entity);
        bool Remove(int ID);
    }
}