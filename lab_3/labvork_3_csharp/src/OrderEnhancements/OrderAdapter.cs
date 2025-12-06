using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderEnhancements;

public class OrderAdapter : IDecoratableOrder
{
    private readonly Order _order;
    
    public OrderAdapter(Order order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }
    
    public string Id => _order.Id;
    public Customer Customer => _order.Customer;
    public List<OrderItem> Items => _order.Items;
    public decimal Total => _order.Total;
    
    public string DeliveryType
    {
        get => _order.DeliveryType;
        set => _order.DeliveryType = value;
    }
    
    public void DisplayEnhancedOrder()
    {
        Console.WriteLine($"Заказ {_order.Id}");
        Console.WriteLine($"Клиент: {_order.Customer.Name} ({_order.Customer.Phone})");
        Console.WriteLine($"Тип доставки: {_order.DeliveryType}");
        Console.WriteLine($"Статус: {_order.Status}");
        Console.WriteLine("Состав заказа:");
        foreach (var item in _order.Items)
        {
            var priceInfo = item.Price == 0 ? "БЕСПЛАТНО" : $"{item.Price:C}";
            Console.WriteLine($"  {item.MenuItem.Name} x{item.Quantity} - {priceInfo}");
        }
        Console.WriteLine($"Итого: {_order.Total:C}");
        Console.WriteLine($"Создан: {_order.CreatedAt:HH:mm:ss}");
    }
    
    public string GetOrderInfo()
    {
        return _order.GetOrderInfo();
    }
}