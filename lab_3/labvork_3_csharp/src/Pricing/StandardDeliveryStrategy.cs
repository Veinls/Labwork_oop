using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Pricing;

public class StandardDeliveryStrategy: IDeliveryStrategy
{
    private const decimal BaseDeliveryCost = 5.00m;
    private const decimal DiscountDeliveryCost = 2.50m;
    private const decimal FreeDeliveryThreshold = 50.00m;
    private const decimal DiscountDeliveryThreshold = 25.00m;

    public decimal CalculateDeliveryCost(Order order)
    {
        decimal itemsTotal = GetItemsTotal(order);
        
        if(itemsTotal >= FreeDeliveryThreshold)
            return 0;
        else if (itemsTotal >= DiscountDeliveryThreshold)
            return DiscountDeliveryCost;
        else
            return BaseDeliveryCost;
    }

    private decimal GetItemsTotal(Order order)
    {
        return order.Items
            .Where(item => item.MenuItem.Name != "Доставка")
            .Sum(item => item.Price * item.Quantity);
    }
    
}