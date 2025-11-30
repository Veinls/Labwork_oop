using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Repositories;

public interface IOrderRepository
{
    void Save(Order order);
    Order? GetById(string id);
    List<Order> GetByCustomer(string customerId);
}