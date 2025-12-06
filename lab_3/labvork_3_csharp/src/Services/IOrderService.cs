using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Services;

public interface IOrderService
{
    Order CreateOrder(Customer customer, List<MenuItem> items);
}