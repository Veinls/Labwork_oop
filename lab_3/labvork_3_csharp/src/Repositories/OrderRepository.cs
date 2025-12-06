using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Dictionary<string, Order> _orders = new();
    public void Save(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        _orders[order.Id] = order;
    }
    public Order? GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException("Id не может быть пустым или равным 0",nameof(id));
        
        if (_orders.TryGetValue(id, out var order))
            return order;
        
        return null;
    }
    
    public List<Order> GetByCustomer(string customerPhone)
    {
        if (string.IsNullOrEmpty(customerPhone))
            throw new ArgumentException("Телефон клиента не может быть пустым", nameof(customerPhone));
        
        return _orders.Values
            .Where(order => order.Customer.Phone == customerPhone)
            .OrderByDescending(order => order.CreatedAt)
            .ToList();
    }
}