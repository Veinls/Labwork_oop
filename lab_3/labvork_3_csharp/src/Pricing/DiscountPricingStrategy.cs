using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Pricing;

public class DiscountPricingStrategy : IPricingStrategy
{
    private readonly decimal _discountPercentage;
    private readonly decimal _minOrderAmount;

    public DiscountPricingStrategy(decimal discountPercentage,  decimal minOrderAmount)
    {
        _discountPercentage =  discountPercentage;
        _minOrderAmount = minOrderAmount;
    }

    public decimal CalculateTotal(Order order)
    {
        if (order.Total < _minOrderAmount)
        {
            Console.WriteLine($"Скидка не применена. Минимальная сумма для скидки: {_minOrderAmount:C}");
            return order.Total;
        }
        
        var discount = order.Total * (_discountPercentage / 100);
        var totalWithDiscount = order.Total - discount;
        
        Console.WriteLine($"Применена скидка {_discountPercentage}%: -{discount:C}");
        Console.WriteLine($"Сумма со скидкой: {totalWithDiscount:C}");
        
        return totalWithDiscount;
    }
}