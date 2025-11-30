using labvork_3_csharp.Domain;
using labvork_3_csharp.Pricing;

namespace labvork_3_csharp.OrderCreation;

public class ExpressOrderFactory : OrderFactory
{
    public ExpressOrderFactory(IDeliveryStrategy deliveryStrategy) 
        : base(deliveryStrategy){}

    protected override string GetOrderType() => "Express";

    protected override void AddDeliveryCost(Order order)
    {
        decimal deliveryCost = _deliveryStrategy.CalculateDeliveryCost(order);
        if (deliveryCost > 0)
        {
            order.AddItem(new MenuItem("delivery", $"Срочная доставка", deliveryCost), 1);
            Console.WriteLine($"Добавлена срочная доставка: {deliveryCost:C}");
        }
        else
        {
            Console.WriteLine("Срочная доставка бесплатна!");
        }
    }
}