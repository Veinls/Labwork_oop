using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderEnhancements;

public class SpecialRequestDecorator : BaseOrderDecorator
{
    private readonly string _specialRequest;
    private readonly decimal _additionalCost;
    
    public SpecialRequestDecorator(IDecoratableOrder decoratedOrder, string specialRequest) 
        : base(decoratedOrder)
    {
        _specialRequest = specialRequest ?? throw new ArgumentNullException(nameof(specialRequest));
        _additionalCost = CalculateAdditionalCost();
    }
    
    private decimal CalculateAdditionalCost()
    {
        if (_specialRequest.Contains("дополнительный соус")) return 1.50m;
        if (_specialRequest.Contains("премиум упаковка")) return 3.00m;
        if (_specialRequest.Contains("подарочная упаковка")) return 2.00m;
        if (_specialRequest.Contains("без лука")) return 0m;
        if (_specialRequest.Contains("острое")) return 0.50m;
        return 0m;
    }
    
    public override decimal Total => _decoratedOrder.Total + _additionalCost;
    
    public override string DeliveryType
    {
        get => "Special";
        set => throw new InvalidOperationException("Нельзя изменить тип доставки для особого заказа");
    }
    
    public override void DisplayEnhancedOrder()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("          ОСОБЫЙ ЗАКАЗ");
        Console.WriteLine("========================================");
        Console.WriteLine($"Особые пожелания: {_specialRequest}");
        if (_additionalCost > 0)
        {
            Console.WriteLine($"Дополнительная стоимость: {_additionalCost:C}");
        }
        Console.WriteLine();
        
        base.DisplayEnhancedOrder();
        
        Console.WriteLine();
        Console.WriteLine($"Итого с учетом особых пожеланий: {Total:C}");
        Console.WriteLine("========================================");
    }
    
    public override string GetOrderInfo()
    {
        var baseInfo = _decoratedOrder.GetOrderInfo();
        return $"{baseInfo} | Особые пожелания";
    }
}