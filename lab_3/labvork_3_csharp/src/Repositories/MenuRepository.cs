using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly Dictionary<string, MenuItem> _menuItems = new()
    {
        { "1", new MenuItem("1","Пицца", 12.99m) },
        { "2", new MenuItem("2", "Суши", 18.50m) },
        { "3", new MenuItem("3","Бургер", 8.75m) },
        { "4", new MenuItem("4","Салат", 9.25m) },
        { "5", new MenuItem("5","Кофе", 4.50m) }
    };

    public MenuItem? GetById(string id) => _menuItems.ContainsKey(id) ? _menuItems[id] : null;

    public List<MenuItem> GetAll() => _menuItems.Values.ToList();
}