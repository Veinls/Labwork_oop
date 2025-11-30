using labvork_3_csharp.Domain;
using labvork_3_csharp.Pricing;
using labvork_3_csharp.Repositories;

namespace labvork_3_csharp.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IPricingStrategy _pricing;

    public OrderService(IOrderRepository repository, IPricingStrategy pricing)
    {
        _repository = repository;
        _pricing = pricing;
    }

    public Order CreateOrder(Customer customer, List<MenuItem> items)
    {
        var order = new Order(customer);
        items.ForEach(item => order.AddItem(item, 1));
        _pricing.CalculateTotal(order);
        
        _repository.Save(order);
        return order;
    }
}