using labvork_3_csharp.Domain;
using labvork_3_csharp.Pricing;

namespace labvork_3_csharp.OrderCreation;

public abstract class OrderFactory
{
    protected readonly IDeliveryStrategy _deliveryStrategy;

    protected OrderFactory(IDeliveryStrategy deliveryStrategy)
    {
        _deliveryStrategy = deliveryStrategy ?? throw new ArgumentNullException(nameof(deliveryStrategy));
    }

    public Order CreateOrder(Customer customer, List<MenuItem> items)
    {
        var order = new Order(customer);
        order.DeliveryType = GetOrderType();
        items.ForEach(item => order.AddItem(item, 1));
        AddDeliveryCost(order);
        
        return order;
    }

    protected virtual void AddDeliveryCost(Order order)
    {
        decimal deliveryCost = _deliveryStrategy.CalculateDeliveryCost(order);
        if (deliveryCost > 0)
        {
            order.AddItem(new MenuItem("delivery", $"Доставка ({GetOrderType()})", deliveryCost), 1);
        }
    }

    protected abstract string GetOrderType();
}