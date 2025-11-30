using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Repositories;

public interface IMenuRepository
{
    MenuItem? GetById(string id);
    List<MenuItem> GetAll();
}