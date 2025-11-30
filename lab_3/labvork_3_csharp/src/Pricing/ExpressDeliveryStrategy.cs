using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Pricing;

public class ExpressDeliveryStrategy : IDeliveryStrategy
{
    private const decimal ExpressDeliveryCost = 8.00m;
    private const decimal FreeExpressThreshold = 100.00m;
    private const decimal MinExpressOrderAmount = 20.00m;

    public decimal CalculateDeliveryCost(Order order)
    {
        if (!IsEligibleForExpress(order))
            throw new InvalidOperationException("Заказ не подходит для экспресс-доставки");
        decimal itemsTotal = GetItemsTotal(order);
        return itemsTotal >= FreeExpressThreshold ? 0 :  ExpressDeliveryCost;
    }

    private bool IsEligibleForExpress(Order order)
    {
        return GetItemsTotal(order) >= MinExpressOrderAmount;
    }
    private decimal GetItemsTotal(Order order)
    {
        return order.Items
            .Where(item => !item.MenuItem.Name.Contains("доставка", StringComparison.OrdinalIgnoreCase))
            .Sum(item => item.Price * item.Quantity);
    }
}