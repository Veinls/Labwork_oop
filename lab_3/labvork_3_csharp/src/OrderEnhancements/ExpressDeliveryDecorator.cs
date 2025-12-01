using labvork_3_csharp.Domain;
using labvork_3_csharp.Pricing;

namespace labvork_3_csharp.OrderEnhancements;

public class ExpressDeliveryDecorator : BaseOrderDecorator
{
    private readonly decimal _expressDeliveryCost;
    private readonly bool _isExpressAvailable;
    
    public ExpressDeliveryDecorator(IDecoratableOrder order) : base(order)
    {
        (_isExpressAvailable, _expressDeliveryCost) = CalculateExpressCost();
    }
    
    private (bool isAvailable, decimal cost) CalculateExpressCost()
    {
        try
        {
            var expressStrategy = new ExpressDeliveryStrategy();
            
            var tempOrder = new Order(_decoratedOrder.Customer);
            foreach (var item in _decoratedOrder.Items)
            {
                tempOrder.AddItem(item.MenuItem, item.Quantity);
            }
            
            var deliveryCost = expressStrategy.CalculateDeliveryCost(tempOrder);
            return (true, deliveryCost);
        }
        catch (InvalidOperationException)
        {
            return (false, 0m);
        }
    }
    
    public override decimal Total => base.Total + (_isExpressAvailable ? _expressDeliveryCost : 0);
    
    public override string DeliveryType => _isExpressAvailable ? "Express" : "Standard";
    
    public override void DisplayEnhancedOrder()
    {
        if (_isExpressAvailable)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("          ЭКСПРЕСС ДОСТАВКА");
            Console.WriteLine("========================================");
            Console.WriteLine($"Стоимость экспресс-доставки: {_expressDeliveryCost:C}");
            Console.WriteLine($"Бонус: Бесплатный напиток");
            Console.WriteLine($"Время доставки: 30-40 минут");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("========================================");
            Console.WriteLine("     Экспресс-доставка недоступна");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }
        
        base.DisplayEnhancedOrder();
        
        if (_isExpressAvailable)
        {
            Console.WriteLine();
            Console.WriteLine($"Общая сумма с доставкой: {Total:C}");
            Console.WriteLine("========================================");
        }
    }
    
    public override string GetOrderInfo()
    {
        var type = _isExpressAvailable ? "Express" : "Standard";
        return $"Заказ {Id} | Сумма: {Total:C} | Тип: {type}";
    }
}