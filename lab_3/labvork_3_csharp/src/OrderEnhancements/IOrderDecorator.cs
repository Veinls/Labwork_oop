using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderEnhancements;

public abstract class OrderDecorator
{
    protected Order _order;
    
    protected OrderDecorator(Order order)
    {
        _order = order;
    }
    
    public string GetOrderInfo()
    {
        return $"Заказ {_order.Id} | Сумма: {_order.Total:C} | Статус: {_order.Status}";
    }
    
    public void DisplayOrderItems()
    {
        Console.WriteLine("Состав заказа:");
        foreach (var item in _order.Items)
        {
            var priceInfo = item.Price == 0 ? "БЕСПЛАТНО" : $"{item.Price:C}";
            Console.WriteLine($"  {item.MenuItem.Name} x{item.Quantity} - {priceInfo}");
        }
        Console.WriteLine($"Итого: {_order.Total:C}");
    }
    
    protected void AddService(string serviceId, string serviceName, decimal cost)
    {
        _order.AddItem(new MenuItem(serviceId, serviceName, cost), 1);
        if (cost > 0)
            Console.WriteLine($"Добавлена услуга: {serviceName} - {cost:C}");
        else
            Console.WriteLine($"Добавлен бонус: {serviceName}");
    }
    
    protected void SetDeliveryType(string deliveryType)
    {
        _order.DeliveryType = deliveryType;
        Console.WriteLine($"Тип доставки: {deliveryType}");
    }
    
    protected bool IsOrderInStatus(OrderStatus status)
    {
        return _order.Status == status;
    }
}