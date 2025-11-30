using labvork_3_csharp.Domain;
using labvork_3_csharp.Pricing;

namespace labvork_3_csharp.OrderEnhancements;

public class ExpressDeliveryDecorator : IOrderDecorator
{
    private readonly Order _order;
    private readonly decimal _expressDeliveryCost;
    private readonly bool _isExpressAvailable;

    public ExpressDeliveryDecorator(Order order)
    {
        _order = order;
        (_isExpressAvailable, _expressDeliveryCost) = CalculateExpressCost();
        ApplyExpressFeatures();
    }
    
    private (bool isAvailable, decimal cost) CalculateExpressCost()
    {
        try
        {
            var expressStrategy = new ExpressDeliveryStrategy();
            var deliveryCost = expressStrategy.CalculateDeliveryCost(_order);
            return (true, deliveryCost);
        }
        catch (InvalidOperationException)
        {
            return (false, 0m);
        }
    }
    
    private void ApplyExpressFeatures()
    {
        if (_isExpressAvailable)
        {
            _order.AddItem(new MenuItem("express_delivery", "Экспресс-доставка", _expressDeliveryCost), 1);
            _order.AddItem(new MenuItem("bonus", "Бесплатный напиток", 0), 1);
            _order.DeliveryType = "Express";
        }
        else
        {
            _order.DeliveryType = "Standard";
        }
    }
    
    public void DisplayEnhancedOrder()
    {
        Console.WriteLine($"Экспресс-заказ {_order.Id}");
        Console.WriteLine("Состав заказа:");
        foreach (var item in _order.Items)
        {
            var priceInfo = item.Price == 0 ? "БЕСПЛАТНО" : $"{item.Price:C}";
            Console.WriteLine($"  {item.MenuItem.Name} x{item.Quantity} - {priceInfo}");
        }
        Console.WriteLine($"Итого: {_order.Total:C}");
    }
    
    public string GetOrderInfo()
    {
        return $"Заказ {_order.Id} | Сумма: {_order.Total:C} | Тип: Express";
    }
}