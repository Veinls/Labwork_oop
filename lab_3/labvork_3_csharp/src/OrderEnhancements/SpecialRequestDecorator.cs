using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderEnhancements;

public class SpecialRequestDecorator : IOrderDecorator
{
    private readonly Order _order;
    private readonly string _specialRequest;
    private decimal _additionalCost;

    public SpecialRequestDecorator(Order order, string specialRequest)
    {
        _order = order;
        _specialRequest = specialRequest;
        _additionalCost = CalculateAdditionalCost();
        ApplySpecialRequest();
    }
    
    private decimal CalculateAdditionalCost()
    {
        if (_specialRequest.Contains("дополнительный соус")) return 1.50m;
        if (_specialRequest.Contains("премиум упаковка")) return 3.00m;
        if (_specialRequest.Contains("подарочная упаковка")) return 2.00m;
        return 0m;
    }
    
    private void ApplySpecialRequest()
    {
        if (_additionalCost > 0)
        {
            _order.AddItem(new MenuItem("special", "Особая услуга", _additionalCost), 1);
        }
        _order.DeliveryType = "Special";
    }
    
    public void DisplayEnhancedOrder()
    {
        Console.WriteLine($"Особый заказ {_order.Id}");
        Console.WriteLine($"Пожелание: {_specialRequest}");
        Console.WriteLine("Состав заказа:");
        foreach (var item in _order.Items)
        {
            Console.WriteLine($"  {item.MenuItem.Name} x{item.Quantity} - {item.Price:C}");
        }
        Console.WriteLine($"Итого: {_order.Total:C}");
    }
    
    public string GetOrderInfo()
    {
        return $"Заказ {_order.Id} | Сумма: {_order.Total:C} | Тип: Special";
    }
}